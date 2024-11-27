using EmployeeManagementSystem.BaseLibrary.VietNam;
using Newtonsoft.Json;

namespace EmployeeManagementSystem.ServerLibrary.Helper
{
    public static class ReadFileJson
    {
        public static async Task<List<Province>> ReadFile(string path)
        {
            var text = await File.ReadAllTextAsync(path);
            var province = JsonConvert.DeserializeObject<List<Province>>(text);
            return province!;
        }

        public static async Task<List<District>> ReadFile(string path, string provinceId)
        {
            var text = await File.ReadAllTextAsync(path);
            var provinces = JsonConvert.DeserializeObject<List<Province>>(text);
            var province = provinces!.Where(i => i.Id == provinceId).FirstOrDefault();
            return province!.Districts!;
        }

        public static async Task<List<Ward>> ReadFile(string path, string provinceId, string districtId)
        {
            var text = await File.ReadAllTextAsync(path);
            var provinces = JsonConvert.DeserializeObject<List<Province>>(text);
            var province = provinces!.Where(i => i.Id == provinceId).FirstOrDefault();
            var district = province!.Districts!.Where(i => i.Id == districtId).FirstOrDefault();
            return district!.Wards!;
        }
    }
}
