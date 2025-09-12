using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using IFMS_V3.Dto.Norsa;
using IFMS_V3.Models.Auth;
using IFMS_V3.Models.Norsa;
using IFMS_V3.Models.Ors;
using IFMS_V3.Services.Auth;
using Microsoft.AspNetCore.Components.WebAssembly.Http;

namespace IFMS_V3.Services.Norsa
{
    public class NorsaService
    {
        private readonly HttpClient _httpClient;
        private readonly CsrfService _csrfService;

        public NorsaService(HttpClient httpClient, CsrfService csrfService)
        {
            _httpClient = httpClient;
            _csrfService = csrfService;
        }

        // Generic dropdown fetching
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

        public Task<List<FundCode>> GetFundCodes(int page = 1, int pageSize = 10)
            => GetDropdownData<FundCode>($"api/fundcode/dropdown?page={page}&pageSize={pageSize}");

        public Task<List<ResponsibilityCenter>> GetResponsibilityCenters(int page = 1, int pageSize = 10)
            => GetDropdownData<ResponsibilityCenter>($"api/responsibilitycenter/dropdown?page={page}&pageSize={pageSize}");

        public Task<List<MFOPAP>> GetMFOPAPs(int page = 1, int pageSize = 10)
            => GetDropdownData<MFOPAP>($"api/mfopap/dropdown?page={page}&pageSize={pageSize}");

        public Task<List<UACS>> GetUACS(int page = 1, int pageSize = 10)
            => GetDropdownData<UACS>($"api/uacs/dropdown?page={page}&pageSize={pageSize}");

        public async Task<ApiResponse<NorsaModel>> CreateNorsaAsync(NorsaModel norsa)
        {
            await _csrfService.FetchCsrfTokenAsync();

            // ✅ Map NorsaModel to AddNorsaDto
            var Norsa = new AddNorsaDto
            {
                ResponsibilityCenterId = norsa.ResponsibilityCenterId,
                Particulars = norsa.Particulars,
                MfoPapId = norsa.MfoPapId,
                UacsCodeId = norsa.UacsCodeId,
                Amount = norsa.Amount,
                JevNumber = norsa.JevNumber,
                JevDate = norsa.JevDate,
                SerialNo = norsa.SerialNo
            };

            var request = new HttpRequestMessage(HttpMethod.Post, "api/Norsa")
            {
                Content = JsonContent.Create(Norsa, options: new JsonSerializerOptions
                {
                    PropertyNamingPolicy = null, // 👈 keep PascalCase
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
                })
            };

            request.Headers.Add("X-CSRF-TOKEN", _csrfService.CsrfToken);
            request.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);

            var response = await _httpClient.SendAsync(request);
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                return new ApiResponse<NorsaModel>
                {
                    Status = "FAILED",
                    Message = content,
                    Data = null
                };
            }

            try
            {
                var options = new JsonSerializerOptions
                {
                    Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) },
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };

                var result = JsonSerializer.Deserialize<ApiResponse<NorsaModel>>(content, options);
                return result ?? new ApiResponse<NorsaModel>
                {
                    Status = "FAILED",
                    Message = "Deserialization returned null",
                    Data = null
                };
            }
            catch (JsonException ex)
            {
                return new ApiResponse<NorsaModel>
                {
                    Status = "FAILED",
                    Message = "Deserialization error: " + ex.Message,
                    Data = null
                };
            }
        }

    }
}
