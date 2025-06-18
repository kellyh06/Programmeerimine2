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
        }

        public async Task<Result<List<Artist>>> List()
        {
            var result = new Result<List<Artist>>();

            try
            {
                var data = await _httpClient.GetFromJsonAsync<List<Artist>>("Artists");
                result.Value = data ?? new List<Artist>();
            }
            catch (Exception ex)
            {
                result.AddError("", ex.Message);
            }

            return result;
        }


        public async Task<Result> Save(Artist artist)
        {
            var result = new Result();

            try
            {
                if (artist.Id == 0)
                {
                    var response = await _httpClient.PostAsJsonAsync("Artists", artist);
                    if (!response.IsSuccessStatusCode)
                    {
                        result.AddError("Server", "Failed to create artist");
                    }
                }
                else
                {
                    var response = await _httpClient.PutAsJsonAsync($"Artists/{artist.Id}", artist);
                    if (!response.IsSuccessStatusCode)
                    {
                        result.AddError("Server", "Failed to update artist");
                    }
                }
            }
            catch (Exception ex)
            {
                result.AddError("Exception", ex.Message);
            }

            return result;
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
