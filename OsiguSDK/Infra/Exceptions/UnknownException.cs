using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OsiguSDK.Infra.Exceptions
{
    public class UnknownException : RequestException
    {
        public UnknownException() : base()
        {
        }

        public UnknownException(string s) : base(s)
        {
        }

        public UnknownException(Exception e) : base(e)
        {
        }

        public UnknownException(string s, Exception e) : base(s, e)
        {
        }

        public UnknownException(Exception e, int responseCode) : base(e, responseCode)
        {
        }

        public UnknownException(string errorText, int responseCode) : base(errorText, responseCode)
        {
        }
    }
}
