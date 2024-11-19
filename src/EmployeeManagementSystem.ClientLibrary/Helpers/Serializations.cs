using System.Text.Json;

namespace EmployeeManagementSystem.ClientLibrary.Helpers
{
    public class Serializations
    {
        public static string SerializeObj<T>(T obj) => JsonSerializer.Serialize(obj);

        public static T DeserializeJsonString<T>(string jsonString) => JsonSerializer.Deserialize<T>(jsonString);

        public static IList<T> DeserializeJsonStringList<T>(string jsonString) => JsonSerializer.Deserialize<IList<T>>(jsonString);
    }
}
