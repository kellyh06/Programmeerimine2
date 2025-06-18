
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace KooliProjekt.PublicApi
{
    public class ApiClient : IApiClient
    {
        private readonly HttpClient _httpClient;

        public ApiClient()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7136/api/");
        }

        public async Task<List<Artist>> List()
        {
            var result = await _httpClient.GetFromJsonAsync<List<Artist>>("Artists");
            return result ?? new List<Artist>();
        }

        public async Task Save(Artist list)
        {
            if (list.Id == 0)
            {
                await _httpClient.PostAsJsonAsync("Artists", list);
            }
            else
            {
                var response = await _httpClient.PutAsJsonAsync("Artists/" + list.Id, list);
                if(!response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                }
            }
        }

        public async Task Delete(int id)
        {
            await _httpClient.DeleteAsync("Artists/" + id);
        }
    }
}