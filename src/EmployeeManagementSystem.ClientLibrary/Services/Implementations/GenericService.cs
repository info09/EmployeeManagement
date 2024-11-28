using EmployeeManagementSystem.BaseLibrary.Response;
using EmployeeManagementSystem.BaseLibrary.SeedWorks;
using EmployeeManagementSystem.ClientLibrary.Helpers;
using EmployeeManagementSystem.ClientLibrary.Models;
using EmployeeManagementSystem.ClientLibrary.Services.Contracts;
using System.Net.Http.Json;

namespace EmployeeManagementSystem.ClientLibrary.Services.Implementations
{
    public class GenericService<T> : IGenericService<T> where T : class
    {
        private readonly GetHttpClient _httpClient;

        public GenericService(GetHttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<GeneralResponse> Delete(string baseUrl, int id)
        {
            var httpClient = await _httpClient.GetPrivateHttpClient();
            var response = await httpClient.DeleteAsync($"{baseUrl}/{id}");
            var result = await response.Content.ReadFromJsonAsync<GeneralResponse>();
            return result!;
        }

        public async Task<List<T>> GetAll(string baseUrl)
        {
            var httpClient = await _httpClient.GetPrivateHttpClient();
            var response = await httpClient.GetFromJsonAsync<List<T>>($"{baseUrl}");
            return response!;
        }

        public async Task<PagedList<T>> GetAllPaging(string baseUrl, GenericSearch search)
        {
            var httpClient = await _httpClient.GetPrivateHttpClient();
            var response = await httpClient.GetFromJsonAsync<PagedList<T>>($"{baseUrl}?branchId={search.BranchId}&keyword={search.Keyword}&PageNumber={search.PageNumber}&PageSize={search.PageSize}");
            return response!;
        }

        public async Task<T> GetById(string baseUrl, int id)
        {
            var httpClient = await _httpClient.GetPrivateHttpClient();
            var response = await httpClient.GetFromJsonAsync<T>($"{baseUrl}/{id}");
            return response!;
        }

        public async Task<GeneralResponse> Insert(string baseUrl, T entity)
        {
            var httpClient = await _httpClient.GetPrivateHttpClient();
            var response = await httpClient.PostAsJsonAsync($"{baseUrl}", entity);
            var result = await response.Content.ReadFromJsonAsync<GeneralResponse>();
            return result!;
        }

        public async Task<GeneralResponse> Update(string baseUrl, T entity)
        {
            var httpClient = await _httpClient.GetPrivateHttpClient();
            var response = await httpClient.PutAsJsonAsync($"{baseUrl}", entity);
            var result = await response.Content.ReadFromJsonAsync<GeneralResponse>();
            return result!;
        }
    }
}
