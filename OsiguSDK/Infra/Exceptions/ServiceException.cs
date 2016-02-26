using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OsiguSDK.Infra.Exceptions
{
    public class ServiceException : RequestException
    {
        public ServiceException() : base()
        {
        }

        public ServiceException(string s) : base(s)
        {
        }

        public ServiceException(Exception e) : base(e)
        {
        }

        public ServiceException(string s, Exception e) : base(s, e)
        {
        }

        public ServiceException(Exception e, int responseCode) : base(e, responseCode)
        {
        }

        public ServiceException(string errorText, int responseCode) : base(errorText, responseCode)
        {
        }
    }
}
