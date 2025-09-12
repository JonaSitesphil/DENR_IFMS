using System.Net.Http.Json;
using System.Text.Json;
using IFMS_V3.Dto.Add;
using IFMS_V3.Dto.Update;
using IFMS_V3.Models.Auth;
using IFMS_V3.Models.User.Roles;
using IFMS_V3.Models.User.UserManagement;
using IFMS_V3.Services.Auth;
using Microsoft.AspNetCore.Components.WebAssembly.Http;
        

namespace IFMS_V3.Services.User
{
    public class UserService
    {
        private readonly HttpClient _httpClient;   
        private readonly CsrfService _csrfService; 

        public UserService(HttpClient httpClient, CsrfService csrfService)
        {
            _httpClient = httpClient;
            _csrfService = csrfService;
        }

        // Method: Create a new user (POST request)
        public async Task<ApiResponse<object>> CreateUser(AddUserDto newUser)
        {
            try
            {
                await _csrfService.FetchCsrfTokenAsync();

                var requestMessage = new HttpRequestMessage(HttpMethod.Post, "api/User")
                {
                    Content = JsonContent.Create(newUser)
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


        public async Task<List<RoleModel>> GetRolesAsync()
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, "api/role/dropdown");
                request.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);

                
                var response = await _httpClient.SendAsync(request);
                response.EnsureSuccessStatusCode(); 

                
                var json = await response.Content.ReadAsStringAsync();

                using var doc = JsonDocument.Parse(json);
                var dataProperty = doc.RootElement.GetProperty("data");

                
                var roles = JsonSerializer.Deserialize<List<RoleModel>>(dataProperty.GetRawText());

                return roles ?? new List<RoleModel>(); 
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching roles: {ex.Message}");
                return new List<RoleModel>(); 
            }
        }

        public async Task<UsermanagementApiResponse> GetUsermanagementDataAsync(int page = 1, int pageSize = 10)
        {
            try
            {

                var url = $"/api/User?page={page}&pageSize={pageSize}";

                var request = new HttpRequestMessage(HttpMethod.Get, url);
                request.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);

                var response = await _httpClient.SendAsync(request);

                
                if (!response.IsSuccessStatusCode)
                    throw new HttpRequestException($"Request failed with status {response.StatusCode}");

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                var result = await response.Content.ReadFromJsonAsync<UsermanagementApiResponse>(options)
                             ?? throw new Exception("No data returned from API.");

                return result; 
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to load user management data", ex);
            }
        }

        
        public async Task UpdateUserStatusAsync(int userId, bool disable)
        {
            
            string action = disable ? "disable" : "enable";

            
            var request = new HttpRequestMessage(HttpMethod.Put, $"/api/User/{action}/{userId}");
            request.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);

            
            var response = await _httpClient.SendAsync(request);

            
            if (!response.IsSuccessStatusCode)
                throw new HttpRequestException($"Failed to update user status: {response.StatusCode}");
        }

        
        public async Task<List<UserModel>> GetUsersAsync()
        {
            
            var response = await _httpClient.GetAsync("/api/User");
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<List<UserModel>>() ?? new List<UserModel>();
        }

        public async Task UpdateUserAsync(int userId, UpdateUserDto dto)
        {
            
            var request = new HttpRequestMessage(HttpMethod.Put, $"/api/User/{userId}")
            {
                Content = JsonContent.Create(dto) 
            };

            
            request.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);

            
            var response = await _httpClient.SendAsync(request);

            if (!response.IsSuccessStatusCode)
                throw new HttpRequestException($"Failed to update user: {response.StatusCode}");
        }
    }
}
