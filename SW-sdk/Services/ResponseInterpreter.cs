using SW.Entities;
using System;
using System.Collections.Generic;
using System.IO;

namespace SW.Services
{
    internal static class ResponseInterpreter
    {
        private const int MaxRawBodyLength = 20000;
        private const int MaxJsonDepth = 128;

        internal static T Interpret<T>(int code, string reason, string body, Exception readError) where T : Response, new()
        {
            T result = InterpretBody<T>(code, reason, body, readError);
            result.HttpStatusCode = code;
            result.HadEmptyBody = readError == null && (body == null || body.Trim().Length == 0);
            return result;
        }
        private static T InterpretBody<T>(int code, string reason, string body, Exception readError) where T : Response, new()
        {
            bool isSuccess = code >= 200 && code < 300;
            string description = string.IsNullOrEmpty(reason) ? "HTTP " + code : "HTTP " + code + " " + reason;
            if (readError != null)
            {
                string detail = readError.Message;
                if (!string.IsNullOrEmpty(body))
                    detail += " | Respuesta parcial recibida: " + Truncate(body);
                return new T()
                {
                    status = "error",
                    message = isSuccess
                        ? string.Format("El servicio respondio HTTP {0} pero no fue posible leer la respuesta completa. Verifique si la operacion fue realizada antes de reintentar.", code)
                        : description + ". No fue posible leer la respuesta completa.",
                    messageDetail = detail
                };
            }
            List<KeyValuePair<string, string>> conversionErrors;
            T parsed = TryDeserialize<T>(body, out conversionErrors);
            if (parsed == null)
            {
                bool isEmpty = body == null || body.Trim().Length == 0;
                return new T()
                {
                    status = "error",
                    message = isSuccess
                        ? string.Format("El servicio respondio HTTP {0} con un formato que el SDK no reconoce. Verifique si la operacion fue realizada antes de reintentar.", code)
                        : description,
                    messageDetail = isEmpty ? "El servicio respondio con un cuerpo vacio." : Truncate(body)
                };
            }
            AppendConversionErrors(parsed, conversionErrors, body);
            if (!isSuccess)
            {
                if (string.IsNullOrEmpty(parsed.status))
                    parsed.status = "error";
                if (string.IsNullOrEmpty(parsed.message))
                    parsed.message = description;
                if (string.IsNullOrEmpty(parsed.messageDetail))
                    parsed.messageDetail = Truncate(body);
            }
            return parsed;
        }
        private static T TryDeserialize<T>(string body, out List<KeyValuePair<string, string>> conversionErrors) where T : Response, new()
        {
            var errors = new List<KeyValuePair<string, string>>();
            conversionErrors = errors;
            if (body == null || body.Trim().Length == 0)
                return null;
            try
            {
                using (var reader = new Newtonsoft.Json.JsonTextReader(new StringReader(body)))
                {
                    reader.MaxDepth = MaxJsonDepth;
                    var token = Newtonsoft.Json.Linq.JToken.ReadFrom(reader);
                    if (token.Type != Newtonsoft.Json.Linq.JTokenType.Object)
                        return null;
                    // Rechaza contenido adicional antes de convertir.
                    while (reader.Read())
                    {
                        if (reader.TokenType != Newtonsoft.Json.JsonToken.Comment)
                            return null;
                    }
                    var serializer = Newtonsoft.Json.JsonSerializer.CreateDefault();
                    if (!HasKnownProperty<T>((Newtonsoft.Json.Linq.JObject)token, serializer))
                        return null;
                    serializer.Error += (sender, args) =>
                    {
                        errors.Add(new KeyValuePair<string, string>(args.ErrorContext.Path, args.ErrorContext.Error.Message));
                        args.ErrorContext.Handled = true;
                    };
                    using (var valueReader = new ResponseJsonReader(token, body))
                    {
                        valueReader.MaxDepth = MaxJsonDepth;
                        T result = serializer.Deserialize<T>(valueReader);
                        return result == null || IsEmpty(result) ? null : result;
                    }
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        private sealed class ResponseJsonReader : Newtonsoft.Json.Linq.JTokenReader
        {
            private readonly string _body;
            private readonly List<int> _lineStarts = new List<int> { 0 };

            internal ResponseJsonReader(Newtonsoft.Json.Linq.JToken token, string body) : base(token)
            {
                _body = body;
                for (int index = 0; index < body.Length; index++)
                {
                    if (body[index] == '\r')
                    {
                        if (index + 1 < body.Length && body[index + 1] == '\n')
                            index++;
                        _lineStarts.Add(index + 1);
                    }
                    else if (body[index] == '\n')
                        _lineStarts.Add(index + 1);
                }
            }
            public override string ReadAsString()
            {
                string value = base.ReadAsString();
                // Permite recuperar campos incompatibles sin desincronizar el JSON.
                // En propiedades string se conserva el texto original.
                var token = CurrentToken;
                if (token != null && (token.Type == Newtonsoft.Json.Linq.JTokenType.Integer
                    || token.Type == Newtonsoft.Json.Linq.JTokenType.Float
                    || token.Type == Newtonsoft.Json.Linq.JTokenType.Boolean
                    || token.Type == Newtonsoft.Json.Linq.JTokenType.Date))
                {
                    var lineInfo = (Newtonsoft.Json.IJsonLineInfo)token;
                    int end = _lineStarts[lineInfo.LineNumber - 1] + lineInfo.LinePosition;
                    if (token.Type == Newtonsoft.Json.Linq.JTokenType.Date)
                    {
                        // Se decodifica el literal de la fecha para conservar escapes, precision y zona horaria.
                        int start = _body.LastIndexOf(_body[end - 1], end - 2);
                        using (var dateReader = new Newtonsoft.Json.JsonTextReader(new StringReader(_body.Substring(start, end - start))))
                            value = dateReader.ReadAsString();
                    }
                    else
                    {
                        int start = end;
                        while (start > 0 && !char.IsWhiteSpace(_body[start - 1]) && ",:[]{}/*".IndexOf(_body[start - 1]) < 0)
                            start--;
                        value = _body.Substring(start, end - start);
                    }
                    SetToken(Newtonsoft.Json.JsonToken.String, value, false);
                }
                return value;
            }
        }
        private static bool HasKnownProperty<T>(Newtonsoft.Json.Linq.JObject json, Newtonsoft.Json.JsonSerializer serializer)
        {
            return HasKnownProperty(typeof(T), json, serializer, 0);
        }
        private static bool HasKnownProperty(Type type, Newtonsoft.Json.Linq.JObject json, Newtonsoft.Json.JsonSerializer serializer, int depth)
        {
            // Solo cuentan propiedades publicas del DTO (sin distinguir mayusculas); un objeto anidado
            // cuenta si trae alguna propiedad conocida de su tipo.
            var contract = serializer.ContractResolver.ResolveContract(type) as Newtonsoft.Json.Serialization.JsonObjectContract;
            if (contract == null)
                return depth > 0;
            foreach (var property in contract.Properties)
            {
                if (property.Ignored || string.IsNullOrEmpty(property.UnderlyingName))
                    continue;
                var member = type.GetProperty(property.UnderlyingName,
                    System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
                if (member == null || member.GetIndexParameters().Length != 0)
                    continue;
                foreach (var received in json.Properties())
                {
                    if (!string.Equals(received.Name, property.PropertyName, StringComparison.OrdinalIgnoreCase))
                        continue;
                    var inner = received.Value as Newtonsoft.Json.Linq.JObject;
                    if (inner != null && depth < 8 && IsNestedObjectType(member.PropertyType)
                        && !HasKnownProperty(member.PropertyType, inner, serializer, depth + 1))
                        continue;
                    return true;
                }
            }
            return false;
        }
        private static bool IsNestedObjectType(Type type)
        {
            return !type.IsValueType && type != typeof(string) && type != typeof(object)
                && !typeof(System.Collections.IEnumerable).IsAssignableFrom(type);
        }
        private static bool IsEmpty<T>(T response)
        {
            foreach (var property in typeof(T).GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance))
            {
                if (!property.CanRead || property.GetIndexParameters().Length != 0)
                    continue;
                object value = property.GetValue(response, null);
                if (value == null || (value is string && ((string)value).Length == 0))
                    continue;
                if (property.PropertyType.IsValueType && value.Equals(Activator.CreateInstance(property.PropertyType)))
                    continue;
                return false;
            }
            return true;
        }
        private static void AppendConversionErrors(Response response, List<KeyValuePair<string, string>> errors, string body)
        {
            if (errors.Count == 0)
                return;
            var paths = new List<string>();
            foreach (var error in errors)
            {
                string path = string.IsNullOrEmpty(error.Key) ? "$" : error.Key;
                if (!paths.Contains(path))
                    paths.Add(path);
            }
            string detail = "Campos de la respuesta no interpretados: " + string.Join(", ", paths.ToArray())
                + " | Respuesta recibida: " + Truncate(body);
            response.messageDetail = string.IsNullOrEmpty(response.messageDetail)
                ? detail : response.messageDetail + " | " + detail;
        }
        private static string Truncate(string value)
        {
            if (value == null || value.Length <= MaxRawBodyLength)
                return value;
            return value.Substring(0, MaxRawBodyLength) + "... [respuesta truncada]";
        }
    }
}
