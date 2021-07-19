using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StandWeb.Data;
using StandWeb.Models;

namespace StandWeb.Controllers
{
    public class GostosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GostosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Gostos
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Gostos.Include(f => f.Carro).Include(f => f.Utilizador);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Gostos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gostos = await _context.Gostos
                .Include(f => f.Carro)
                .Include(f => f.Utilizador)
                .FirstOrDefaultAsync(m => m.IdGostos == id);
            if (gostos == null)
            {
                return NotFound();
            }

            return View(gostos);
        }

        // GET: Gostos/Create
        public IActionResult Create()
        {
            ViewData["CarrosFK"] = new SelectList(_context.Carros, "IdCarros", "Foto");
            ViewData["UtilizadoresFK"] = new SelectList(_context.Utilizadores, "IdUtilizador", "Email");
            return View();
        }

        // POST: Gostos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdGostos,UtilizadoresFK,CarrosFK")] Gostos gostos)
        {
            if (ModelState.IsValid)
            {
                _context.Add(gostos);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CarrosFK"] = new SelectList(_context.Carros, "IdCarros", "Foto", gostos.CarrosFK);
            ViewData["UtilizadoresFK"] = new SelectList(_context.Utilizadores, "IdUtilizador", "Email", gostos.UtilizadoresFK);
            return View(gostos);
        }

        // GET: Gostos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gostos = await _context.Gostos.FindAsync(id);
            if (gostos == null)
            {
                return NotFound();
            }
            ViewData["CarrosFK"] = new SelectList(_context.Carros, "IdCarros", "Foto", gostos.CarrosFK);
            ViewData["UtilizadoresFK"] = new SelectList(_context.Utilizadores, "IdUtilizador", "Email", gostos.UtilizadoresFK);
            return View(gostos);
        }

        // POST: Gostos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdGostos,UtilizadoresFK,CarrosFK")] Gostos gostos)
        {
            if (id != gostos.IdGostos)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gostos);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GostosExists(gostos.IdGostos))
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
            ViewData["CarrosFK"] = new SelectList(_context.Carros, "IdCarros", "Foto", gostos.CarrosFK);
            ViewData["UtilizadoresFK"] = new SelectList(_context.Utilizadores, "IdUtilizador", "Email", gostos.UtilizadoresFK);
            return View(gostos);
        }

        // GET: Gostos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gostos = await _context.Gostos
                .Include(f => f.Carro)
                .Include(f => f.Utilizador)
                .FirstOrDefaultAsync(m => m.IdGostos == id);
            if (gostos == null)
            {
                return NotFound();
            }

            return View(gostos);
        }

        // POST: Gostos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var gostos = await _context.Gostos.FindAsync(id);
            _context.Gostos.Remove(gostos);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GostosExists(int id)
        {
            return _context.Gostos.Any(e => e.IdGostos == id);
        }
    }
}
