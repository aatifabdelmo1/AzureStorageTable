using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ConsumeWebApi1
{
    class Program
    {


        static string clientId = "7451a405-08c6-49d6-bf6d-0d4a2afb9971";
        static string clientKey = "3O7y37fcnqD5Gp5DG52US6E-D.I~_9l0-D";
        static string resourceUrl = "https://webapi22.azurewebsites.net/api";


        static void Main(string[] args)
        {
            var user = new UserPasswordCredential("abdelmonem@cloudco142.onmicrosoft.com", "P@ssw0rd");

            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                var authContext = new Microsoft.IdentityModel.Clients.ActiveDirectory.AuthenticationContext("https://login.microsoftonline.com/99d12f02-6d56-4898-9a28-5cd8b3370f39");
                //AuthenticationResult authResult = authContext.AcquireTokenAsync(resource, clientId, user).GetAwaiter().GetResult();
                ClientCredential clientCredentials = new ClientCredential(clientId, clientKey);
                var authResult = authContext.AcquireTokenAsync(resourceUrl, clientCredentials).GetAwaiter().GetResult();

                string accesstoken = authResult.AccessToken;
                var request = new HttpRequestMessage(HttpMethod.Get, "https://webapi22.azurewebsites.net/api/test/1");
                var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accesstoken);
                var response = client.SendAsync(request).GetAwaiter().GetResult();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var json = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                    dynamic obj = JsonConvert.DeserializeObject(json);
                    string result = obj["Name"];
                }
            }
            
            catch (Exception e)
            {

                Console.WriteLine(e.Message.ToString());
                Console.ReadLine();
            }

        }
    }
}
