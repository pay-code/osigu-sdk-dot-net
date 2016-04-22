using System.Net;
using System.Text;
using OsiguSDK.Core.Client;
using OsiguSDK.Core.Config;
using OsiguSDK.Core.Requests;
using OsiguSDK.Providers.Models;
using RestSharp;

namespace OsiguSDK.Providers.Clients
{
    public class QueueClient : BaseClient
    {
        public QueueClient(IConfiguration configuration) : base(configuration)
        {
        }

        public QueueStatus CheckQueueStatus(string queueId)
        {
            var urlBuilder = new StringBuilder("/queue/").Append(queueId);
            var requestData = new RequestData(urlBuilder.ToString(), Method.GET, null, null);

            var response = SendRequest(requestData);
            
            //if the result is 303 (SEE OTHER) means that the resource was created successfully
            if (response.StatusCode == HttpStatusCode.SeeOther)
            {
                var locationUrl = GetLocationHeader(response);
                var id = base.GetIdFromResourceUrl(locationUrl);

                return new QueueStatus
                {
                    Status = QueueStatus.QueueStatusEnum.COMPLETED,
                    ResourceId = id
                };
            }

            var queueStatus = Deserialize<QueueStatus>(response, requestData.RootElement);

            return queueStatus;
        }
    
    }
}
