using System;

namespace OsiguSDK.Core.Exceptions
{
    internal class ConfigurationException : Exception
    {
        public ConfigurationException()
        {
        }

        public ConfigurationException(string message) : base(message)
        {
        }

        public ConfigurationException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public ConfigurationException(Exception e) : base(e.Message, e)
        {
        }
    }
}
