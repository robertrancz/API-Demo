using IdentityModel.Client;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace UnifiApiDemo.Business
{
    public class ApiUtil
    {
        public string Host { get; set; } = "unifiapi.waters.com";
        public int ApiPort { get; set; } = 50034;        
        public string Protocol { get; set; } = "https";
        public string ApiBasePath { get; set; } = "/unifi/v1";

        public int IdentityServerPort { get; set; } = 50333;
        public string IdentityServerTokenAddress { get; set; } = "/identity/connect/token";


        //private HttpClient

        public string GetEndpointUrl(params object[] values)
        {
            Uri uri = new UriBuilder(Protocol, Host, ApiPort, ApiBasePath).Uri; 
            string url = uri.ToString();

            if (values.Length > 0)
                url += string.Format("/{0}", values[0]);

            for (int i = 1; i < values.Length; i++)
                url += string.Format("/{0}", values[i]);

            return url;
        }

        public async Task<HttpClient> GetAuthorizedClient(string identityServerTokenAddress = "", string apiBaseAddress = "")
        {
            if(string.IsNullOrWhiteSpace(identityServerTokenAddress))
            {
                identityServerTokenAddress = new UriBuilder(Protocol, Host, IdentityServerPort, IdentityServerTokenAddress).Uri.ToString();
            }
            if (string.IsNullOrWhiteSpace(apiBaseAddress))
            {
                apiBaseAddress = new UriBuilder(Protocol, Host, ApiPort, ApiBasePath).Uri.ToString();
            }

            //--- Authorization: create a TokenClient instance
            TokenClient tokenClient = new TokenClient(identityServerTokenAddress, "resourceownerclient", "secret");
            TokenResponse token = await tokenClient.RequestResourceOwnerPasswordAsync("administrator", "administrator42", "unifi");

            //--- Create HttpClient
            Console.WriteLine("Create an HTTPClient instance \n");
            HttpClient client = new HttpClient { BaseAddress = new Uri(apiBaseAddress) };
            Console.WriteLine($"HTTPClient base adress: {client.BaseAddress.ToString()} \n");

            //--- Set the access token as bearer token to give access to the current user
            client.SetBearerToken(token.AccessToken);

            Console.WriteLine($"Authorization Header:  {client.DefaultRequestHeaders.Authorization} \n");

            return client;
        }

        public T Deserialize<T>(string json)
        {
            string text = json;

            //--- if the response contains an array of objects, look for the "value" property
            JObject jObject = JObject.Parse(json);
            if (jObject != null)
            {
                JToken value = jObject["value"];
                if (value != null && value is JArray)
                    text = value.ToString();
            }

            //--- now we can use JsonConvert to deserialize the string
            return JsonConvert.DeserializeObject<T>(text);
        }
    }
}
