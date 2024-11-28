using EmployeeManagementSystem.BaseLibrary.Dtos;
using EmployeeManagementSystem.ClientLibrary.Helpers;
using EmployeeManagementSystem.ClientLibrary.Services.Contracts;
using System.Net.Http.Json;

namespace EmployeeManagementSystem.ClientLibrary.Services.Implementations
{
    public class PlaceService : IPlaceService
    {
        private readonly GetHttpClient _httpClient;
        private const string AuthUrl = "api/places";

        public PlaceService(GetHttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ProvinceDto>> GetProvinceListAsync()
        {
            var httpClient = await _httpClient.GetPrivateHttpClient();
            var result = await httpClient.GetFromJsonAsync<List<ProvinceDto>>($"{AuthUrl}/getProvinces");
            return result!;
        }

        public async Task<List<DistrictDto>> GetDistrictListByProvinceIdAsync(string provinceId)
        {
            var httpClient = await _httpClient.GetPrivateHttpClient();
            var result = await httpClient.GetFromJsonAsync<List<DistrictDto>>($"{AuthUrl}/getDistricts/{provinceId}");
            return result!;
        }

        public async Task<List<WardDto>> GetWardListByDistrictIdAsync(string provinceId, string districtId)
        {
            var httpClient = await _httpClient.GetPrivateHttpClient();
            var result = await httpClient.GetFromJsonAsync<List<WardDto>>($"{AuthUrl}/getWards/{provinceId}/{districtId}");
            return result!;
        }
    }
}
