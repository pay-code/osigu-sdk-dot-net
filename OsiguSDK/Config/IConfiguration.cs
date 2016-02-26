namespace OsiguSDK.Core.Config
{
    public interface IConfiguration
    {
        /// <summary>
        /// Object containing Authentication data </summary>
        /// <returns> Authentication </returns>
        Authentication.Authentication Authentication { get; set; }

        /// <summary>
        /// Base URL containing host name and port of the API server </summary>
        /// <returns> messagingBaseUrl </returns>
        string BaseUrl { get; set; }

        string Slug { get; set; }

    }
}