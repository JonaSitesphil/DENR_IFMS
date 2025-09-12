using System.Net.Http.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Components.WebAssembly.Http;
using Microsoft.JSInterop;

namespace IFMS_V3.Services.Auth
{
    public class CsrfService
    {

        public string? CsrfToken { get; private set; }

        private readonly HttpClient _httpClient;
        private readonly IJSRuntime _jsRuntime;

        public CsrfService(HttpClient httpClient, IJSRuntime jsRuntime)
        {
            _httpClient = httpClient;
            _jsRuntime = jsRuntime;
        }

        public async Task FetchCsrfTokenAsync()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "api/auth/csrf-token");
            request.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);

            var response = await _httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var tokenResponse = await response.Content.ReadFromJsonAsync<CsrfTokenResponse>();
                CsrfToken = tokenResponse?.CsrfToken;

                if (!string.IsNullOrEmpty(CsrfToken))
                {
                    // Optional: store in sessionStorage
                    await _jsRuntime.InvokeVoidAsync("sessionStorage.setItem", "CSRF_TOKEN", CsrfToken);
                    Console.WriteLine($"CSRF Token fetched: {CsrfToken}");
                }
            }
            else
            {
                Console.WriteLine("Failed to fetch CSRF token");
            }
        }

        public class CsrfTokenResponse
        {
            [JsonPropertyName("csrfToken")]
            public string? CsrfToken { get; set; }
        }
    }
}
