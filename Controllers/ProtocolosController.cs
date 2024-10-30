using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using pdtcc_doc_academy.Models;
using pdtcc_doc_academy.Repositories;

namespace pdtcc_doc_academy.Controllers
{
    public class ProtocolosController : Controller
    {
        private readonly AppDBContext _context;

        public ProtocolosController(AppDBContext context)
        {
            _context = context;
        }

        // GET: Protocolos
        public async Task<IActionResult> Index()
        {
            var appDBContext = _context.Protocolo.Include(p => p.aluno).Include(p => p.funcionario);
            return View(await appDBContext.ToListAsync());
        }

        // GET: Protocolos/Details/5
        public async Task<IActionResult> Details(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var protocolo = await _context.Protocolo
                .Include(p => p.aluno)
                .Include(p => p.funcionario)
                .FirstOrDefaultAsync(m => m.idProtocolo == id);
            if (protocolo == null)
            {
                return NotFound();
            }

            return View(protocolo);
        }

        // GET: Protocolos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Protocolos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Escola")] // Somente Alunos podem criar protocolos
        public async Task<IActionResult> Create(int selectedOption, int idFuncionario)
        {
            // Buscar o ID do aluno a partir das claims
            var claimAlunoId = User.Claims.FirstOrDefault(c => c.Type == "AlunoId");

            if (claimAlunoId == null)
            {
                return Unauthorized("Você precisa estar logado como aluno para criar um protocolo.");
            }

            // Converte o valor do claim para int (já que o ID do aluno é um int)
            int idAluno = int.Parse(claimAlunoId.Value);

            switch (selectedOption)
            {
                case 1:
                    return await HandleAtestadoMatricula(idFuncionario, idAluno);
                case 2:
                    return await HandleAutorizacao(idFuncionario, idAluno);
                case 3:
                    return await HandleComunicado(idFuncionario, idAluno);
                default:
                    ModelState.AddModelError("", "Opção inválida.");
                    return RedirectToAction("Index");
            }
        }


        private async Task<IActionResult> HandleAtestadoMatricula(int idFuncionario, int idAluno)
        {
            var funcionario = await _context.Funcionario.FindAsync(idFuncionario);
            var aluno = await _context.aluno.FindAsync(idAluno);

            if (aluno == null)
            {
                ModelState.AddModelError("", "Funcionário ou Aluno não encontrado");
                return View();
            }

            var protocolo = new Protocolo
            {
                tipo_Doc = "Atestado Matricula",
                fk_aluno = aluno.idAluno,
                fk_func = 1
            };

            _context.Add(protocolo);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Alunos", protocolo);
        }


        private async Task<IActionResult> HandleAutorizacao(int idFuncionario, int idAluno)
        {

            var funcionario = await _context.Funcionario.FindAsync(idFuncionario);
            var aluno = await _context.aluno.FindAsync(idAluno);
            if (funcionario == null || aluno == null)
            {
                ModelState.AddModelError("", "Funcionario não encontrado");
                return View();
            }

            var protocolo = new Protocolo
            {
                tipo_Doc = "Autorização",
                fk_aluno = aluno.idAluno,
                fk_func = 1
            };
            _context.Add(protocolo);
            await _context.SaveChangesAsync();

            return View("AutorizacaoView");
        }

        private async Task<IActionResult> HandleComunicado(int idFuncionario, int idAluno)
        {

            var funcionario = await _context.Funcionario.FindAsync(idFuncionario);
            var aluno = await _context.aluno.FindAsync(idAluno);
            if (funcionario == null || aluno == null)
            {
                ModelState.AddModelError("", "Funcionario não encontrado");
                return View();
            }

            var protocolo = new Protocolo
            {
                tipo_Doc = "Comunicado",
                fk_aluno = aluno.idAluno,
                fk_func = 1
            };
             _context.Add(protocolo);
            await _context.SaveChangesAsync();

            return View("ComunicadoView");
        }





        // GET: Protocolos/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var protocolo = await _context.Protocolo.FindAsync(id);
            if (protocolo == null)
            {
                return NotFound();
            }
            ViewData["fk_aluno"] = new SelectList(_context.aluno, "idAluno", "emailAluno", protocolo.fk_aluno);
            ViewData["fk_func"] = new SelectList(_context.Funcionario, "IdFuncionario", "email_func", protocolo.fk_func);
            return View(protocolo);
        }

        // POST: Protocolos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("idProtocolo,fk_aluno,fk_func,tipo_Doc")] Protocolo protocolo)
        {
            if (id != protocolo.idProtocolo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(protocolo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProtocoloExists(protocolo.idProtocolo))
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
            ViewData["fk_aluno"] = new SelectList(_context.aluno, "idAluno", "emailAluno", protocolo.fk_aluno);
            ViewData["fk_func"] = new SelectList(_context.Funcionario, "IdFuncionario", "email_func", protocolo.fk_func);
            return View(protocolo);
        }

        // GET: Protocolos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var protocolo = await _context.Protocolo
                .Include(p => p.aluno)
                .Include(p => p.funcionario)
                .FirstOrDefaultAsync(m => m.idProtocolo == id);
            if (protocolo == null)
            {
                return NotFound();
            }

            return View(protocolo);
        }

        // POST: Protocolos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var protocolo = await _context.Protocolo.FindAsync(id);
            if (protocolo != null)
            {
                _context.Protocolo.Remove(protocolo);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProtocoloExists(int id)
        {
            return _context.Protocolo.Any(e => e.idProtocolo == id);
        }
    }



}