using ACCDataModel.Stammdaten;
using Microsoft.AspNetCore.Authorization;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Microsoft.Identity.Web;
using ACCDataModel.DTO;

namespace ACCWebUI.Services
{
    [Authorize]
    public class OrderService
    {
        private readonly HttpClient _httpClient;
        private readonly ITokenAcquisition _tokenAcquisition;

        public OrderService(HttpClient httpClient, ITokenAcquisition tokenAcquisition)
        {
            _httpClient = httpClient;
            _tokenAcquisition = tokenAcquisition;
        }

        public async Task<string> AcquireToken()
        {
            string[] scopes = { "api://02513cef-ceba-453b-89c2-7cb1783c416a/User.Read" };
            var authResult = await _tokenAcquisition.GetAuthenticationResultForUserAsync(scopes);

            return authResult.AccessToken;
        }

        public async Task<OrderResponseDTO> GetOrdersAsync(int currentPage, int pageSize)
        {

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await AcquireToken());

            HttpResponseMessage response = await _httpClient.GetAsync($"http://localhost:32771/orders?currentPage={currentPage}&pageSize={pageSize}");
            if (response.IsSuccessStatusCode)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                OrderResponseDTO auftragsListe = JsonConvert.DeserializeObject<OrderResponseDTO>(responseBody);

                return auftragsListe;
            }
            else
            {
                return null;
            }
        }

        public async Task<int> GetOrdersCountAsync()
        {

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await AcquireToken());

            HttpResponseMessage response = await _httpClient.GetAsync($"http://localhost:32771/orders/count");
            if (response.IsSuccessStatusCode)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                int count = JsonConvert.DeserializeObject<int>(responseBody);
                return count;
            }
            else
            {
                return 0;
            }
        }

        public Task<Auftrag> GetOrderAsync(int auftragID)
        {
            return null;
        }

        public Task<Auftrag> CreateOrderAsync(Auftrag auftrag)
        {
            return null;
        }

        public Task<Auftrag> UpdateOrderAsync(int auftragID, Auftrag auftrag)
        {
            return null;
        }

        public Task<Auftrag> DeleteOrderAsync(int auftragID)
        {
            return null;
        }
    }
}
