using EmployeeManagementSystem.BaseLibrary.Dtos;

namespace EmployeeManagementSystem.ClientLibrary.Services.Contracts
{
    public interface IPlaceService
    {
        Task<List<ProvinceDto>> GetProvinceListAsync();
        Task<List<DistrictDto>> GetDistrictListByProvinceIdAsync(string provinceId);
        Task<List<WardDto>> GetWardListByDistrictIdAsync(string provinceId, string districtId);
    }
}
