using Microsoft.AspNetCore.Mvc;
using Services;
using System.IdentityModel.Tokens.Jwt;

namespace Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeServices employeeServices;

        public EmployeeController(IEmployeeServices employeeServices)
        {
            this.employeeServices = employeeServices;
        }

        public async Task<IActionResult> Devices(string empId = null) // Add optional parameter
{
    var token = HttpContext.Session.GetString("JWToken");
    if (string.IsNullOrEmpty(token))
    {
        Console.WriteLine("No token found in session.");
        return RedirectToAction("Login", "Account");
    }

    var tokenHandler = new JwtSecurityTokenHandler();
    var jwtToken = tokenHandler.ReadJwtToken(token);
    empId ??= jwtToken.Claims.FirstOrDefault(c => c.Type == "EmpId")?.Value; // Use parameter if provided, else token

    if (string.IsNullOrEmpty(empId))
    {
        Console.WriteLine("No empId found in token or parameters.");
        return RedirectToAction("Login", "Account");
    }

    Console.WriteLine($"Fetching employee data for empId: {empId}");
    var employee = await employeeServices.GetEmployeeByIdAsync(empId, token);
    if (employee == null)
    {
        Console.WriteLine($"Employee not found for empId: {empId}");
        return RedirectToAction("Login", "Account");
    }

    return View(employee);
}
    }
}