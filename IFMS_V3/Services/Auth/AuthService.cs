using System.Net.Http.Json;
using IFMS_V3.Models.Auth.Login;
using IFMS_V3.Models.Auth;
using Microsoft.AspNetCore.Components.WebAssembly.Http;
using IFMS_V3.Models.Auth.ChangePassword;

namespace IFMS_V3.Services.Auth
{
    public class AuthService
    {

        private readonly HttpClient _httpClient;
        private readonly CsrfService _csrfService;

        public AuthService(HttpClient httpClient, CsrfService csrfService)
        {
            _httpClient = httpClient;
            _csrfService = csrfService; 
        }

        public async Task<ApiResponse<Dictionary<string, string[]>>> LoginAsync(LoginModel loginModel)
        {
            try
            {
                var requestMessage = new HttpRequestMessage(HttpMethod.Post, "api/Auth/login")
                {
                    Content = JsonContent.Create(loginModel)
                };
                requestMessage.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);

                var response = await _httpClient.SendAsync(requestMessage);

                var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<Dictionary<string, string[]>>>();

                return apiResponse ?? new ApiResponse<Dictionary<string, string[]>>
                {
                    Status = "FAILED",
                    Message = "No response from server.",
                    Data = null
                };
            }
            catch (HttpRequestException ex)
            {
                return new ApiResponse<Dictionary<string, string[]>>
                {
                    Status = "FAILED",
                    Message = $"Network error: {ex.Message}",
                    Data = null
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<Dictionary<string, string[]>>
                {
                    Status = "FAILED",
                    Message = $"Unexpected error: {ex.Message}",
                    Data = null
                };
            }
        }

        public async Task<bool> LogoutAsync()
        {
            try
            {
                await _csrfService.FetchCsrfTokenAsync();

                var requestMessage = new HttpRequestMessage(HttpMethod.Post, "api/Auth/logout");
                requestMessage.Headers.Add("X-CSRF-TOKEN", _csrfService.CsrfToken);
                requestMessage.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);

                var response = await _httpClient.SendAsync(requestMessage);
                response.EnsureSuccessStatusCode();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Logout failed: {ex.Message}");
                return false;
            }
        }

        public async Task<ApiResponse<string>> ChangePasswordAsync(ChangePasswordModel changePasswordModel)
        {
            try
            {
                await _csrfService.FetchCsrfTokenAsync();

                var requestMessage = new HttpRequestMessage(HttpMethod.Post, "api/Password/change-password")
                {
                    Content = JsonContent.Create(changePasswordModel)
                };
                requestMessage.Headers.Add("X-CSRF-TOKEN", _csrfService.CsrfToken);
                requestMessage.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);

                var response = await _httpClient.SendAsync(requestMessage);

                var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<string>>();

                return apiResponse ?? new ApiResponse<string>
                {
                    Status = "FAILED",
                    Message = "No response from server.",
                    Data = null
                };
            }
            catch (HttpRequestException ex)
            {
                return new ApiResponse<string>
                {
                    Status = "FAILED",
                    Message = $"Network error: {ex.Message}",
                    Data = null
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<string>
                {
                    Status = "FAILED",
                    Message = $"Unexpected error: {ex.Message}",
                    Data = null
                };
            }
        }


    }
}
