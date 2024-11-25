using Microsoft.AspNetCore.Mvc;
using RestauranteApi.Models;  
using RestauranteApi.Context;  
using System.Linq;
using Microsoft.EntityFrameworkCore; 
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;


namespace RestauranteApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly RestauranteContext _context;

        public AuthController(RestauranteContext context)
        {
            _context = context;
        }
        
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel loginModel)
        {
            var usuario = _context.Usuarios
                .FirstOrDefault(u => u.Username == loginModel.Username);

            if (usuario == null || usuario.Password != loginModel.Password)
            {
                return Unauthorized("Usuario o contraseña incorrectos.");
            }

            return Ok("Login exitoso");
        }

        
    }
}
