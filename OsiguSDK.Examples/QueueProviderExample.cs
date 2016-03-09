using OsiguSDK.Core.Config;
using OsiguSDK.Providers.Clients;
using OsiguSDK.Providers.Models;

namespace OsiguSDKExamples
{
    class QueueProviderExample
    {
        private QueueClient _client;
        public QueueProviderExample(IConfiguration configuration)
        {
            _client = new QueueClient(configuration);
        }

        public QueueStatus CheckQueueStatus(string queueId)
        {
            return _client.CheckQueueStatus(queueId);
        }
    }
}
