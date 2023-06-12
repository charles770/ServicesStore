using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Threading;
using System;
using ServicesStore.Api.Gateway.BookRemote;
using ServicesStore.Api.Gateway.InterfaceRemote;

namespace ServicesStore.Api.Gateway.MessageHandler
{
    public class BookHandler : DelegatingHandler
    {
        private readonly ILogger<BookHandler> _logger;

        private readonly IAuthorRemote _authorRemote;

        public BookHandler(ILogger<BookHandler> logger, IAuthorRemote autorRemote)
        {
            _logger = logger;
            _authorRemote = autorRemote;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {

            var time = Stopwatch.StartNew();
            _logger.LogInformation("Request start");
            var response = await base.SendAsync(request, cancellationToken);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var result = JsonSerializer.Deserialize<BookModelRemote>(content, options);
                var responseAuthor = await _authorRemote.GetAuthor(result.AuthorBook ?? Guid.Empty);
                if (responseAuthor.result)
                {
                    var objectAutor = responseAuthor.author;
                    result.AuthorData = objectAutor;
                    var resultStr = JsonSerializer.Serialize(result);
                    response.Content = new StringContent(resultStr, System.Text.Encoding.UTF8, "application/json");
                }
            }

            _logger.LogInformation($"The process takes {time.ElapsedMilliseconds}ms");

            return response;
        }

    }
}
