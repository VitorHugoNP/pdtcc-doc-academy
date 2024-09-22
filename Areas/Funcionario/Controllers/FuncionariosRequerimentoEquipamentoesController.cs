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
    public class FuncionariosRequerimentoEquipamentoesController : Controller
    {
        private readonly AppDBContext _context;

        public FuncionariosRequerimentoEquipamentoesController(AppDBContext context)
        {
            _context = context;
        }

        // GET: Funcionario/FuncionariosRequerimentoEquipamentoes
        public async Task<IActionResult> Index()
        {
            return View(await _context.RequerimentoEquipamento.ToListAsync());
        }

        // GET: Funcionario/FuncionariosRequerimentoEquipamentoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var requerimentoEquipamento = await _context.RequerimentoEquipamento
                .FirstOrDefaultAsync(m => m.IdReq == id);
            if (requerimentoEquipamento == null)
            {
                return NotFound();
            }

            return View(requerimentoEquipamento);
        }

        // GET: Funcionario/FuncionariosRequerimentoEquipamentoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Funcionario/FuncionariosRequerimentoEquipamentoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdReq,dataReq")] RequerimentoEquipamento requerimentoEquipamento)
        {
            if (ModelState.IsValid)
            {
                _context.Add(requerimentoEquipamento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(requerimentoEquipamento);
        }

        // GET: Funcionario/FuncionariosRequerimentoEquipamentoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var requerimentoEquipamento = await _context.RequerimentoEquipamento.FindAsync(id);
            if (requerimentoEquipamento == null)
            {
                return NotFound();
            }
            return View(requerimentoEquipamento);
        }

        // POST: Funcionario/FuncionariosRequerimentoEquipamentoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdReq,dataReq")] RequerimentoEquipamento requerimentoEquipamento)
        {
            if (id != requerimentoEquipamento.IdReq)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(requerimentoEquipamento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RequerimentoEquipamentoExists(requerimentoEquipamento.IdReq))
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
            return View(requerimentoEquipamento);
        }

        // GET: Funcionario/FuncionariosRequerimentoEquipamentoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var requerimentoEquipamento = await _context.RequerimentoEquipamento
                .FirstOrDefaultAsync(m => m.IdReq == id);
            if (requerimentoEquipamento == null)
            {
                return NotFound();
            }

            return View(requerimentoEquipamento);
        }

        // POST: Funcionario/FuncionariosRequerimentoEquipamentoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var requerimentoEquipamento = await _context.RequerimentoEquipamento.FindAsync(id);
            if (requerimentoEquipamento != null)
            {
                _context.RequerimentoEquipamento.Remove(requerimentoEquipamento);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RequerimentoEquipamentoExists(int id)
        {
            return _context.RequerimentoEquipamento.Any(e => e.IdReq == id);
        }
    }
}
