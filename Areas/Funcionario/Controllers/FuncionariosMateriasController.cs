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
    public class FuncionariosMateriasController : Controller
    {
        private readonly AppDBContext _context;

        public FuncionariosMateriasController(AppDBContext context)
        {
            _context = context;
        }

        // GET: Funcionario/FuncionariosMaterias
        public async Task<IActionResult> Index()
        {
            return View(await _context.Materia.ToListAsync());
        }

        // GET: Funcionario/FuncionariosMaterias/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var materias = await _context.Materia
                .FirstOrDefaultAsync(m => m.IdMateria == id);
            if (materias == null)
            {
                return NotFound();
            }

            return View(materias);
        }

        // GET: Funcionario/FuncionariosMaterias/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Funcionario/FuncionariosMaterias/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdMateria,nomeMateria")] Materias materias)
        {
            if (ModelState.IsValid)
            {
                _context.Add(materias);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(materias);
        }

        // GET: Funcionario/FuncionariosMaterias/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var materias = await _context.Materia.FindAsync(id);
            if (materias == null)
            {
                return NotFound();
            }
            return View(materias);
        }

        // POST: Funcionario/FuncionariosMaterias/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdMateria,nomeMateria")] Materias materias)
        {
            if (id != materias.IdMateria)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(materias);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MateriasExists(materias.IdMateria))
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
            return View(materias);
        }

        // GET: Funcionario/FuncionariosMaterias/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var materias = await _context.Materia
                .FirstOrDefaultAsync(m => m.IdMateria == id);
            if (materias == null)
            {
                return NotFound();
            }

            return View(materias);
        }

        // POST: Funcionario/FuncionariosMaterias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var materias = await _context.Materia.FindAsync(id);
            if (materias != null)
            {
                _context.Materia.Remove(materias);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MateriasExists(int id)
        {
            return _context.Materia.Any(e => e.IdMateria == id);
        }
    }
}
