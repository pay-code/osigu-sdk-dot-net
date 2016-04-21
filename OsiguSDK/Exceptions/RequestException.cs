using System;
using System.Collections.Generic;

namespace OsiguSDK.Core.Exceptions
{
    public class RequestException : Exception
    {
        public RequestException()
        {
        }

        public RequestException(string s)
            : base(s)
        {
        }

        public RequestException(Exception e)
            : base(e.Message, e)
        {
        }

        public RequestException(string s, Exception e)
            : base(s, e)
        {
        }

        public RequestException(Exception e, int responseCode)
            : base(e.Message, e)
        {
            ResponseCode = responseCode;
        }


        public RequestException(string errorText, int responseCode)
            : this(errorText)
        {
            ResponseCode = responseCode;
        }

        public RequestException(string errorText, int responseCode, IList<Models.RequestError.ValidationError> errors)
            : this(errorText)
        {
            ResponseCode = responseCode;
            Errors = errors;
        }


        public virtual int ResponseCode { get; private set; }
        public IList<Models.RequestError.ValidationError> Errors { get; set; } 
    }
}
