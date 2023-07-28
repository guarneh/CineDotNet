using CIneDotNet.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            return View();
        }

        [HttpPost]
        public  IActionResult login(string Mail, string Password ) 
        {
            var usuarioActual = _context.usuarios
                .Where(u => u.Mail.Equals(Mail) && u.Password.Equals(Password))
                .FirstOrDefault();
            if (usuarioActual != null)
            {
                
                HttpContext.Session.SetInt32("id", usuarioActual.id);
                ViewData["userId"] = usuarioActual.id;
                ViewData["esAdmin"] = usuarioActual.EsAdmin;
                
                return RedirectToAction("Details", "usuarios", usuarioActual);

                
            }
            else
            { 
                return RedirectToAction("index", "login");
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
