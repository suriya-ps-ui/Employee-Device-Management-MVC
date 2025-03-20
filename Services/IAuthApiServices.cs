using Models;
namespace Services{
    public interface IAuthApiServices{
        Task<string> LoginAsync(LoginRequest model);
        Task<List<User>> GetAllUsersAsync(string token);
        Task RegisterUserAsync(RegisterRequest user,string token);
        Task UpdateUserAsync(string empId,RegisterRequest user,string token);
        Task DeleteUserAsync(string empId,string token);        
    }
}