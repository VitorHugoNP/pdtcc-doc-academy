using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Crypto.Generators;
using pdtcc_doc_academy.Models;
using pdtcc_doc_academy.Repositories;

namespace pdtcc_doc_academy.Controllers
{
    public class EscolasController : Controller
    {
        private readonly AppDBContext _context;

        public EscolasController(AppDBContext context)
        {
            _context = context;
        }

        // GET: Escolas
        public async Task<IActionResult> Index()
        {
            return View(await _context.Escola.ToListAsync());
        }

        // GET: Escolas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var escolas = await _context.Escola
                .FirstOrDefaultAsync(m => m.idEscola == id);
            if (escolas == null)
            {
                return NotFound();
            }

            return View(escolas);
        }

        // GET: Escolas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("idEscola,nomeEscola,enderecoEscola,emailEscola,senhaEscola")] Escola escola)
        {
            if (ModelState != null)
            {
                var usuario = new Usuario // pega os dados para acesso e salva na tabela usuario já com o tipo especifico
                {
                    emailUsuario = escola.emailEscola,
                    senhaUsuario = escola.senhaEscola,
                    tipoUsuario = "Escola"
                };
                _context.Add(usuario);
                var result = await _context.SaveChangesAsync();
                var escolas = new Escola
                {
                    nomeEscola = escola.nomeEscola,
                    enderecoEscola = escola.enderecoEscola,
                    emailEscola = escola.emailEscola,
                    senhaEscola = escola.senhaEscola,
                    fk_usuario = usuario.idUsuario
                };

                _context.Add(escolas);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(escola);
        }

        // GET: Escolas/Edit/5
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

        // POST: Escolas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("idEscola,nomeEscola,enderecoEscola,emailEscola,senhaEscola")] Escola escolas)
        {
            if (id != escolas.idEscola)
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
                    if (!EscolasExists(escolas.idEscola))
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

        // GET: Escolas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var escolas = await _context.Escola
                .FirstOrDefaultAsync(m => m.idEscola == id);
            if (escolas == null)
            {
                return NotFound();
            }

            return View(escolas);
        }

        // POST: Escolas/Delete/5
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
            return _context.Escola.Any(e => e.idEscola == id);
        }
    }
}
