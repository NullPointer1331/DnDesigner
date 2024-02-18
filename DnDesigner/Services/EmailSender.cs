using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace YourNamespace.Services
{
    public class EmailSender : IEmailSender
    {

        private readonly ILogger<EmailSender> _logger;
        private readonly IOptions<ElasticEmailOptions> _options;

        public EmailSender(IOptions<ElasticEmailOptions> options, ILogger<EmailSender> logger)
        {
            _options = options;
            _logger = logger;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string message)
        {
            if (string.IsNullOrEmpty(_options.Value.ApiKey))
            {
                throw new Exception("Null <link>ElasticEmail</link> API key");
            }

            await Execute(_options.Value.ApiKey, toEmail, subject, message);
        }

        public async Task Execute(string apiKey, string toEmail, string subject, string message)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.elasticemail.com");
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
                    _logger.LogInformation($"Email to {toEmail} queued successfully!");
                }
                else
                {
                    _logger.LogError($"Failed to send email to {toEmail}. Status code: {response.StatusCode}");
                }
            }
        }
    }

    public class ElasticEmailOptions
    {
        public string ApiKey { get; set; }
    }
}