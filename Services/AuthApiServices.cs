using System.Net.Http.Headers;
using System.Text.Json;
using Services;

namespace Models{
    public class AuthApiServices:IAuthApiServices{
        HttpClient httpClient;
        IConfiguration configuration;
        public AuthApiServices(HttpClient httpClient,IConfiguration configuration){
            this.httpClient=httpClient;
            this.configuration=configuration;
            httpClient.BaseAddress=new Uri(configuration["ApiBaseUrl"]??throw new Exception("Base API Url is null."));
        }
        public async Task<string> LoginAsync(LoginRequest model){
            var content=new StringContent(JsonSerializer.Serialize(model),System.Text.Encoding.UTF8,"application/json");
            var response=await httpClient.PostAsync("Auth/Login",content);
            if (response.IsSuccessStatusCode){
                var apiResponse=JsonSerializer.Deserialize<ApiResponse>(await response.Content.ReadAsStringAsync())??throw new Exception("Token is null/");
                return apiResponse.token;
            }
            throw new Exception("Invalid login attempt.");
        }
        public async Task<List<User>> GetAllUsersAsync(string token){
            httpClient.DefaultRequestHeaders.Authorization=new AuthenticationHeaderValue("Bearer",token);
            var response=await httpClient.GetAsync("Auth/Users");
            if(response.IsSuccessStatusCode){
                return JsonSerializer.Deserialize<List<User>>(await response.Content.ReadAsStringAsync())??new List<User>();
            }
            return new List<User>();
        }

        public async Task RegisterUserAsync(RegisterRequest user,string token){
            httpClient.DefaultRequestHeaders.Authorization=new AuthenticationHeaderValue("Bearer",token);
            var content=new StringContent(JsonSerializer.Serialize(user),System.Text.Encoding.UTF8,"application/json");
            var response=await httpClient.PostAsync("Auth/Register",content);
            if (!response.IsSuccessStatusCode){ 
                throw new Exception("Failed to register user");
            }
        }

        public async Task UpdateUserAsync(string empId,RegisterRequest user,string token){
            httpClient.DefaultRequestHeaders.Authorization=new AuthenticationHeaderValue("Bearer",token);
            var content=new StringContent(JsonSerializer.Serialize(user),System.Text.Encoding.UTF8,"application/json");
            var response=await httpClient.PutAsync($"Auth/Users/{empId}",content);
            if (!response.IsSuccessStatusCode){
                throw new Exception("Failed to update user");
            }
        }

        public async Task DeleteUserAsync(string empId,string token){
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await httpClient.DeleteAsync($"Auth/Users/{empId}");
            if(!response.IsSuccessStatusCode){ 
                throw new Exception("Failed to delete user");
            }
        }
    }
}