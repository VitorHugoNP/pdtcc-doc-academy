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
    public class FuncionariosEscolasController : Controller
    {
        private readonly AppDBContext _context;

        public FuncionariosEscolasController(AppDBContext context)
        {
            _context = context;
        }

        // GET: Funcionario/FuncionariosEscolas
        public async Task<IActionResult> Index()
        {
            return View(await _context.Escola.ToListAsync());
        }

        // GET: Funcionario/FuncionariosEscolas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var escolas = await _context.Escola
                .FirstOrDefaultAsync(m => m.IdEscola == id);
            if (escolas == null)
            {
                return NotFound();
            }

            return View(escolas);
        }

        // GET: Funcionario/FuncionariosEscolas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Funcionario/FuncionariosEscolas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdEscola,nomeEscola,enderecoEscola,emailEscola,telefoneEscola")] Escolas escolas)
        {
            if (ModelState.IsValid)
            {
                _context.Add(escolas);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(escolas);
        }

        // GET: Funcionario/FuncionariosEscolas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var escolas = await _context.Escola.FindAsync(id);
            if (escolas == null)
            {
                return NotFound();
            }
            return View(escolas);
        }

        // POST: Funcionario/FuncionariosEscolas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdEscola,nomeEscola,enderecoEscola,emailEscola,telefoneEscola")] Escolas escolas)
        {
            if (id != escolas.IdEscola)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(escolas);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EscolasExists(escolas.IdEscola))
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
            return View(escolas);
        }

        // GET: Funcionario/FuncionariosEscolas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var escolas = await _context.Escola
                .FirstOrDefaultAsync(m => m.IdEscola == id);
            if (escolas == null)
            {
                return NotFound();
            }

            return View(escolas);
        }

        // POST: Funcionario/FuncionariosEscolas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var escolas = await _context.Escola.FindAsync(id);
            if (escolas != null)
            {
                _context.Escola.Remove(escolas);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EscolasExists(int id)
        {
            return _context.Escola.Any(e => e.IdEscola == id);
        }
    }
}
