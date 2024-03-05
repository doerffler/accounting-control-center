using ACCDataModel.Stammdaten;
using ACCWebUI.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Graph.ExternalConnectors;
using Microsoft.Identity.Web;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace ACCWebUI.Services
{
    [Authorize]
    public class ApiService
    {
        private readonly HttpClient _httpClient;
        private readonly ITokenAcquisition _tokenAcquisition;
        private readonly IConfiguration _configuration;
        private string _apiBaseUrl;
        private ResourceEndpoint _endpoint;

        public ApiService(HttpClient httpClient, ITokenAcquisition tokenAcquisition, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _tokenAcquisition = tokenAcquisition;
            _configuration = configuration;
        }

        public void InitializeEndpoint(ResourceEndpoint endpoint) 
        {
            string apiEndpoint = _configuration["AccApiEndpoint"].ToString();

            if (string.IsNullOrEmpty(apiEndpoint))
            {
                throw new Exception("Please set the API endpoint in your appsettings.json");
            }

            _apiBaseUrl = apiEndpoint;
            _endpoint = endpoint;
        }

        private async Task<string> AcquireToken()
        {
            string[] scopes = { "api://02513cef-ceba-453b-89c2-7cb1783c416a/User.Read" };
            var authResult = await _tokenAcquisition.GetAuthenticationResultForUserAsync(scopes);
            return authResult.AccessToken;
        }

        private async Task<T> SendRequestAsync<T>(HttpMethod method, string relativeUrl, HttpContent? payload = null)
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await AcquireToken());

                HttpResponseMessage response;

                switch (method.Method.ToUpper())
                {
                    case "GET":
                        response = await _httpClient.GetAsync(_apiBaseUrl + relativeUrl);
                        break;
                    case "POST":
                        response = await _httpClient.PostAsync(_apiBaseUrl + relativeUrl, payload);
                        break;
                    case "PUT":
                        response = await _httpClient.PutAsync(_apiBaseUrl + relativeUrl, payload);
                        break;
                    case "DELETE":
                        response = await _httpClient.DeleteAsync(_apiBaseUrl + relativeUrl); 
                        break;
                    default:
                        throw new ArgumentException("Invalid HTTP method specified.");
                }

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<T>(responseBody);
                }
                else
                {
                    return default;
                }
            } catch (JsonSerializationException)
            {
                return default;
            }
        }

        public async Task<T> GetAsync<T>(int? currentPage = 0, int? pageSize = 0)
        {
            return await SendRequestAsync<T>(HttpMethod.Get, $"{_endpoint}?currentPage={currentPage}&pageSize={pageSize}");
        }

        public async Task<T> GetByIdAsync<T>(int itemId)
        {
            return await SendRequestAsync<T>(HttpMethod.Get, $"{_endpoint}/{itemId}");
        }

        public async Task<T> CreateAsync<T>(T item)
        {
            var content = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json");
            return await SendRequestAsync<T>(HttpMethod.Post, $"{_endpoint}", content);
        }

        public async Task<T> UpdateAsync<T>(int itemId, T item)
        {
            var content = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json");
            return await SendRequestAsync<T>(HttpMethod.Put, $"{_endpoint}/{itemId}", content);
        }

        public async Task<T> DeleteAsync<T>(int itemId)
        {
            return await SendRequestAsync<T>(HttpMethod.Delete, $"{_endpoint}/{itemId}");
        }
    }
}
