using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Net;
using System.Text;
using ElasticEmail.Api;
using ElasticEmail.Client;
using ElasticEmail.Model;

namespace DnDesigner
{
    public class EmailsPostExample
    {
        public static void Test2()
        {
            NameValueCollection values = new NameValueCollection();
            values.Add("apikey", "API KEY HERE");
            values.Add("from", "sender email here");
            values.Add("fromName", "Your Company Name");
            values.Add("to", "recipient email here");
            values.Add("subject", "DnDsigner Account Confirmation");
            values.Add("bodyText", "This confirms your email address and account for DnDsigner!");
            values.Add("bodyHtml", "<h1>Html Body</h1>");
            values.Add("isTransactional", "true");

            string address = "https://api.elasticemail.com/v2/email/send";

            string response = Send(address, values);

            Console.WriteLine(response);
        }

        static string Send(string address, NameValueCollection values)
        {
            using (WebClient client = new WebClient())
            {
                try
                {
                    byte[] apiResponse = client.UploadValues(address, values);
                    return Encoding.UTF8.GetString(apiResponse);

                }
                catch (Exception ex)
                {
                    return "Exception caught: " + ex.Message + "\n" + ex.StackTrace;
                }
            }
        }
    }
}