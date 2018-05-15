using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using UnifiApiDemo.Business.Model;

namespace UnifiApiDemo.Business
{
    public class SampleResultsApi
    {
        public async Task<SampleResult> GetSampleResult(Guid resultId)
        {
            if (resultId == Guid.Empty)
            {
                throw new ArgumentException("The folder ID is required!", nameof(resultId));
            }

            ApiUtil api = new ApiUtil();
            var url = api.GetEndpointUrl("sampleresults", resultId);
            HttpClient httpClient = await api.GetAuthorizedClient();
            httpClient.DefaultRequestHeaders.Remove("Accept");
            httpClient.DefaultRequestHeaders.Add("Accept", "application/json;odata.metadata=full");

            var response = await httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.StatusCode);
                return null;
            }

            var json = await response.Content.ReadAsStringAsync();

            var sampleResult = api.Deserialize<SampleResult>(json);
            return sampleResult;
        }
    }
}
