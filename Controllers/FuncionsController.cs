using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CIneDotNet.Models;
using Microsoft.AspNetCore.Authorization;

namespace CIneDotNet.Controllers
{
    [Authorize]
    public class FuncionsController : Controller
    {
        private readonly MyContext _context;
        

        public FuncionsController(MyContext context)
        {
            _context = context;
        }

        // GET: Funcions
        public async Task<IActionResult> Index()
        {
            var myContext = _context.funciones.Include(f => f.miPelicula).Include(f => f.miSala);
            return View(await myContext.ToListAsync());
        }

        // GET: Funcions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.funciones == null)
            {
                return NotFound();
            }

            var funcion = await _context.funciones
                .Include(f => f.miPelicula)
                .Include(f => f.miSala)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (funcion == null)
            {
                return NotFound();
            }

            return View(funcion);
        }

        // GET: Funcions/Create
        public IActionResult Create()
        {
            ViewData["idPelicula"] = new SelectList(_context.peliculas, "id", "id");
            ViewData["idSala"] = new SelectList(_context.salas, "id", "id");
            return View();
        }

        // POST: Funcions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,idSala,idPelicula,fecha,cantClientes,costo")] Funcion funcion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(funcion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["idPelicula"] = new SelectList(_context.peliculas, "id", "id", funcion.idPelicula);
            ViewData["idSala"] = new SelectList(_context.salas, "id", "id", funcion.idSala);
            return View(funcion);
        }

        // GET: Funcions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.funciones == null)
            {
                return NotFound();
            }

            var funcion = await _context.funciones.FindAsync(id);
            if (funcion == null)
            {
                return NotFound();
            }
            ViewData["idPelicula"] = new SelectList(_context.peliculas, "id", "id", funcion.idPelicula);
            ViewData["idSala"] = new SelectList(_context.salas, "id", "id", funcion.idSala);
            return View(funcion);
        }

        // POST: Funcions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,idSala,idPelicula,fecha,cantClientes,costo")] Funcion funcion)
        {
            if (id != funcion.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(funcion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FuncionExists(funcion.ID))
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
            ViewData["idPelicula"] = new SelectList(_context.peliculas, "id", "id", funcion.idPelicula);
            ViewData["idSala"] = new SelectList(_context.salas, "id", "id", funcion.idSala);
            return View(funcion);
        }

        // GET: Funcions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.funciones == null)
            {
                return NotFound();
            }

            var funcion = await _context.funciones
                .Include(f => f.miPelicula)
                .Include(f => f.miSala)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (funcion == null)
            {
                return NotFound();
            }

            return View(funcion);
        }

        // POST: Funcions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.funciones == null)
            {
                return Problem("Entity set 'MyContext.funciones'  is null.");
            }
            var funcion = await _context.funciones.FindAsync(id);
            if (funcion != null)
            {
                _context.funciones.Remove(funcion);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FuncionExists(int id)
        {
          return _context.funciones.Any(e => e.ID == id);
        }
    }
}
