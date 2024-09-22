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
    public class FuncionariosGradeCurricularsController : Controller
    {
        private readonly AppDBContext _context;

        public FuncionariosGradeCurricularsController(AppDBContext context)
        {
            _context = context;
        }

        // GET: Funcionario/FuncionariosGradeCurriculars
        public async Task<IActionResult> Index()
        {
            return View(await _context.GradeCurricular.ToListAsync());
        }

        // GET: Funcionario/FuncionariosGradeCurriculars/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gradeCurricular = await _context.GradeCurricular
                .FirstOrDefaultAsync(m => m.IdGradeCurricular == id);
            if (gradeCurricular == null)
            {
                return NotFound();
            }

            return View(gradeCurricular);
        }

        // GET: Funcionario/FuncionariosGradeCurriculars/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Funcionario/FuncionariosGradeCurriculars/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdGradeCurricular")] GradeCurricular gradeCurricular)
        {
            if (ModelState.IsValid)
            {
                _context.Add(gradeCurricular);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(gradeCurricular);
        }

        // GET: Funcionario/FuncionariosGradeCurriculars/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gradeCurricular = await _context.GradeCurricular.FindAsync(id);
            if (gradeCurricular == null)
            {
                return NotFound();
            }
            return View(gradeCurricular);
        }

        // POST: Funcionario/FuncionariosGradeCurriculars/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdGradeCurricular")] GradeCurricular gradeCurricular)
        {
            if (id != gradeCurricular.IdGradeCurricular)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gradeCurricular);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GradeCurricularExists(gradeCurricular.IdGradeCurricular))
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
            return View(gradeCurricular);
        }

        // GET: Funcionario/FuncionariosGradeCurriculars/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gradeCurricular = await _context.GradeCurricular
                .FirstOrDefaultAsync(m => m.IdGradeCurricular == id);
            if (gradeCurricular == null)
            {
                return NotFound();
            }

            return View(gradeCurricular);
        }

        // POST: Funcionario/FuncionariosGradeCurriculars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var gradeCurricular = await _context.GradeCurricular.FindAsync(id);
            if (gradeCurricular != null)
            {
                _context.GradeCurricular.Remove(gradeCurricular);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GradeCurricularExists(int id)
        {
            return _context.GradeCurricular.Any(e => e.IdGradeCurricular == id);
        }
    }
}
