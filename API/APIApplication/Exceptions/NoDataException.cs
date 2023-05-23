using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace APIApplication.Exceptions
{
    /// <summary>
    /// When this exception is thrown, it is because there is no data returned from the external datasource
    /// </summary>
    public class NoDataException : System.Exception
    {
        public NoDataException()
        {
        }

        public NoDataException(string message): base(message)
        {
        }

        public NoDataException(string message, System.Exception inner): base(message, inner)
        {
        }

        protected NoDataException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
