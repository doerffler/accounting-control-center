using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DoePaAdmin.ViewModel.Services
{
    public class ApiReciever<T> : IApiReciever<T>
    {
        public string Endpoint { get; set; }
        public T Response { get; set; }
        public static HttpClient client = new();

        public ApiReciever(string Endpoint)
        {
            this.Endpoint = Endpoint;
        }

        public async Task<T> ReadData()
        {
            client.BaseAddress = new Uri(Endpoint);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            
            HttpResponseMessage response = await client.GetAsync(Endpoint);
            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                Response = JsonSerializer.Deserialize<T>(data);
            }

            return Response;
        }
    }
}
