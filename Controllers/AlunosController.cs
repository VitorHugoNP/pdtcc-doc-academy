using System;
using System.Collections.Generic;
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
    public class AlunosController : Controller
    {
        private readonly AppDBContext _context;

        public AlunosController(AppDBContext context)
        {
            _context = context;
        }

        // GET: Alunos
        public async Task<IActionResult> Index()
        {
            return View(await _context.aluno.ToListAsync());
        }

        // GET: Alunos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var alunos = await _context.aluno
                .FirstOrDefaultAsync(m => m.idAluno == id);
            if (alunos == null)
            {
                return NotFound();
            }

            return View(alunos);
        }

        // GET: Alunos/Create
        public async Task<IActionResult> CreateAluno()
        {
            ViewBag.Curso = await _context.Curso.ToListAsync();
            return View();
        }

        // POST: Alunos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Escola")]
        public async Task<IActionResult> CreateAluno([Bind("idAluno,nomeAluno,cpfAluno,rgAluno,rmAluno,emailAluno,senhaAluno")] Alunos alunos, int cursoId)
        {
            if (ModelState != null)
            {
                var usuario = new Usuario // pega os dados para acesso e salva na tabela usuario já com o tipo especifico
                {
                    emailUsuario = alunos.emailAluno,
                    senhaUsuario = alunos.senhaAluno,
                    tipoUsuario = "Aluno"
                };
                _context.AddAsync(usuario);
                var result = await _context.SaveChangesAsync();
                var aluno = new Alunos
                {
                    cpfAluno = alunos.cpfAluno,
                    emailAluno = alunos.emailAluno,
                    senhaAluno = alunos.senhaAluno,
                    nomeAluno = alunos.nomeAluno,
                    rgAluno = alunos.rgAluno,
                    rmAluno = alunos.rmAluno,                    
                    fk_usuario = usuario.idUsuario
                };
                _context.AddAsync(aluno);
                await _context.SaveChangesAsync();

                // Associar o aluno ao curso
                var alunoCurso = new AlunoCurso
                {
                    fk_aluno = aluno.idAluno, // ID do aluno que foi criado
                    fk_curso = cursoId // ID do curso selecionado
                };

                _context.aluno_curso.AddAsync(alunoCurso);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(alunos);
        }

        // GET: Alunos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var alunos = await _context.aluno.FindAsync(id);
            if (alunos == null)
            {
                return NotFound();
            }
            return View(alunos);
        }

        // POST: Alunos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("idAluno,nomeAluno,cpfAluno,rgAluno,rmAluno,emailAluno,senhaAluno")] Alunos alunos)
        {
            if (id != alunos.idAluno)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(alunos);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AlunosExists(alunos.idAluno))
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
            return View(alunos);
        }

        // GET: Alunos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var alunos = await _context.aluno
                .FirstOrDefaultAsync(m => m.idAluno == id);
            if (alunos == null)
            {
                return NotFound();
            }

            return View(alunos);
        }

        // POST: Alunos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var alunos = await _context.aluno.FindAsync(id);
            if (alunos != null)
            {
                _context.aluno.Remove(alunos);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AlunosExists(int id)
        {
            return _context.aluno.Any(e => e.idAluno == id);
        }
    }
}
