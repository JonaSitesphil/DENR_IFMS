using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using IFMS_V3.Dto.Burs;
using IFMS_V3.Dto.Ors;
using IFMS_V3.Models.Auth;
using IFMS_V3.Models.Ors;
using IFMS_V3.Models.Ors.AdminDashboardModel;
using IFMS_V3.Models.Ors.SectionChiefModel;
using IFMS_V3.Services.Auth;
using Microsoft.AspNetCore.Components.WebAssembly.Http;
using Nextended.Core.Extensions;
using static IFMS_V3.Models.Ors.BudgetOfficer.BudgetOfficerModel;
using static IFMS_V3.Models.Ors.ProcessorModel.ProcessorModel;
using static IFMS_V3.Models.Ors.SectionChiefModel.SectionChiefModel;

namespace IFMS_V3.Services.Ors
{
    public class OrsService
    {
        private readonly HttpClient _httpClient;
        private readonly CsrfService _csrfService;

        public OrsService(HttpClient httpClient, CsrfService csrfService)
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

        public Task<List<FundCode>> GetFundCodes(int page = 1, int pageSize = 10)
            => GetDropdownData<FundCode>($"api/fundcode/dropdown?page={page}&pageSize={pageSize}");

        public Task<List<ResponsibilityCenter>> GetResponsibilityCenters(int page = 1, int pageSize = 10)
            => GetDropdownData<ResponsibilityCenter>($"api/responsibilitycenter/dropdown?page={page}&pageSize={pageSize}");

        public Task<List<FundCluster>> GetFundClusters(int page = 1, int pageSize = 10)
            => GetDropdownData<FundCluster>($"api/fundcluster/dropdown?page={page}&pageSize={pageSize}");

        public Task<List<MFOPAP>> GetMFOPAPs(int page = 1, int pageSize = 10)
            => GetDropdownData<MFOPAP>($"api/mfopap/dropdown?page={page}&pageSize={pageSize}");

        public Task<List<UACS>> GetUACS(int page = 1, int pageSize = 10)
            => GetDropdownData<UACS>($"api/uacs/dropdown?page={page}&pageSize={pageSize}");

        public async Task<ApiResponse<OrsModel>> CreateORSAsync(OrsModel ors)
        {
            await _csrfService.FetchCsrfTokenAsync();

            // ✅ Map OrsModel to AddOrsDto
            var dto = new AddOrsDto
            {
                Ors_Id = ors.Ors_Id,
                OrsNumber = ors.OrsNumber,
                OrsDate = ors.OrsDate ?? DateTime.Now,  // fallback if null
                AllotmentClass = ors.AllotmentClass,
                Payee = ors.Payee,
                Office = ors.Office,
                Address = ors.Address,
                Particulars = ors.Particulars,
                Amount = ors.Amount,
                Total = ors.Total,
                SignatoryA = ors.SignatoryA,
                SignatoryB = ors.SignatoryB,
                PositionA = ors.PositionA,
                PositionB = ors.PositionB,
                FundCodeId = ors.FundCodeId,
                FundClusterId = ors.FundClusterId,
                ResponsibilityCenterId = ors.ResponsibilityCenterId,
                UacsCodeId = ors.UacsCodeId,
                MfoPapId = ors.MfoPapId
            };

            var request = new HttpRequestMessage(HttpMethod.Post, "api/Ors")
            {
                Content = JsonContent.Create(dto)
            };

            request.Headers.Add("X-CSRF-TOKEN", _csrfService.CsrfToken);
            request.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);

            var response = await _httpClient.SendAsync(request);
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"API error: {content}");
                return new ApiResponse<OrsModel>
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
                var result = JsonSerializer.Deserialize<ApiResponse<OrsModel>>(content, options);
                return result ?? new ApiResponse<OrsModel>
                {
                    Status = "FAILED",
                    Message = "Deserialization returned null",
                    Data = null
                };
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"Deserialization error: {ex.Message}");
                return new ApiResponse<OrsModel>
                {
                    Status = "FAILED",
                    Message = "Deserialization error: " + ex.Message,
                    Data = null
                };
            }
        }

        public async Task<OrsModel?> GetORSByIdAsync(int id)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"api/ors/{id}");
            request.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);

            var response = await _httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode) return null;

            var content = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) },
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            var result = JsonSerializer.Deserialize<ApiResponse<OrsModel>>(content, options);
            return result?.Data;
        }
        public async Task<List<OrsModel>> GetAllForBudgetOfficerIIAsync()
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, "api/ors/budget-officer-ii");
                request.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);

                var response = await _httpClient.SendAsync(request);
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) },
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };

                var result = JsonSerializer.Deserialize<ApiResponse<List<OrsModel>>>(content, options);
                return result?.Data ?? new List<OrsModel>();
            }
            catch
            {
                return new List<OrsModel>();
            }
        }

        public async Task<List<SectionChiefDashboardModel>> GetAllForSectionChiefAsync()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "api/ors/section-chief");
            request.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);

            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadFromJsonAsync<
                    SectionChiefModel.SectionChiefDashboardApiResponse>(
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                // ✅ Extract orsListA (for /section-chief endpoint)
                if (apiResponse?.Data?.orsListA != null)
                {
                    return apiResponse.Data.orsListA;
                }
            }

            return new List<SectionChiefDashboardModel>();
        }

        public async Task<List<SectionChiefDashboardModel>> GetAllForReviewAsync()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "api/ors/section-chief-for-review");
            request.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);

            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadFromJsonAsync<
                    SectionChiefModel.SectionChiefDashboardApiResponse>(
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                // ✅ Extract orsListB (for /section-chief-for-review endpoint)
                if (apiResponse?.Data?.orsListB != null)
                {
                    return apiResponse.Data.orsListB;
                }
            }

            return new List<SectionChiefDashboardModel>();
        }


        public async Task<AdminDashboardApiResponse> GetAdminOrsDashboardDataAsync()
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, "/api/Ors/Dashboard");

                request.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);

                var response = await _httpClient.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };

                    var result = await response.Content.ReadFromJsonAsync<AdminDashboardApiResponse>(options);

                    return result;
                }
                else
                {
                    throw new HttpRequestException($"Request failed with status {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to load dashboard data", ex);
            }
        }
        public async Task<AdminDashboardApiResponse> GetAdminBursDashboardDataAsync()
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, "/api/Burs/Dashboard");

                request.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);

                var response = await _httpClient.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };

                    var result = await response.Content.ReadFromJsonAsync<AdminDashboardApiResponse>(options);

                    return result;
                }
                else
                {
                    throw new HttpRequestException($"Request failed with status {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to load dashboard data", ex);
            }
        }
        public async Task<List<ProcessorDashboardModel>> GetAllProcessorDataAsync()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "api/Ors/Processor");
            request.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);

            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadFromJsonAsync<ProcessorDashboardApiResponse>(
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                if (apiResponse?.Data?.OrsList != null)
                {
                    return apiResponse.Data.OrsList;
                }
            }

            return new List<ProcessorDashboardModel>();
        }
        //public async Task<List<SectionChiefDashboardModel>> GetPendingDashboardAsync()
        //{
        //    var request = new HttpRequestMessage(HttpMethod.Get, "api/Ors/section-chief");
        //    request.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);

        //    var response = await _httpClient.SendAsync(request);

        //    if (response.IsSuccessStatusCode)
        //    {
        //        var apiResponse = await response.Content.ReadFromJsonAsync<SectionChiefDashboardApiResponse>(
        //            new System.Text.Json.JsonSerializerOptions
        //            {
        //                PropertyNameCaseInsensitive = true
        //            });

        //        if (apiResponse?.Data?.Ors != null)
        //        {
        //            return apiResponse.Data.Ors;
        //        }
        //    }

        //    return new List<SectionChiefDashboardModel>();
        //}
        public async Task<List<BudgetOfficerDashboard>> GetAllBudgetOfficerDataAsync()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "api/Ors/budget-officer-ii");
            request.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);


            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadFromJsonAsync<BudgetOfficerDashboardApiResponse>(
                    new System.Text.Json.JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                if (apiResponse?.Data?.OrsList != null)
                {
                    return apiResponse.Data.OrsList;
                }
            }

            return new List<BudgetOfficerDashboard>();
        }

        public async Task<bool> MarkReviewedByProcessorAsync(int id)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, $"/api/Ors/mark-reviewed-by-processor/{id}");
            request.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);

            var response = await _httpClient.SendAsync(request);
            return response.IsSuccessStatusCode;
        }
        //public async Task<bool> MarkRevieweByProcessorAsync(int id)
        //{
        //    var request = new HttpRequestMessage(HttpMethod.Put, $"/api/Ors/mark-reviewed-by-processor/{id}");
        //    request.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);

        //    var response = await _httpClient.SendAsync(request);
        //    return response.IsSuccessStatusCode;
        //}


        public async Task<bool> MarkReviewedByBudgetOfficerIIAsync(int id)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, $"/api/Ors/mark-approved-by-budget-officer-ii/{id​}");
            request.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);

            var response = await _httpClient.SendAsync(request);
            return response.IsSuccessStatusCode;
        }
        //public async Task<bool> MarkReviewedBySectionChiefAsync(int id)
        //{
        //    var request = new HttpRequestMessage(HttpMethod.Put, $"/api/Ors/mark-approved-by-budget-officer-ii/{id​}");
        //    request.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);

        //    var response = await _httpClient.SendAsync(request);
        //    return response.IsSuccessStatusCode;
        //}
        //public async Task<bool> MarkReviewedByAssistantDivisionChiefAsync(int id)
        //{
        //    var request = new HttpRequestMessage(HttpMethod.Put, $"/api/Ors/mark-approved-by-budget-officer-ii/{id​}");
        //    request.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);

        //    var response = await _httpClient.SendAsync(request);
        //    return response.IsSuccessStatusCode;
        //}


        public async Task<bool> MarkRejectedByBudgetOfficerIIAsync(int id)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, $"/api/Ors/mark-rejected/{id​}");
            request.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);

            var response = await _httpClient.SendAsync(request);
            return response.IsSuccessStatusCode;
        }

    }
}
