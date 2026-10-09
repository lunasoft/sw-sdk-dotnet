using SW.Helpers;
using SW.Services.AcceptReject;
using SW.Services.Account.AccountBalance;
using SW.Services.Account.AccountUser;
using SW.Services.Authentication;
using SW.Services.Cancelation;
using SW.Services.CancelationRetention;
using SW.Services.Csd;
using SW.Services.Pdf;
using SW.Services.Pendings;
using SW.Services.Relations;
using SW.Services.Resend;
using SW.Services.Stamp;
using SW.Services.StampRetention;
using SW.Services.Storage;
using SW.Services.Taxpayer;
using SW.Services.Validate;
using System;
using System.IO;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SW.Helpers
{
    internal static class ResponseHelper
    {
        private const int MaxExceptionBodyBytes = 1024 * 1024;
        private const int ExceptionBodyReadTimeout = 30000;

        private static T ToResponse<T>(Exception ex) where T : SW.Entities.Response, new()
        {
            // La autenticacion tiene prioridad.
            for (Exception current = ex; current != null; current = current.InnerException)
            {
                AuthenticationResponseException authentication = current as AuthenticationResponseException;
                if (authentication != null)
                {
                    var original = authentication.Response;
                    return new T
                    {
                        status = original.status,
                        message = original.message,
                        messageDetail = original.messageDetail,
                        HttpStatusCode = original.HttpStatusCode,
                        HadEmptyBody = original.HadEmptyBody
                    };
                }
            }
            for (Exception current = ex; current != null; current = current.InnerException)
            {
                WebException webException = current as WebException;
                HttpWebResponse response = webException == null ? null : webException.Response as HttpWebResponse;
                if (response != null)
                    return ReadExceptionResponse<T>(response);
            }
            return new T
            {
                status = "error",
                message = ex.Message,
                messageDetail = ex.GetErrorDetail()
            };
        }

        private static T ReadExceptionResponse<T>(HttpWebResponse response) where T : SW.Entities.Response, new()
        {
            int code = (int)response.StatusCode;
            string reason = response.StatusDescription;
            string charset = HttpBodyReader.ExplicitCharset(response.ContentType);
            Exception readError;
            byte[] bytes = ReadExceptionBody(response, out readError);
            string body = HttpBodyReader.Decode(bytes, charset);
            return SW.Services.ResponseInterpreter.Interpret<T>(code, reason, body, readError);
        }

        private static byte[] ReadExceptionBody(HttpWebResponse response, out Exception error)
        {
            error = null;
            using (MemoryStream bytes = new MemoryStream())
            {
                try
                {
                    using (Stream stream = response.GetResponseStream())
                    {
                        var elapsed = System.Diagnostics.Stopwatch.StartNew();
                        byte[] buffer = new byte[4096];
                        while (true)
                        {
                            int remaining = ExceptionBodyReadTimeout - (int)elapsed.ElapsedMilliseconds;
                            if (remaining <= 0)
                                throw new IOException("Se agoto el plazo de lectura de la respuesta.");
                            if (stream.CanTimeout)
                                stream.ReadTimeout = remaining;
                            int read = stream.Read(buffer, 0, Math.Min(buffer.Length, MaxExceptionBodyBytes + 1 - (int)bytes.Length));
                            if (read == 0)
                                break;
                            bytes.Write(buffer, 0, read);
                            if (bytes.Length > MaxExceptionBodyBytes)
                            {
                                bytes.SetLength(MaxExceptionBodyBytes);
                                throw new IOException("La respuesta excedio el limite de lectura de 1048576 bytes.");
                            }
                        }
                    }
                    if (response.ContentLength >= 0 && bytes.Length < response.ContentLength)
                        error = new IOException(string.Format("Respuesta incompleta: se anunciaron {0} bytes y se recibieron {1}.", response.ContentLength, bytes.Length));
                }
                catch (Exception ex)
                {
                    error = ex;
                }
                finally
                {
                    try
                    {
                        response.Close();
                    }
                    catch (Exception)
                    {
                        // El cierre no debe reemplazar el diagnostico recibido.
                    }
                }
                return bytes.ToArray();
            }
        }

        internal static AuthResponse ToAuthResponse(this Exception ex)
        {
            return ToResponse<AuthResponse>(ex);
        }
        internal static AccountResponse ToAccountResponse(this Exception ex)
        {
            return ToResponse<AccountResponse>(ex);
        }
        internal static AccountBalanceResponse ToAccountBalanceResponse(this Exception ex)
        {
            return ToResponse<AccountBalanceResponse>(ex);
        }
        internal static CancelationResponse ToCancelationResponse(this Exception ex)
        {
            return ToResponse<CancelationResponse>(ex);
        }
        internal static StampResponseV1 ToStampResponseV1(this Exception ex)
        {
            return ToResponse<StampResponseV1>(ex);
        }
        internal static StampResponseV1CadenaOriginalSAT ToStampResponseV1CadenaOriginalSAT(this Exception ex)
        {
            return ToResponse<StampResponseV1CadenaOriginalSAT>(ex);
        }
        internal static StampResponseV2 ToStampResponseV2(this Exception ex)
        {
            return ToResponse<StampResponseV2>(ex);
        }
        internal static StampResponseV3 ToStampResponseV3(this Exception ex)
        {
            return ToResponse<StampResponseV3>(ex);
        }
        internal static StampResponseV4 ToStampResponseV4(this Exception ex)
        {
            return ToResponse<StampResponseV4>(ex);
        }
        internal static ValidateXMLResponse ToValidateXMLResponse(this Exception ex)
        {
            return ToResponse<ValidateXMLResponse>(ex);
        }
        internal static string GetErrorDetail(this Exception ex)
        {
            StringBuilder detail = new StringBuilder();
            for (Exception inner = ex.InnerException; inner != null; inner = inner.InnerException)
            {
                if (string.IsNullOrEmpty(inner.Message) || inner.Message == ex.Message)
                    continue;
                if (detail.Length > 0)
                    detail.Append(" -> ");
                detail.Append(inner.Message);
            }
            return detail.ToString();
        }
        internal static AcceptRejectResponse ToAcceptRejectResponse(this Exception ex)
        {
            return ToResponse<AcceptRejectResponse>(ex);
        }
        internal static RelationsResponse ToRelationsResponse(this Exception ex)
        {
            return ToResponse<RelationsResponse>(ex);
        }
        internal static PendingsResponse ToPendingsResponse(this Exception ex)
        {
            return ToResponse<PendingsResponse>(ex);
        }
        internal static ResendResponse ToResendResponse(this Exception ex)
        {
            return ToResponse<ResendResponse>(ex);
        }
        internal static PdfResponse ToPdfResponse(this Exception ex)
        {
            return ToResponse<PdfResponse>(ex);
        }
        internal static CsdResponse ToCsdResponse(this Exception ex)
        {
            return ToResponse<CsdResponse>(ex);
        }
        internal static InfoCsdResponse ToInfoCsdResponse(this Exception ex)
        {
            return ToResponse<InfoCsdResponse>(ex);
        }
        internal static ListInfoCsdResponse ToListInfoCsdResponse(this Exception ex)
        {
            return ToResponse<ListInfoCsdResponse>(ex);
        }
        internal static TaxpayerResponse ToTaxpayerResponse(this Exception ex)
        {
            return ToResponse<TaxpayerResponse>(ex);
        }
        internal static StorageResponse ToStorageResponse(this Exception ex)
        {
            return ToResponse<StorageResponse>(ex);
        }
        internal static AccountUserActionsResponse ToAccountUserActionsResponse(this Exception ex)
        {
            return ToResponse<AccountUserActionsResponse>(ex);
        }
        internal static AccountGetUsersResponse ToAccountGetUsersResponse(this Exception ex)
        {
            return ToResponse<AccountGetUsersResponse>(ex);
        }
        internal static AccountGetUserResponse ToAccountGetUserResponse(this Exception ex)
        {
            return ToResponse<AccountGetUserResponse>(ex);
        }
        internal static StampRetentionResponseV3 ToStampRetentionResponseV3(this Exception ex)
        {
            return ToResponse<StampRetentionResponseV3>(ex);
        }
    }
}
