using System;

namespace WebApiTests.Exceptions
{
    public class ModelBindingException : Exception
    {
        public ModelBindingException(string message, Exception innerException) : base(message, innerException)
        {
            
        }
    }
}