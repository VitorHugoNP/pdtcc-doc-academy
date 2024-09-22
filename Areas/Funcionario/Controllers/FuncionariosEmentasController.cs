using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using pdtcc_doc_academy.Models;
using pdtcc_doc_academy.Repositories;

namespace pdtcc_doc_academy.Areas.Funcionario.Controllers
{
    [Area("Funcionario")]
    public class FuncionariosEmentasController : Controller
    {
        private readonly AppDBContext _context;

        public FuncionariosEmentasController(AppDBContext context)
        {
            _context = context;
        }

        // GET: Funcionario/FuncionariosEmentas
        public async Task<IActionResult> Index()
        {
            return View(await _context.Ementas.ToListAsync());
        }

        // GET: Funcionario/FuncionariosEmentas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ementas = await _context.Ementas
                .FirstOrDefaultAsync(m => m.IdEmentas == id);
            if (ementas == null)
            {
                return NotFound();
            }

            return View(ementas);
        }

        // GET: Funcionario/FuncionariosEmentas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Funcionario/FuncionariosEmentas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdEmentas")] Ementas ementas)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ementas);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ementas);
        }

        // GET: Funcionario/FuncionariosEmentas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ementas = await _context.Ementas.FindAsync(id);
            if (ementas == null)
            {
                return NotFound();
            }
            return View(ementas);
        }

        // POST: Funcionario/FuncionariosEmentas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdEmentas")] Ementas ementas)
        {
            if (id != ementas.IdEmentas)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ementas);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmentasExists(ementas.IdEmentas))
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
            return View(ementas);
        }

        // GET: Funcionario/FuncionariosEmentas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ementas = await _context.Ementas
                .FirstOrDefaultAsync(m => m.IdEmentas == id);
            if (ementas == null)
            {
                return NotFound();
            }

            return View(ementas);
        }

        // POST: Funcionario/FuncionariosEmentas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ementas = await _context.Ementas.FindAsync(id);
            if (ementas != null)
            {
                _context.Ementas.Remove(ementas);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmentasExists(int id)
        {
            return _context.Ementas.Any(e => e.IdEmentas == id);
        }
    }
}
