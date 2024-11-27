using EmployeeManagementSystem.BaseLibrary.Dtos;
using EmployeeManagementSystem.BaseLibrary.VietNam;

using Newtonsoft.Json;

namespace EmployeeManagementSystem.ServerLibrary.Helper
{
    public static class ReadFileJson
    {
        public static async Task<List<ProvinceDto>> ReadFile(string path)
        {
            var text = await File.ReadAllTextAsync(path);
            var province = JsonConvert.DeserializeObject<List<Province>>(text);
            var provinceDto = province!.Select(i => new ProvinceDto(i.Id, i.Name)).ToList();
            return provinceDto;
        }

        public static async Task<List<DistrictDto>> ReadFile(string path, string provinceId)
        {
            var text = await File.ReadAllTextAsync(path);
            var provinces = JsonConvert.DeserializeObject<List<Province>>(text);
            var province = provinces!.Where(i => i.Id == provinceId).FirstOrDefault();
            var districtDto = province!.Districts!.Select(i => new DistrictDto(i.Id, i.Name)).ToList();
            return districtDto;
        }

        public static async Task<List<WardDto>> ReadFile(string path, string provinceId, string districtId)
        {
            var text = await File.ReadAllTextAsync(path);
            var provinces = JsonConvert.DeserializeObject<List<Province>>(text);
            var province = provinces!.Where(i => i.Id == provinceId).FirstOrDefault();
            var district = province!.Districts!.Where(i => i.Id == districtId).FirstOrDefault();
            var wardDto = district!.Wards!.Select(i => new WardDto(i.Id, i.Name, i.Level)).ToList();
            return wardDto;
        }
    }
}
