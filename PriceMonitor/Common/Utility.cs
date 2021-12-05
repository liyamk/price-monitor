namespace PriceMonitor.Common
{
    using System;
    using PriceMonitor.Exceptions;

    public static class Utility
    {
        public static string GetEnvironmentValue(string key)
        {
            if(string.IsNullOrEmpty(key))
            {
                throw new MissingEnvVarException("Environment key can not be null or empty");
            }

            string value = Environment.GetEnvironmentVariable(key, EnvironmentVariableTarget.Process);

            if(string.IsNullOrEmpty(value))
            {
                throw new MissingEnvVarException($"Unable to find environment value for given key: {key}");
            }

            return value;
        }
    }
}
