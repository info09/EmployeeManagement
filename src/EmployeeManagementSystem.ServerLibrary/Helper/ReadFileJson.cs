using EmployeeManagementSystem.BaseLibrary.Dtos;
using EmployeeManagementSystem.BaseLibrary.VietNam;

using Newtonsoft.Json;

namespace EmployeeManagementSystem.ServerLibrary.Helper
{
    public static class ReadFileJson
    {
        public static async Task<List<ProvinceDto>> ReadFileAsync(string path)
        {
            using (var stream = new StreamReader(path))
            using (var jsonReader = new JsonTextReader(stream))
            {
                var serializer = new JsonSerializer();
                var provinces = serializer.Deserialize<List<Province>>(jsonReader);
                var provinceDto = provinces?.Select(i => new ProvinceDto(i.Id, i.Name)).ToList();
                return provinceDto ?? new List<ProvinceDto>();
            }
        }

        public static async Task<List<DistrictDto>> ReadFileAsync(string path, string provinceId)
        {
            var text = await File.ReadAllTextAsync(path);
            var provinces = JsonConvert.DeserializeObject<List<Province>>(text);
            var province = provinces!.Where(i => i.Id == provinceId).FirstOrDefault();
            var districtDto = province!.Districts!.Select(i => new DistrictDto(i.Id, i.Name)).ToList();
            return districtDto;
        }

        public static async Task<List<WardDto>> ReadFileAsync(string filePath, string provinceId, string districtId)
        {
            var fileContent = await File.ReadAllTextAsync(filePath);
            var provinces = JsonConvert.DeserializeObject<List<Province>>(fileContent);
            var targetProvince = provinces?.FirstOrDefault(p => p.Id == provinceId);
            var targetDistrict = targetProvince?.Districts?.FirstOrDefault(d => d.Id == districtId);
            var wardDtos = targetDistrict?.Wards?.Select(w => new WardDto(w.Id, w.Name, w.Level))?.ToList();
            return wardDtos ?? new List<WardDto>();
        }
    }
}
