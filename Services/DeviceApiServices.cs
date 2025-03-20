using System.Net.Http.Headers;
using System.Text.Json;
using Models;
namespace Services
{
    public class DeviceApiServices : IDeviceApiServices
    {
        private readonly HttpClient httpClient;
        private readonly IConfiguration configuration;

        public DeviceApiServices(HttpClient httpClient, IConfiguration configuration)
        {
            this.httpClient = httpClient;
            this.configuration = configuration;
            httpClient.BaseAddress = new Uri(configuration["ApiBaseUrl"] ?? throw new Exception("Base API Url is null."));
        }

        public async Task<List<Laptop>> GetLaptopsAsync(string empId, string token)
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await httpClient.GetAsync($"Laptop/Employees/{empId}/Laptops");
            return response.IsSuccessStatusCode
                ? JsonSerializer.Deserialize<List<Laptop>>(await response.Content.ReadAsStringAsync()) ?? new List<Laptop>()
                : new List<Laptop>();
        }

        public async Task<Laptop?> CreateLaptopAsync(Laptop laptop, string token)
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var content = new StringContent(JsonSerializer.Serialize(laptop), System.Text.Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync($"Laptop/Employees/{laptop.empId}/Laptops", content);
            if (!response.IsSuccessStatusCode) throw new Exception("Failed to create laptop");
            return JsonSerializer.Deserialize<Laptop>(await response.Content.ReadAsStringAsync());
        }

        public async Task UpdateLaptopAsync(string empId, string lapHostName, Laptop laptop, string token)
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var content = new StringContent(JsonSerializer.Serialize(laptop), System.Text.Encoding.UTF8, "application/json");
            var response = await httpClient.PutAsync($"Laptop/Employees/{empId}/Laptops/{lapHostName}", content);
            if (!response.IsSuccessStatusCode) throw new Exception("Failed to update laptop");
        }

        public async Task DeleteLaptopAsync(string empId, string lapHostName, string token)
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await httpClient.DeleteAsync($"Laptop/Employees/{empId}/Laptops/{lapHostName}");
            if (!response.IsSuccessStatusCode) throw new Exception("Failed to delete laptop");
        }

        public async Task<List<Keyboard>> GetKeyboardsAsync(string empId, string token)
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await httpClient.GetAsync($"Keyboard/Employees/{empId}/Keyboards");
            return response.IsSuccessStatusCode
                ? JsonSerializer.Deserialize<List<Keyboard>>(await response.Content.ReadAsStringAsync()) ?? new List<Keyboard>()
                : new List<Keyboard>();
        }

        public async Task<Keyboard?> CreateKeyboardAsync(Keyboard keyboard, string token)
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var content = new StringContent(JsonSerializer.Serialize(keyboard), System.Text.Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync($"Keyboard/Employees/{keyboard.empId}/Keyboards", content);
            if (!response.IsSuccessStatusCode) throw new Exception("Failed to create keyboard");
            return JsonSerializer.Deserialize<Keyboard>(await response.Content.ReadAsStringAsync());
        }

        public async Task UpdateKeyboardAsync(string keyId, Keyboard keyboard, string token)
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var content = new StringContent(JsonSerializer.Serialize(keyboard), System.Text.Encoding.UTF8, "application/json");
            var response = await httpClient.PutAsync($"Keyboard/Employees/{keyboard.empId}/Keyboards/{keyId}", content);
            if (!response.IsSuccessStatusCode) throw new Exception("Failed to update keyboard");
        }

        public async Task DeleteKeyboardAsync(string empId, string keyId, string token)
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await httpClient.DeleteAsync($"Keyboard/Employees/{empId}/Keyboards/{keyId}");
            if (!response.IsSuccessStatusCode) throw new Exception("Failed to delete keyboard"); // Fixed logic
        }

        public async Task<List<Mouse>> GetMousesAsync(string empId, string token)
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await httpClient.GetAsync($"Mouse/Employees/{empId}/Mouses");
            return response.IsSuccessStatusCode
                ? JsonSerializer.Deserialize<List<Mouse>>(await response.Content.ReadAsStringAsync()) ?? new List<Mouse>()
                : new List<Mouse>();
        }

        public async Task<Mouse?> CreateMouseAsync(Mouse mouse, string token)
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var content = new StringContent(JsonSerializer.Serialize(mouse), System.Text.Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync($"Mouse/Employees/{mouse.empId}/Mouses", content);
            if (!response.IsSuccessStatusCode) throw new Exception("Failed to create mouse");
            return JsonSerializer.Deserialize<Mouse>(await response.Content.ReadAsStringAsync());
        }

        public async Task UpdateMouseAsync(string mouseId, Mouse mouse, string token)
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var content = new StringContent(JsonSerializer.Serialize(mouse), System.Text.Encoding.UTF8, "application/json");
            var response = await httpClient.PutAsync($"Mouse/Employees/{mouse.empId}/Mouses/{mouseId}", content);
            if (!response.IsSuccessStatusCode) throw new Exception("Failed to update mouse");
        }

        public async Task DeleteMouseAsync(string empId, string mouseId, string token)
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await httpClient.DeleteAsync($"Mouse/Employees/{empId}/Mouses/{mouseId}");
            if (!response.IsSuccessStatusCode) throw new Exception("Failed to delete mouse");
        }
    }
}