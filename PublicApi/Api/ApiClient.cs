
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

        // HttpClient tuleb DI kaudu Program.cs failist
        public ApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:7136/api/");
        }

        public async Task<Result<List<Artist>>> List()
        {
            var result = new Result<List<Artist>>();

            try
            {
                result.Value = await _httpClient.GetFromJsonAsync<List<Artist>>("Artists");
            }
            catch (Exception ex)
            {
                result.AddError("_", ex.Message);
            }

            return result;
        }


        public async Task<Result> Save(Artist artist)
        {
            HttpResponseMessage response;

            if (artist.Id == 0)
            {
                response = await _httpClient.PostAsJsonAsync("Artists", artist);
            }
            else
            {
                response = await _httpClient.PutAsJsonAsync("Artists/" + artist.Id, artist);
            }

            if (!response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<Result>();
                return result;
            }

            return new Result();
        }

        public async Task<Result> Delete(int id)
        {
            var result = new Result();

            try
            {
                var response = await _httpClient.DeleteAsync($"Artists/{id}");
                if (!response.IsSuccessStatusCode)
                {
                    result.AddError("Server", $"Failed to delete artist with ID {id}");
                }
            }
            catch (Exception ex)
            {
                result.AddError("Exception", ex.Message);
            }

            return result;
        }
    }
}
