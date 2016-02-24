using System.Text;
using Newtonsoft.Json;

namespace OsiguSDK.Models.Requests
{
    class RequestError
    {
        /// <summary>
        /// internally used to indicate the type of exception being stored is a ServiceException
        /// </summary>
        public const int SERVICEEXCEPTION = 1;
        /// <summary>
        /// internally used to indicate the type of exception being stored is a PolicyException
        /// </summary>
        public const int POLICYEXCEPTION = 2;

        /**
	    * instance of a ServiceException
	    */
        private ServiceException _serviceException = null;
        /** 
         * instance of a PolicyException
        */
        private PolicyException _policyException = null;

        [JsonProperty(PropertyName = "serviceException")]
        public ServiceException ServiceException
        {
            get
            {
                return _serviceException;
            }
            set
            {
                ExceptionType = SERVICEEXCEPTION;
                _serviceException = value;
            }
        }

        [JsonIgnore]
        public int ExceptionType;

        [JsonIgnore]
        public int HttpResponseCode;

        /// <summary>
        /// return the policyException instance
        /// </summary>
        [JsonProperty(PropertyName = "policyException")]
        public PolicyException PolicyException
        {
            get
            {
                return _policyException;
            }
            set
            {
                this._policyException = value;
                ExceptionType = POLICYEXCEPTION;
            }
        }

        /// <summary>
        /// utility constructor to create an RequestError instance with all fields set </summary>
        /// <param name="type"> </param>
        /// <param name="httpResponseCode"> </param>
        /// <param name="text"> </param>
        /// <param name="variables"> </param>on
        public RequestError(int type, int httpResponseCode, string text, params string[] variables)
        {
            switch (type)
            {
                case SERVICEEXCEPTION:
                    _serviceException = new ServiceException {Text = text, Variables = variables};
                    break;
                case POLICYEXCEPTION:
                    _policyException = new PolicyException {Text = text, Variables = variables};
                    break;
            }
            ExceptionType = type;
            HttpResponseCode = httpResponseCode;
        }

        /// <summary>
        /// default constructor
        /// </summary>
        public RequestError()
        {
            HttpResponseCode = 400;
            ExceptionType = 0;
        }

        /// <summary>
        /// generate a textual representation of the RequestError including all nested elements and classes 
        /// </summary>
        public override string ToString()
        {
            var buffer = new StringBuilder();            
            if (_serviceException != null)
            {
                buffer.Append("serviceException = {");
                buffer.Append(", text = ");
                buffer.Append(_serviceException.Text);
                buffer.Append(", variables = ");
                if (_serviceException.Variables != null)
                {
                    buffer.Append("{");
                    for (int i = 0; i < _serviceException.Variables.Length; i++)
                    {
                        buffer.Append("[");
                        buffer.Append(i);
                        buffer.Append("] = ");
                        buffer.Append(_serviceException.Variables[i]);
                    }
                    buffer.Append("}");
                }
                buffer.Append("}");
            }
            if (_policyException != null)
            {
                buffer.Append("policyException = {");
                buffer.Append(", text = ");
                buffer.Append(_policyException.Text);
                buffer.Append(", variables = ");
                if (_policyException.Variables != null)
                {
                    buffer.Append("{");
                    for (var i = 0; i < _policyException.Variables.Length; i++)
                    {
                        buffer.Append("[");
                        buffer.Append(i);
                        buffer.Append("] = ");
                        buffer.Append(_policyException.Variables[i]);
                    }
                    buffer.Append("}");
                }
                buffer.Append("}");
            }

            return buffer.ToString();
        }
    }
}
