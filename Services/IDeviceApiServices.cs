using Models;
namespace Services{
    public interface IDeviceApiServices{
        Task<List<Laptop>> GetLaptopsAsync(string empId,string token);
        Task<Laptop?> CreateLaptopAsync(Laptop laptop,string token);
        Task UpdateLaptopAsync(string empId,string lapHostName,Laptop laptop,string token);
        Task DeleteLaptopAsync(string empId,string lapHostName,string token);

        Task<List<Keyboard>> GetKeyboardsAsync(string empId,string token);
        Task<Keyboard?> CreateKeyboardAsync(Keyboard keyboard,string token);
        Task UpdateKeyboardAsync(string keyId,Keyboard keyboard,string token);
        Task DeleteKeyboardAsync(string empId,string keyId,string token);

        Task<List<Mouse>> GetMousesAsync(string empId,string token);
        Task<Mouse?> CreateMouseAsync(Mouse mouse,string token);
        Task UpdateMouseAsync(string mouseId,Mouse mouse,string token);
        Task DeleteMouseAsync(string empId,string mouseId,string token);

    }
}