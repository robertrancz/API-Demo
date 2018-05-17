using IdentityModel.Client;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using UnifiApiDemo.Business.Model;

namespace UnifiApiDemo.Business
{
    public class FoldersApiClient
    {
        public async Task<List<Folder>> GetFolders()
        {
            ApiUtil api = new ApiUtil();
            var url = api.GetEndpointUrl("folders");
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

            var foldersList = api.Deserialize<List<Folder>>(json);
            return foldersList;
        }

        public async Task<List<Item>> GetItems(Guid folderId)
        {
            if(folderId == Guid.Empty)
            {
                throw new ArgumentException("The folder ID is required!", nameof(folderId));
            }

            ApiUtil api = new ApiUtil();
            var url = api.GetEndpointUrl("folders", folderId, "items");

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

            var itemsList = api.Deserialize<List<Item>>(json);
            return itemsList;
        }
    }
}
