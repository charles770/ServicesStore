using Microsoft.Extensions.Logging;
using ServicesStore.Api.Basket.RemoteInterface;
using ServicesStore.Api.Basket.RemoteModel;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Ubiety.Dns.Core;

namespace ServicesStore.Api.Basket.RemoteServices
{
    public class BooksService : IBooksService
    {
        private readonly IHttpClientFactory _httpClient;
        private readonly ILogger<BooksService> _looger;

        public BooksService(IHttpClientFactory httpClient, ILogger<BooksService> looger)
        {
            _httpClient = httpClient;
            _looger = looger;
        }

        public async Task<(bool res, RemoteBook book, string error)> GetBook(Guid bookId)
        {
            try
            {
                var client = _httpClient.CreateClient("Books");
                var response = await client.GetAsync($"api/bookitem/{bookId}");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    var result = JsonSerializer.Deserialize<RemoteBook>(content, options);
                    return (true, result, null);
                }

                return (false, null, response.ReasonPhrase);

            }
            catch (Exception e)
            {
                _looger?.LogError(e.ToString());
                return (false, null, e.Message);
            }
        }
    }
}
