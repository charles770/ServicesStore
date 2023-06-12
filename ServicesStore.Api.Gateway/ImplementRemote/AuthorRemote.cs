using Microsoft.Extensions.Logging;
using ServicesStore.Api.Gateway.BookRemote;
using ServicesStore.Api.Gateway.InterfaceRemote;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System;

namespace ServicesStore.Api.Gateway.ImplementRemote
{
    public class AuthorRemote : IAuthorRemote
    {
        private readonly IHttpClientFactory _httpClient;
        private readonly ILogger<AuthorRemote> _logger;

        public AuthorRemote(IHttpClientFactory httpClient, ILogger<AuthorRemote> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<(bool result, AuthorModelRemote author, string errorMessage)> GetAuthor(Guid AuthorId)
        {
            try
            {
                var client = _httpClient.CreateClient("AuthorService");
                var response = await client.GetAsync($"Author/{AuthorId}");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                    var result = JsonSerializer.Deserialize<AuthorModelRemote>(content, options);
                    return (true, result, null);
                }

                return (false, null, response.ReasonPhrase);
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                return (false, null, e.Message);
            }

        }
    }
}
