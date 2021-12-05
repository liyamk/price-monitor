namespace PriceMonitor.Exceptions
{
    using System;

    public class MissingEnvVarException : Exception
    {
        public MissingEnvVarException(string message) : base(message)
        {
        }

        public MissingEnvVarException(string message, Exception innerException) : base(message, innerException)
        {               
        }
    }
}
