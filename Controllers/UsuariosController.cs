using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CIneDotNet.Models;

namespace CIneDotNet.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly MyContext _context;

        public UsuariosController(MyContext context)
        {
            _context = context;
        }

        // GET: Usuarios
        public async Task<IActionResult> Index()
        {
              return View(await _context.usuarios.ToListAsync());
        }

        // GET: Usuarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.usuarios == null)
            {
                return NotFound();
            }

            var usuario = await _context.usuarios
                .FirstOrDefaultAsync(m => m.id == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // GET: Usuarios/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Usuarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,DNI,Nombre,Apellido,Mail,Password,IntentosFallidos,Bloqueado,Credito,FechaNacimiento,EsAdmin")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                _context.Add(usuario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(usuario);
        }

        // GET: Usuarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.usuarios == null)
            {
                return NotFound();
            }

            var usuario = await _context.usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return View(usuario);
        }

        // POST: Usuarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,DNI,Nombre,Apellido,Mail,Password,IntentosFallidos,Bloqueado,Credito,FechaNacimiento,EsAdmin")] Usuario usuario)
        {
            if (id != usuario.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(usuario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuarioExists(usuario.id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(usuario);
        }

        // GET: Usuarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.usuarios == null)
            {
                return NotFound();
            }

            var usuario = await _context.usuarios
                .FirstOrDefaultAsync(m => m.id == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.usuarios == null)
            {
                return Problem("Entity set 'MyContext.usuarios'  is null.");
            }
            var usuario = await _context.usuarios.FindAsync(id);
            if (usuario != null)
            {
                _context.usuarios.Remove(usuario);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsuarioExists(int id)
        {
          return _context.usuarios.Any(e => e.id == id);
        }

        
        public IActionResult esAdmin()
        {
            if (HttpContext.Session.GetInt32("id") != null)
            {
                Usuario logueado = _context.usuarios.Include(u => u.MisFunciones).Include(U => U.Tickets).Where(u => u.id == HttpContext.Session.GetInt32("id")).FirstOrDefault();
                if (logueado.EsAdmin)
                {
                    return RedirectToAction("MisDatosAdmin", "usuarios");
                }else
                    return RedirectToAction("MisDatos", "usuarios");
            }
            else
            {
                return RedirectToAction("index", "login");
            }
        }

        public IActionResult MisDatos() 
        {
            if (HttpContext.Session.GetInt32("id") != null)
            {
                Usuario logueado = _context.usuarios.Include(u => u.MisFunciones).Include(U => U.Tickets).Where(u => u.id == HttpContext.Session.GetInt32("id")).FirstOrDefault();
                
                return View(logueado);
            }
            else
            {
                return RedirectToAction("index","login");
            }
        }

        public IActionResult MisDatosAdmin()
        {
            if (HttpContext.Session.GetInt32("id") != null)
            {
                Usuario logueado = _context.usuarios.Include(u => u.MisFunciones).Include(U => U.Tickets).Where(u => u.id == HttpContext.Session.GetInt32("id")).FirstOrDefault();
                if (logueado.EsAdmin)
                {
                    return View(logueado);
                }
                else
                    return RedirectToAction("MisDatos", "usuarios");
            }
            else
            {
                return RedirectToAction("index", "login");
            }
        }

        [HttpPost]
        public IActionResult DevolverEntrada(int idFuncion, int cantEntradas)
        {
            Usuario usuarioActual = _context.usuarios.Where(u => u.id == HttpContext.Session.GetInt32("id")).FirstOrDefault();
            FuncionUsuario fuSelected = _context.funcionUsuarios.Include(fu => fu.funcion).Include(fu => fu.usuario).Where(fu => fu.usuario == usuarioActual && fu.idFuncion == idFuncion).FirstOrDefault();
            if (fuSelected != null)
            {
                if (fuSelected.cantEntradas >= cantEntradas)
                {
                    usuarioActual.Credito += fuSelected.funcion.costo * cantEntradas;
                    fuSelected.cantEntradas -= cantEntradas;
                    if (fuSelected.cantEntradas == 0)
                    {
                        usuarioActual.Tickets.Remove(fuSelected);
                        fuSelected.funcion.funcionUsuarios.Remove(fuSelected);
                        fuSelected.funcion.clientes.Remove(usuarioActual);
                        _context.funcionUsuarios.Remove(fuSelected);
                        _context.SaveChanges();
                        return RedirectToAction("index", "home");
                    }
                    else
                    {
                        _context.usuarios.Update(usuarioActual);
                        _context.funcionUsuarios.Update(fuSelected);
                        _context.SaveChanges();
                        return RedirectToAction("index", "home");
                    }
                }
                else
                    return NotFound();
            }
            else
                return NotFound();
        }

        public IActionResult CargarCredito( double credito)
        {
            if (credito > 0)
            { 
                Usuario usuarioActual = _context.usuarios.Where(u => u.id == HttpContext.Session.GetInt32("id")).FirstOrDefault();
                if (usuarioActual != null)
                {                   
                    usuarioActual.Credito += credito;
                    _context.usuarios.Update(usuarioActual);
                    _context.SaveChanges();
                    return RedirectToAction("esAdmin","usuarios");
                }
                else
                    return RedirectToAction("index", "login");
            }
            else
                return RedirectToAction("esAdmin", "usuarios");
        }
    }
}
