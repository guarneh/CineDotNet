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
        public  IActionResult Login(string Mail, string Password ) 
        {
            var usuarioActual = _context.usuarios
                .Where(u => u.Mail.Equals(Mail))
                .FirstOrDefault();
            if (usuarioActual != null && usuarioActual.Bloqueado == false)
            {
                if (usuarioActual.Password.Equals(Password))
                {
                    usuarioActual.IntentosFallidos = 0;
                    _context.usuarios.Update(usuarioActual);
                    _context.SaveChanges();
                    HttpContext.Session.SetInt32("id", usuarioActual.id);
                    return RedirectToAction("EsAdmin", "usuarios");
                }
                else
                {
                    usuarioActual.IntentosFallidos++;
                    if (usuarioActual.IntentosFallidos >= 3)
                    {
                        usuarioActual.Bloqueado = true;
                        _context.usuarios.Update(usuarioActual);
                        _context.SaveChanges();
                        return View("index");
                    }
                    else
                    {
                        _context.usuarios.Update(usuarioActual);
                        _context.SaveChanges();
                        return View("index");
                    }
                }   
            }
            else
            { 
                return View("index");
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
