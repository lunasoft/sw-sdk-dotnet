using System;
using System.Runtime.Serialization;

namespace SW.Helpers
{
    [Serializable]
    internal class TestEnviromentException : Exception
    {
        public TestEnviromentException()
        {
        }

        public TestEnviromentException(string message) : base(message)
        {
        }

        public TestEnviromentException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected TestEnviromentException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}