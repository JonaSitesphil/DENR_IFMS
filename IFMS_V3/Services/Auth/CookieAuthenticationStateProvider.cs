using System.Net.Http.Json;
using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Http;

namespace IFMS_V3.Services.Auth
{
    public class CookieAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly HttpClient _httpClient;

        // Constructor injection of HttpClient
        public CookieAuthenticationStateProvider(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                // Create HttpRequestMessage for the /api/auth/userinfo endpoint
                var requestMessage = new HttpRequestMessage(HttpMethod.Get, "api/auth/userinfo");

                // CRITICAL LINE: Explicitly tell the browser to include credentials (cookies)
                // for this cross-origin request.
                requestMessage.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);

                // Send the request using SendAsync, then read the JSON content
                var response = await _httpClient.SendAsync(requestMessage);

                response.EnsureSuccessStatusCode(); // Throws HttpRequestException for non-2xx status codes

                var userResponse = await response.Content.ReadFromJsonAsync<UserInfoDto>();

                if (userResponse != null && userResponse.IsAuthenticated)
                {
                    var claims = userResponse.Claims.Select(c => new Claim(c.Type, c.Value));
                    var identity = new ClaimsIdentity(claims, "Cookies");
                    var user = new ClaimsPrincipal(identity);
                    return new AuthenticationState(user);
                }
            }
            catch (HttpRequestException ex) // Catches 401 Unauthorized, 403 Forbidden, and network errors
            {
                // If the user is not authenticated, the backend's [Authorize] endpoint will likely return 401.
                // This is expected and means the user is not logged in.
                if (ex.StatusCode == System.Net.HttpStatusCode.Unauthorized || ex.StatusCode == System.Net.HttpStatusCode.Forbidden)
                {
                    Console.WriteLine("User not authenticated or forbidden (expected for unauthenticated user).");
                }
                else
                {
                    Console.WriteLine($"Network or API error during authentication state check: {ex.Message}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred during authentication state check: {ex.Message}");
            }

            // User is not authenticated or an error occurred
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }

        // Call this method when the user successfully logs in
        public void NotifyUserLoggedIn(ClaimsPrincipal principal)
        {
            var authState = Task.FromResult(new AuthenticationState(principal));
            NotifyAuthenticationStateChanged(authState);
        }

        // Call this method when the user logs out
        public void NotifyUserLoggedOut()
        {
            var authState = Task.FromResult(new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity())));
            NotifyAuthenticationStateChanged(authState);
        }
    }

    // DTO to match what your backend /api/auth/userinfo endpoint should return
    public class UserInfoDto
    {
        public bool IsAuthenticated { get; set; }
        public List<ClaimDto> Claims { get; set; } = new List<ClaimDto>();
    }

    public class ClaimDto
    {
        public string Type { get; set; }
        public string Value { get; set; }
    }
}
    
