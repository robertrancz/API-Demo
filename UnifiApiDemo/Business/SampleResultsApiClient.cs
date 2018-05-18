using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

using UnifiApiDemo.Business.Model;
using UnifiApiDemo.Business.Model.Chromatograms;
using UnifiApiDemo.Business.Model.Components;
using UnifiApiDemo.Business.Model.Spectra;

namespace UnifiApiDemo.Business
{
    public class SampleResultsApiClient
    {
        public async Task<SampleResult> GetSampleResult(Guid resultId)
        {
            if (resultId == Guid.Empty)
            {
                throw new ArgumentException("The result ID parameter is required!", nameof(resultId));
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

        public async Task<List<Component>> GetIdentifiedComponents(Guid resultId)
        {
            if (resultId == Guid.Empty)
            {
                throw new ArgumentException("The result ID parameter is required!", nameof(resultId));
            }

            ApiUtil api = new ApiUtil();
            var url = api.GetEndpointUrl("sampleresults", resultId, "components.identified");
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

            var components = api.Deserialize<List<Component>>(json);
            return components;
        }

        public async Task<int> GetAccurateMseSpectraCount(Guid resultId)
        {
            if (resultId == Guid.Empty)
            {
                throw new ArgumentException("The result ID parameter is required!", nameof(resultId));
            }

            ApiUtil api = new ApiUtil();
            var url = api.GetEndpointUrl("sampleresults", resultId, "spectra", "mass.mse", "$count");
            HttpClient httpClient = await api.GetAuthorizedClient();
            httpClient.DefaultRequestHeaders.Remove("Accept");
            httpClient.DefaultRequestHeaders.Add("Accept", "text/plain");

            var response = await httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.StatusCode);
                return -1;
            }

            var responseText = await response.Content.ReadAsStringAsync();
            Int32.TryParse(responseText, out var spectraCount);

            return spectraCount;
        }

        public async Task<List<SpectrumInfo>> GetSpectrumInfo(Guid resultId)
        {
            if (resultId == Guid.Empty)
            {
                throw new ArgumentException("The result ID parameter is required!", nameof(resultId));
            }

            ApiUtil api = new ApiUtil();
            var url = api.GetEndpointUrl("sampleresults", resultId, "spectruminfos");
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

            var spectrumInfos = api.Deserialize<List<SpectrumInfo>>(json);
            return spectrumInfos;
        }

        public async Task<List<ChromatogramInfo>> GetChromatogramInfo(Guid resultId)
        {
            if (resultId == Guid.Empty)
            {
                throw new ArgumentException("The result ID parameter is required!", nameof(resultId));
            }

            ApiUtil api = new ApiUtil();
            var url = api.GetEndpointUrl("sampleresults", resultId, "chromatograminfos");
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

            var chromatogramInfos = api.Deserialize<List<ChromatogramInfo>>(json);
            return chromatogramInfos;
        }
    }
}
