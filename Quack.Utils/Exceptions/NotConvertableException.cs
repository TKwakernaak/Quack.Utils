using System;
using System.Collections.Generic;
using System.Text;

namespace Quack.Utils.Exceptions
{
    public class NotConvertableException : Exception
    {
        public NotConvertableException() : base()
        {
        }

        public NotConvertableException(string message) : base(message)
        {
        }

        public NotConvertableException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
