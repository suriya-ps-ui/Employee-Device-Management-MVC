using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var token = HttpContext.Session.GetString("JWToken");
            if (string.IsNullOrEmpty(token))
            {
                ViewData["IsAuthenticated"] = false;
            }
            else
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var jwtToken = tokenHandler.ReadJwtToken(token);
                ViewData["IsAuthenticated"] = true;
                ViewData["Username"] = jwtToken.Claims.FirstOrDefault(c => c.Type == "unique_name")?.Value;
                ViewData["Role"] = jwtToken.Claims.FirstOrDefault(c => c.Type == "role")?.Value;
            }
            return View();
        }
    }
}