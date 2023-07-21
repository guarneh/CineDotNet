using CIneDotNet.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CIneDotNet.Controllers
{
    public class LoginController : Controller
    {
        private readonly MyContext _context;
       
        public LoginController(MyContext context)
        {
            _context = context;
           
        }
        public IActionResult Index()
        {
            ClaimsPrincipal claimUser = HttpContext.User;
            if (claimUser.Identity.IsAuthenticated)
            {
                return RedirectToAction("index", "home");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> login(Usuario usuario) 
        {
            var usuarioActual = _context.usuarios
                .Where(u => u.Mail.Equals(usuario.Mail) && u.Password.Equals(usuario.Password))
                .FirstOrDefault();
            if (usuarioActual != null)
            {
                List<Claim> claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.NameIdentifier, usuarioActual.Mail),
                    new Claim(ClaimTypes.Role, usuarioActual.EsAdmin.ToString())
                };

                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims,

                    CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,new ClaimsPrincipal(claimsIdentity));
                return RedirectToAction("details", "usuarios" , usuarioActual.id);

                
            }
            else
            { 
                return RedirectToAction("index", "home");
            }
                
        }

        

        

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register( Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                if (_context.usuarios.Any(u => u.Mail == usuario.Mail || u.DNI == usuario.DNI))
                {
                    
                    return RedirectToAction("index");
                }
                else
                {
                var nuevo = new Usuario
                {
                    DNI = usuario.DNI,
                    Nombre = usuario.Nombre,
                    Apellido = usuario.Apellido,
                    Mail = usuario.Mail,
                    Password = usuario.Password,
                    IntentosFallidos = 0,
                    Bloqueado = false,
                    Credito = 0,
                    FechaNacimiento = usuario.FechaNacimiento,
                    EsAdmin = false
                };
                _context.usuarios.Add(nuevo);
                await _context.SaveChangesAsync();
                return RedirectToAction("index"); 
                }
            }
            return View(usuario);
        }
    }
}
