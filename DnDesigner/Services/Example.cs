using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using ElasticEmail.Api;
using ElasticEmail.Client;
using ElasticEmail.Model;
namespace Example
{
    public class Example
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.elasticemail.com/v4";
            // Configure API key authorization: apikey
            config.ApiKey.Add("X-ElasticEmail-ApiKey", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // config.ApiKeyPrefix.Add("X-ElasticEmail-ApiKey", "Bearer");
            var apiInstance = new CampaignsApi(config);
            var name = "name_example";  // string | Name of Campaign to delete
            try
            {
                // Delete Campaign
                apiInstance.CampaignsByNameDelete(name);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling CampaignsApi.CampaignsByNameDelete: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
