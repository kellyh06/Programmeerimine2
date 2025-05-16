using KooliProjekt.WpfApp.Api;
using System.Net.Http;
using System.Net.Http.Json;
using WpfApp1.Api;

namespace WpfApp1.Api
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

            return result;
        }

        public async Task Save(Artist list)
        {
            if (list.Id == 0)
            {
                await _httpClient.PostAsJsonAsync("Artist", list);
            }
            else
            {
                await _httpClient.PutAsJsonAsync("Artist/" + list.Id, list);
            }
        }

        public async Task Delete(int id)
        {
            await _httpClient.DeleteAsync("Artist/" + id);
        }
    }
}