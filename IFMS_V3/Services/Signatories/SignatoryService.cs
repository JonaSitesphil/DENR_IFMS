using System.Net.Http;
using System.Net.Http.Json;
using IFMS_V3.Dto.Signatories;
using IFMS_V3.Dto.UpdateSignatories;
using IFMS_V3.Models.Auth;
using IFMS_V3.Models.User.Signatories;
using IFMS_V3.Services.Auth;
using IFMS_V3.Dto.UpdateSignatories;
using Microsoft.AspNetCore.Components.WebAssembly.Http;

namespace IFMS_V3.Services.Signatories
{
    public class SignatoryService
    {
          private readonly HttpClient _httpClient;
        private readonly CsrfService _csrfService;

        public SignatoryService(HttpClient httpClient, CsrfService csrfService)
        {
            _httpClient = httpClient;
            _csrfService = csrfService;
        }

        public async Task<SignatoriesApiResponse?> GetSignatoriesAsync(int page, int pageSize)
        {
            try
            {
                await _csrfService.FetchCsrfTokenAsync();

                var requestMessage = new HttpRequestMessage(HttpMethod.Get, $"api/Signatories?page={page}&pageSize={pageSize}");
                requestMessage.Headers.Add("X-CSRF-TOKEN", _csrfService.CsrfToken);
                requestMessage.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);

                var response = await _httpClient.SendAsync(requestMessage);
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadFromJsonAsync<SignatoriesApiResponse>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error in GetSignatoriesAsync: {ex.Message}");
                return null;
            }
        }
        

        public async Task<ApiResponse<object>> CreateSignatory(AddSignatoriesDto newSignatory)
        {
            try
            {
                await _csrfService.FetchCsrfTokenAsync();

                var requestMessage = new HttpRequestMessage(HttpMethod.Post, "api/Signatories")
                {
                    Content = JsonContent.Create(newSignatory)
                };

                requestMessage.Headers.Add("X-CSRF-TOKEN", _csrfService.CsrfToken);
                requestMessage.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);

                var response = await _httpClient.SendAsync(requestMessage);
                var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<object>>();

                return apiResponse ?? new ApiResponse<object>
                {
                    Status = "FAILED",
                    Message = "No response from server.",
                    Data = null
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<object>
                {
                    Status = "FAILED",
                    Message = $"Unexpected error: {ex.Message}",
                    Data = null
                };
            }
        }

        public async Task<ApiResponse<object>> UpdateSignatoryAsync(int signatoryId, UpdateSignatoriesDto dto)
        {
            try
            {
                await _csrfService.FetchCsrfTokenAsync();

                var requestMessage = new HttpRequestMessage(HttpMethod.Put, $"api/Signatories/{signatoryId}")
                {
                    Content = JsonContent.Create(dto)
                };

                requestMessage.Headers.Add("X-CSRF-TOKEN", _csrfService.CsrfToken);
                requestMessage.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);

                var response = await _httpClient.SendAsync(requestMessage);
                response.EnsureSuccessStatusCode();

                var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<object>>();
                return apiResponse ?? new ApiResponse<object>
                {
                    Status = "FAILED",
                    Message = "No response from server.",
                    Data = null
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<object>
                {
                    Status = "FAILED",
                    Message = $"Unexpected error: {ex.Message}",
                    Data = null
                };
            }
        }

        public async Task UpdateUserStatusAsync(int userId, bool disable)
        {
            // Choose API endpoint based on disable flag
            string action = disable ? "disable" : "enable";

            // Build PUT request to update user status
            var request = new HttpRequestMessage(HttpMethod.Put, $"/api/Signatories/{action}/{userId}");
            request.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);

            // Send request
            var response = await _httpClient.SendAsync(request);

            // Throw error if failed
            if (!response.IsSuccessStatusCode)
                throw new HttpRequestException($"Failed to update user status: {response.StatusCode}");
        }
    }
}
