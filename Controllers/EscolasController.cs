using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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
            //return View(await _context.Escola.ToListAsync());

            // Obtém o ID da escola a partir das claims do usuário autenticado
            var idEscolaClaim = User.Claims.FirstOrDefault(c => c.Type == "idEscola")?.Value;

            if (idEscolaClaim == null)
            {
                return NotFound("Escola não encontrada.");
            }

            // Converte o ID da escola para int
            int idEscola = int.Parse(idEscolaClaim);

            // Busca a escola logada no banco de dados
            var escola = await _context.Escola
                .FirstOrDefaultAsync(e => e.idEscola == idEscola);

            if (escola == null)
            {
                return NotFound("Escola não encontrada.");
            }

            return View(escola); // Retorna a view com a escola logada
        }

        // GET: Protocolos
        public async Task<IActionResult> VerProtocolos()
        {
            var protocolos = await _context.Protocolo
                .Include(p => p.aluno)
                .Include(p => p.funcionario)
                .ToListAsync();

            return View(protocolos); // Passa a lista de protocolos para a view
        }

        // GET: Escolas/Details/5
        public IActionResult Details(int id)
        {
            var escola = _context.Escola.Find(id); // Obtém a escola específica pelo ID
            if (escola == null)
            {
                return NotFound();
            }
            return View(escola); // Passa o objeto escola para a view
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

        // GET: Schools/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var escola = await _context.Escola.FindAsync(id);
            if (escola == null)
            {
                return NotFound();
            }
            return View(escola);
        }

        // POST: Schools/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("idEscola,nomeEscola,enderecoEscola,emailEscola,senhaEscola")] Escola escola)
        {
            if (id != escola.idEscola)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(escola);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EscolaExists(escola.idEscola))
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
            return View(escola);
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

        private bool EscolaExists(int id)
        {
            return _context.Escola.Any(e => e.idEscola == id);
        }

        // GET: Escolas/Alunos
        public async Task<IActionResult> ListarAlunos()
        {
            var alunos = await _context.aluno.ToListAsync();
            return View(alunos);
        }

        // GET: Alunos/Edit/5
        public async Task<IActionResult> EditAluno(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aluno = await _context.aluno.FindAsync(id);
            if (aluno == null)
            {
                return NotFound();
            }

            // Obter listas de cursos e séries
            ViewBag.Cursos = await _context.Curso.ToListAsync();
            ViewBag.Series = await _context.Serie.ToListAsync();

            // Obter o alunoCurso e alunoSerie para preencher os campos
            var alunoCurso = await _context.aluno_curso.FirstOrDefaultAsync(ac => ac.fk_aluno == aluno.idAluno);
            var alunoSerie = await _context.aluno_serie.FirstOrDefaultAsync(ase => ase.fk_aluno == aluno.idAluno);

            // Preencher as informações do aluno com o curso e série
            ViewBag.CursoId = alunoCurso.fk_curso;
            ViewBag.SerieId = alunoSerie.fk_serie;

            return View(aluno);
        }

        // POST: Escolas/EditAluno/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Escola")]
        public async Task<IActionResult> EditAluno(int id, [Bind("idAluno,nomeAluno,cpfAluno,rgAluno,rmAluno,emailAluno,senhaAluno")] Alunos alunos, int cursoId, int serieId)
        {
            if (id != alunos.idAluno)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Atualiza as informações do usuário
                    var usuario = await _context.Usuario.FindAsync(alunos.fk_usuario);
                    if (usuario != null)
                    {
                        usuario.emailUsuario = alunos.emailAluno;
                        usuario.senhaUsuario = alunos.senhaAluno; // Considere hash da senha
                        _context.Update(usuario);
                    }

                    // Atualiza as informações do aluno
                    _context.Update(alunos);

                    // Atualiza a associação do aluno ao curso
                    var alunoCurso = await _context.aluno_curso
                        .FirstOrDefaultAsync(ac => ac.fk_aluno == alunos.idAluno);
                    if (alunoCurso != null)
                    {
                        alunoCurso.fk_curso = cursoId; // Atualiza o ID do curso
                        _context.Update(alunoCurso);
                    }

                    // Atualiza a associação do aluno à série
                    var alunoSerie = await _context.aluno_serie
                        .FirstOrDefaultAsync(ase => ase.fk_aluno == alunos.idAluno);
                    if (alunoSerie != null)
                    {
                        alunoSerie.fk_serie = serieId; // Atualiza o ID da série
                        _context.Update(alunoSerie);
                    }

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
                return RedirectToAction("Index", "Escolas");
            }
            return View(alunos);
        }

        private bool AlunosExists(int id)
        {
            return _context.aluno.Any(e => e.idAluno == id);
        }

    }
}
