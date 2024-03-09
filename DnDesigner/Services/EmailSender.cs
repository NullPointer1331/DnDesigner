using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity.UI.Services;
using ElasticEmailClient;

namespace YourNamespace.Services
{
    public static class EmailSender
    {
        private static readonly ElasticEmailOptions _options;

        static EmailSender()
        {
            _options = new ElasticEmailOptions("");
        }

        public static async Task SendEmailAsync(string toEmail, string subject, string message)
        {
            if (string.IsNullOrEmpty(_options.ApiKey))
            {
                throw new Exception("Null ElasticEmail API key");
            }

            await Execute(_options.ApiKey, toEmail, subject, message);
        }

        private static async Task Execute(string apiKey, string toEmail, string subject, string message)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.elasticemail.com/v2/email/send?");
                var content = new FormUrlEncodedContent(new[]
                {
                        new KeyValuePair<string, string>("apikey", apiKey),
                        new KeyValuePair<string, string>("to", toEmail),
                        new KeyValuePair<string, string>("subject", subject),
                        new KeyValuePair<string, string>("body", message)
                    });

                var response = await client.PostAsync("/v2/email/send", content);
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Email sent to {toEmail}");
                }
                else
                {
                    Console.WriteLine($"Failed to send email to {toEmail}");
                }
            }
        }
    }

    public class ElasticEmailOptions
    {
        public ElasticEmailOptions(string apiKey)
        {
            ApiKey = apiKey;
        }
        public string ApiKey { get; set; }
    }
}