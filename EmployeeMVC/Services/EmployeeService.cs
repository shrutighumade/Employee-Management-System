using EmployeeMVC.Models;
using System.Net.Http.Json;

namespace EmployeeMVC.Services
{
    public class EmployeeService
    {
        private readonly HttpClient _httpClient;
        protected string _baseUrl = "http://localhost:5269
        // /api/employee"; // ✅ must match backend

        public EmployeeService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Employee>> GetAllAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Employee>>(_baseUrl) ?? new List<Employee>();
        }

        public async Task<Employee?> GetByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Employee>($"{_baseUrl}/{id}");
        }

        public async Task CreateAsync(Employee emp)
        {
            var response = await _httpClient.PostAsJsonAsync(_baseUrl, emp);
            if (!response.IsSuccessStatusCode)
            {
                var body = await response.Content.ReadAsStringAsync();
                throw new ApplicationException($"Create failed: {response.StatusCode} - {body}");
            }
        }


        public async Task UpdateAsync(Employee emp)
        {
            await _httpClient.PutAsJsonAsync($"{_baseUrl}/{emp.Id}", emp);
        }

        public async Task DeleteAsync(int id)
        {
            await _httpClient.DeleteAsync($"{_baseUrl}/{id}");
        }
    }
}
