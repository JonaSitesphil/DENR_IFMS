using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using IFMS_V3.Models.Burs;
using IFMS_V3.Models.Auth;
using IFMS_V3.Services.Auth;
using IFMS_V3.Dto.Burs; // ✅ include DTO namespace
using Microsoft.AspNetCore.Components.WebAssembly.Http;

namespace IFMS_V3.Services.Burs
{
    public class BursService
    {
        private readonly HttpClient _httpClient;
        private readonly CsrfService _csrfService;

        public BursService(HttpClient httpClient, CsrfService csrfService)
        {
            _httpClient = httpClient;
            _csrfService = csrfService;
        }

        private async Task<List<T>> GetDropdownData<T>(string endpoint)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, endpoint);
                request.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);

                var response = await _httpClient.SendAsync(request);
                response.EnsureSuccessStatusCode();
                var json = await response.Content.ReadAsStringAsync();

                using var doc = JsonDocument.Parse(json);

                if (doc.RootElement.TryGetProperty("data", out var dataProp) &&
                    dataProp.TryGetProperty("dropdownItems", out var dropdownItems))
                {
                    return JsonSerializer.Deserialize<List<T>>(dropdownItems.GetRawText()) ?? new List<T>();
                }

                return new List<T>();
            }
            catch
            {
                return new List<T>();
            }
        }

        public Task<List<ResponsibilityCenter>> GetResponsibilityCenters(int page = 1, int pageSize = 10)
            => GetDropdownData<ResponsibilityCenter>($"api/responsibilitycenter/dropdown?page={page}&pageSize={pageSize}");

        public Task<List<MFOPAP>> GetMFOPAPs(int page = 1, int pageSize = 10)
            => GetDropdownData<MFOPAP>($"api/mfopap/dropdown?page={page}&pageSize={pageSize}");

        public Task<List<UACS>> GetUACS(int page = 1, int pageSize = 10)
            => GetDropdownData<UACS>($"api/uacs/dropdown?page={page}&pageSize={pageSize}");

        // ✅ FIXED: use AddBursDto instead of BursModel
        public async Task<ApiResponse<AddBursDto>> CreateBursAsync(BursModel burs)
        {
            await _csrfService.FetchCsrfTokenAsync();

            // ✅ Map model -> dto
            var bursDto = new AddBursDto
            {
                BursNumber = burs.BursNumber,
                BursDate = burs.BursDate,
                Fund = burs.Fund,
                Payee = burs.Payee,
                Office = burs.Office,
                Address = burs.Address,
                Particulars = burs.Particulars,
                ResponsibilityCenterId = burs.ResponsibilityCenterId,
                MfoPapId = burs.MfoPapId,
                UacsCodeId = burs.UacsCodeId,
                Amount = burs.Amount,
                SignatoryA = burs.SignatoryA,
                SignatoryB = burs.SignatoryB,
                PositionA = burs.PositionA,
                PositionB = burs.PositionB
            };

            var request = new HttpRequestMessage(HttpMethod.Post, "api/Burs")
            {
                Content = JsonContent.Create(bursDto)
            };

            request.Headers.Add("X-CSRF-TOKEN", _csrfService.CsrfToken);
            request.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);

            var response = await _httpClient.SendAsync(request);
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"API error: {content}");
                return new ApiResponse<AddBursDto>
                {
                    Status = "FAILED",
                    Message = content,
                    Data = null
                };
            }

            var options = new JsonSerializerOptions
            {
                Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) },
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            try
            {
                var result = JsonSerializer.Deserialize<ApiResponse<AddBursDto>>(content, options);
                return result ?? new ApiResponse<AddBursDto>
                {
                    Status = "FAILED",
                    Message = "Deserialization returned null",
                    Data = null
                };
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"Deserialization error: {ex.Message}");
                return new ApiResponse<AddBursDto>
                {
                    Status = "FAILED",
                    Message = "Deserialization error: " + ex.Message,
                    Data = null
                };
            }
        }
    }
}
