using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ACC.ViewModel.Services
{
    public class ApiReceiver : IApiReceiver
    {

        private static HttpClient _client = new();

        public static async Task<T> ReadData<T>(string Endpoint)
        {
            T Response = default;

            _client.BaseAddress = new Uri(Endpoint);
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            
            HttpResponseMessage response = await _client.GetAsync(Endpoint);
            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                Response = JsonSerializer.Deserialize<T>(data);
            }

            return Response;
        }
    }
}
