using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using log4net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OsiguSDK.Config;
using OsiguSDK.Infra.Exceptions;
using OsiguSDK.Infra.Utils;
using RestSharp;

namespace OsiguSDK.Infra
{
    public abstract class BaseClient
    {
        protected static ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private IRestClient _client = null;

        /// <summary>
        /// Initialize ClientBase </summary>
        /// <param name="configuration"> </param>
        protected BaseClient(IConfiguration configuration)
        {
            Configuration = configuration;
            SetRestClient();
        }

        /// <summary>
        /// Get Configuration object </summary>
        /// <returns> Configuration </returns>
        protected IConfiguration Configuration { get; private set; }

        /// <summary>
        /// Set Rest Client
        /// </summary>
        private void SetRestClient()
        {
            _client = new RestClient(Configuration.BaseUrl);

            var assembly = Assembly.GetExecutingAssembly();
            var assemblyName = new AssemblyName(assembly.FullName);
            var version = assemblyName.Version;

            _client.UserAgent = "Osigu-SDK-" + version;
        }

        /// <summary>
        /// Execute method and deserialize response json
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="requestData"></param>
        /// <returns>T</returns>
        protected T ExecuteMethod<T>(RequestData requestData)
        {
            var response = SendRequest(requestData);
            var deserializedResponse = Deserialize<T>(response, requestData.RootElement);
            return deserializedResponse;
        }

        /// <summary>
        /// Execute method asynchronously and deserialize response json
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="requestData"></param>
        /// <param name="callbackResponse"></param>
        /// 
        protected void ExecuteMethodAsync<T>(RequestData requestData, Action<T, RequestException> callbackResponse) where T : new()
        {
            SendRequestAsync(requestData, callbackResponse);
        }

        /// <summary>
        /// Execute method and validate response 
        /// </summary>
        /// <param name="requestData"></param>
        protected void ExecuteMethod(RequestData requestData)
        {
            var response = SendRequest(requestData);
            ValidateResponse(response);
        }

        /// <summary>
        /// Send  request
        /// </summary>
        /// <param name="requestData"></param>
        /// <returns> IRestResponse </returns>
        private IRestResponse SendRequest(RequestData requestData)
        {
            var request = CreateRequest(requestData);
            return _client.Execute(request);
        }

        /// <summary>
        /// Send  request asynchronously
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="requestData"></param>
        /// <param name="callbackResponse"></param>
        private void SendRequestAsync<T>(RequestData requestData, Action<T, RequestException> callbackResponse) where T : new()
        {
            var request = CreateRequest(requestData);

            _client.ExecuteAsync(request, (response) =>
            {
                try
                {
                    var jsonObject = Deserialize<T>(response, requestData.RootElement);
                    callbackResponse(jsonObject, null);
                }
                catch (RequestException e)
                {
                    callbackResponse(new T(), e);
                }
            });
        }

        /// <summary>
        /// Add Request parameters 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="formParams"></param>
        protected void AddRequestParams(ref RestRequest request, object formParams)
        {  
            var formParamsDictionary = formParams.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public).ToDictionary(prop => (prop.GetCustomAttributes(typeof(DisplayNameAttribute), false).First() as DisplayNameAttribute).DisplayName, prop => prop.GetValue(formParams, null));

            if (formParamsDictionary == null)
            {
                if (Logger.IsDebugEnabled)
                {
                    Logger.Debug("No request form parameters!");
                }
                return;
            }

            foreach (var entry in formParamsDictionary.Where(entry => entry.Value != null))
            {
                if (entry.Value is string[])
                {
                    var arr = (string[])entry.Value;
                    foreach (var arrItem in arr.Where(arrItem => arrItem != null)) {
                        request.AddParameter(entry.Key, arrItem, ParameterType.GetOrPost);
                    }
                }
                else
                {
                    request.AddParameter(entry.Key, Convert.ToString(entry.Value), ParameterType.GetOrPost);
                }
            }

            if (Logger.IsDebugEnabled)
            {
                Logger.Debug("Request form parameters: " + string.Join(", ", request.Parameters.ToArray().Select<Parameter, string>(ism => ism != null ? ism.ToString() : "{}").ToArray()));
            }
        }

        private RestRequest CreateRequest(RequestData requestData)
        {
            if (Logger.IsDebugEnabled)
            {
                Logger.Debug("Initiating connection to resource path: " + requestData.ResourcePath);
            }

            var request = new RestRequest(requestData.ResourcePath);

            //setup connection with custom authorization
            var authentication = Configuration.Authentication;
            //if (authentication.Type.Equals(Authentication.AuthType.BASIC))
            //{
            //    request = SetupRequestWithCustomAuthorization(ref request, "Basic", GetAuthorizationHeader(authentication.Username, authentication.Password));
            //}
            
            if (authentication.Type.Equals(Authentication.AuthType.OAuth))
            {
                request = SetupRequestWithCustomAuthorization(ref request, "OAuth", authentication.AccessToken);
            }

            request.AddHeader("Accept", "*/*");
            request.Method = requestData.RequestMethod;

            if (requestData.RequestMethod == Method.POST)
            {
                request.AddHeader("content-type", requestData.ContentType);

                if (requestData.FormParams != null)
                {
                    if (requestData.ContentType.Equals(RequestData.JSON_CONTENT_TYPE))
                    {
                        request.JsonSerializer = new RestSharpJsonNetSerializer();
                        request.RequestFormat = DataFormat.Json;
                        request.AddBody(requestData.FormParams);
                    }
                    else if (requestData.ContentType.Equals(RequestData.FORM_URL_ENCOEDED_CONTENT_TYPE))
                    {
                        AddRequestParams(ref request, requestData.FormParams);
                    }
                }
            }

            return request;
        }

        private RestRequest SetupRequestWithCustomAuthorization(ref RestRequest request, string authorizationScheme, string authHeaderValue)
        {
            if (authHeaderValue != null)
            {
                request.AddHeader("Authorization", authorizationScheme + " " + authHeaderValue);

                if (Logger.IsDebugEnabled)
                {
                    Logger.Debug("Authorization type " + authorizationScheme + " using " + authHeaderValue);
                }
            }

            return request;
        }

        /// <summary>
        /// Deserialize response stream
        /// </summary>
        /// <param name="response"> </param>
        /// <param name="rootElement"> </param>
        protected T Deserialize<T>(IRestResponse response, string rootElement)
        {
            var responseCode = (int)response.StatusCode;

            if (Logger.IsDebugEnabled)
            {
                Logger.Debug("Response status code: " + responseCode);
            }

            if (responseCode >= 200 && responseCode < 300)
            {	
                try
                {
                    return DeserializeStream<T>(response.Content, rootElement);
                }
                catch (Exception e)
                {
                    throw new RequestException(e);
                }
            }
            else
            {
                //Read RequestError from the response and throw the Exception
                throw ReadRequestException(response);
            }
        }

        /// <summary>
        /// Deserialize from stream
        /// </summary>
        /// <param name="content"></param>
        /// <param name="rootElement"> </param>
        protected T DeserializeStream<T>(String content, string rootElement)
        {
            return ConvertJsonToObject<T>(content, rootElement);
        }

        /// <summary>
        /// Convert object to specific object type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        protected T ConvertJsonToObject<T>(String json)
        {
            return ConvertJsonToObject<T>(json, null);
        }

        /// <summary>
        /// Convert JSON to specific object type using json root element
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <param name="rootElement"></param>
        /// <returns></returns>
        public static T ConvertJsonToObject<T>(String json, string rootElement)
        {
            if (Logger.IsDebugEnabled)
            {
                Logger.Debug("JSON to Deserialize: " + json);
            }

            var settings = new JsonSerializerSettings
            {
                MissingMemberHandling = MissingMemberHandling.Ignore,
                DateParseHandling = DateParseHandling.DateTime,
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                NullValueHandling = NullValueHandling.Ignore
            };

            try
            {
                if (null != rootElement)
                {
                    var rootObject = JObject.Parse(json);
                    var rootToken = rootObject.SelectToken(rootElement);

                    return JsonConvert.DeserializeObject<T>(rootToken.ToString(), settings);
                }
                return JsonConvert.DeserializeObject<T>(json, settings);
            }
            catch (Exception e)
            {
                throw new RequestException(e);
            }
        }

        private RequestException ReadRequestException(IRestResponse response)
        {
            const string errorText = "Unexpected error occured.";
            var responseCode = (int) response.StatusCode;

            try
            {
                var requestError = DeserializeStream<RequestError>(response.Content, null);

                if (responseCode >= 400 && responseCode < 500)
                {
                    return requestError.Errors != null ? new RequestException(requestError.Errors.First().Message, responseCode) : new RequestException(requestError.Message, responseCode);
                }
                    
                //If is not a request exception then is a service exception
                return new ServiceException(requestError.Message, responseCode);                
            }
            catch (Exception e)
            {
                return new UnknownException(e, (int)response.StatusCode);
            }
        }

        protected void ValidateResponse(IRestResponse response)
        {
            var responseCode = (int)response.StatusCode;

            if (Logger.IsDebugEnabled)
            {
                Logger.Debug("Response status code: " + responseCode);
            }

            if (!(responseCode >= 200 && responseCode < 300))
            {
                throw ReadRequestException(response);
            }  
        }

        protected string GetIdFromResourceUrl(string resourceUrl)
        {
            var id = "";
            if (resourceUrl.Contains('/'))
            {
                var arrResourceUrl = resourceUrl.Split('/');
                if (arrResourceUrl.Length > 0)
                {
                    id = arrResourceUrl[arrResourceUrl.GetUpperBound(0)];
                }
            }

            return id;
        }

        private string GetAuthorizationHeader(string username, string password)
        {
            var credentials = username + ":" + password;
            var credentialsAsBytes = Encoding.UTF8.GetBytes(credentials);
            return Convert.ToBase64String(credentialsAsBytes).Trim();
        }
    }
}
