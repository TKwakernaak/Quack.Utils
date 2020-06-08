using System;
using System.Collections.Generic;
using System.Text;

namespace Quack.Utils.Exceptions
{
    public class NotConvertibleException : Exception
    {
        public NotConvertibleException() : base()
        {
        }

        public NotConvertibleException(string message) : base(message)
        {
        }

        public NotConvertibleException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
