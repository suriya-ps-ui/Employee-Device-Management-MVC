using System.Net.Http.Headers;
using System.Text.Json;
using Models;
using Services;

namespace EmployeeDeviceManagementMVC.Services
{
    public class EmployeeApiServices : IEmployeeServices
    {
        private readonly HttpClient httpClient;
        private readonly IConfiguration configuration;

        public EmployeeApiServices(HttpClient httpClient, IConfiguration configuration)
        {
            this.httpClient = httpClient;
            this.configuration = configuration;
            httpClient.BaseAddress = new Uri(configuration["ApiBaseUrl"] ?? throw new Exception("Base API Url is null."));
        }

        public async Task<List<Employee>> GetAllEmployeesAsync(string token)
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await httpClient.GetAsync("Employee/Employees");
            if (response.IsSuccessStatusCode)
            {
                return JsonSerializer.Deserialize<List<Employee>>(await response.Content.ReadAsStringAsync()) ?? new List<Employee>();
            }
            return new List<Employee>();
        }

        public async Task<Employee?> GetEmployeeByIdAsync(string empId, string token)
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await httpClient.GetAsync($"Employee/Employees/{empId}");
            
            var jsonResponse = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"API Response for empId {empId}: {jsonResponse}");

            if (response.IsSuccessStatusCode)
            {
                try
                {
                    return JsonSerializer.Deserialize<Employee>(jsonResponse);
                }
                catch (JsonException ex)
                {
                    Console.WriteLine($"Deserialization failed: {ex.Message}");
                    return null;
                }
            }
            return null;
        }

        public async Task<EmployeeRequest?> CreateEmployeeAsync(EmployeeRequest employee, string token)
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var content = new StringContent(JsonSerializer.Serialize(employee), System.Text.Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync("Employee/Employees", content);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Failed to create employee");
            }
            return JsonSerializer.Deserialize<EmployeeRequest>(await response.Content.ReadAsStringAsync());
        }

        public async Task UpdateEmployeeAsync(string empId, EmployeeRequest employee, string token)
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var content = new StringContent(JsonSerializer.Serialize(employee), System.Text.Encoding.UTF8, "application/json");
            var response = await httpClient.PutAsync($"Employee/Employees/{empId}", content);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Failed to update employee");
            }
        }

        public async Task DeleteEmployeeAsync(string empId, string token)
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await httpClient.DeleteAsync($"Employee/Employees/{empId}");
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Failed to delete employee");
            }
        }
    }
}