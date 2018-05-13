using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using UnifiApiDemo.Models;
using IdentityModel.Client;

namespace UnifiApiDemo.Business
{
    public static class SampleResultToMzMLConverter
    {
        static HttpClient client;
        static int fileNo = 0;

        public static async void ConvertToMzml(UnifiAPIViewModel apiModel)
        {
            string apiBaseAddress = apiModel.UnifiServerURI; //"https://unifiapi.waters.com:50034/unifi/v1/"; ;
            string idTokenAddress = "https://unifiapi.waters.com:50333/identity/connect/token";

            client = GetClient(idTokenAddress, apiBaseAddress).Result;
            if (client != null)
            {
                client.DefaultRequestHeaders.Remove("Accept");
                client.DefaultRequestHeaders.Add("Accept", "application/json;odata.metadata=full");

                JObject jsonResponse = await GetMseEndpointResponse(apiModel.UnifiServerURI);
            }
        }

        #region Get/Map/Parse response    
        private static async Task<JObject> GetMseEndpointResponse(string unifiServerURI)
        {
            string resultId = "3d81f45e-7f97-41ca-aa29-844280d015ee";
            string requestedURL = unifiServerURI + "sampleresults(" + resultId + ")";

            if (client.DefaultRequestHeaders.Accept.ToString().StartsWith("application/json") || requestedURL.IndexOf("$count") > 0)
            {
                Console.WriteLine("Get response for request: " + requestedURL);
                string responseBody = await getResponseAsString(requestedURL);
                if (requestedURL.IndexOf("$count") > 0)
                {
                    int numSpectra = Int32.Parse(responseBody);
                    Console.WriteLine("numSpectra = " + numSpectra);
                    //continue;
                }

                if (!responseBody.Equals(string.Empty))
                    return mapStringToJson(responseBody, requestedURL);
                else
                {
                    Console.WriteLine("Response string is empty!");
                }
            }
            return null;
        }

        private static async Task<String> getResponseAsString(string requestURL)
        {
            HttpResponseMessage responseMessage = await client.GetAsync(requestURL);
            if (!responseMessage.IsSuccessStatusCode)
            {
                Console.WriteLine(responseMessage.ToString());
                return string.Empty;
            }
            String responseBody = await responseMessage.Content.ReadAsStringAsync();

            return responseBody;
        }

        private static JObject mapStringToJson(string responseBody, string requestedURL)
        {
            //--- Get the results as array
            Console.WriteLine("Display results: \n");
            JObject response = JObject.Parse(responseBody);
            JArray values = response["value"] as JArray;

            fileNo++;
            var file = System.IO.File.Create(@"D:\file" + fileNo + ".txt");
            using (var writer = new System.IO.StreamWriter(file))
            {
                writer.WriteLine("RESPONSE for " + requestedURL);

                if (values != null && values.Count > 0)
                {
                    foreach (JObject responseValue in values)
                    {
                        foreach (JProperty prop in responseValue.Properties())
                        {
                            Console.WriteLine($"{prop.Name} = {prop.Value}");
                            writer.WriteLine($"{prop.Name} = {prop.Value}");
                        }
                        Console.WriteLine(Environment.NewLine);
                        writer.WriteLine(Environment.NewLine);
                    }
                }
                else
                {
                    JObject jObject = JObject.Parse(responseBody);
                    IEnumerator<KeyValuePair<string, JToken>> jsonEnum = jObject.GetEnumerator();

                    while (jsonEnum.MoveNext())
                    {
                        writer.WriteLine(jsonEnum.Current.Key + " = " + jsonEnum.Current.Value);
                        Console.WriteLine(Environment.NewLine);
                        writer.WriteLine(Environment.NewLine);
                    }
                    return jObject;
                    // Deserialize<List<object>>(responseBody);
                }
            }
            return response;
        }
        #endregion Get/Map/Parse response

        #region Authorization 
        private static async Task<HttpClient> GetClient(string idTokenAddress, string apiBaseAddress)
        {

            //--- Authorization: create a TokenClient instance
            TokenClient tokenClient = new TokenClient(idTokenAddress, "resourceownerclient", "secret");

            TokenResponse token;
            // if (!isDemoServer)
            //    token = await tokenClient.RequestResourceOwnerPasswordAsync("administrator", "administrator",
            //        "unifi");
            //else
            token = await tokenClient.RequestResourceOwnerPasswordAsync("administrator", "administrator42", "unifi");

            //--- Create HttpClient
            Console.WriteLine("Create an HTTPClient instance \n");
            HttpClient client = new HttpClient { BaseAddress = new Uri(apiBaseAddress) };
            Console.WriteLine($"HTTPClient base adress: {client.BaseAddress.ToString()} \n");

            //--- Set the access token as bearer token to give access to the current user
            client.SetBearerToken(token.AccessToken);

            Console.WriteLine($"Authorization Header:  {client.DefaultRequestHeaders.Authorization} \n");

            return client;
        }
        #endregion Authorization
    }
}
