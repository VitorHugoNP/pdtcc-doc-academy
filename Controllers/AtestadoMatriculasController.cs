using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iText.Kernel.Pdf;
using iText.Layout.Element;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using pdtcc_doc_academy.Models;
using pdtcc_doc_academy.Repositories;

namespace pdtcc_doc_academy.Controllers
{
    public class AtestadoMatriculasController : Controller
    {
        private readonly AppDBContext _context;

        public AtestadoMatriculasController(AppDBContext context)
        {
            _context = context;
        }

        // GET: AtestadoMatriculas
        public async Task<IActionResult> Index()
        {
            return View(await _context.Atestado_Matricula.ToListAsync());
        }

        // GET: AtestadoMatriculas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var atestadoMatricula = await _context.Atestado_Matricula
                .FirstOrDefaultAsync(m => m.IdAtest_mat == id);
            if (atestadoMatricula == null)
            {
                return NotFound();
            }

            return View(atestadoMatricula);
        }

        // Ação para gerar e baixar o PDF
        [HttpGet("autorizacao/downloadPdf/{idProcolo}")]
        [Authorize(Roles = "Escola")]
        public async Task<IActionResult> DownloadComunicadoPdfAsync(int idProcolo)
        {
            // Buscando a autorização pelo ID do protocolo
            Atestado_Matricula atestado_Matricula = await _context.Atestado_Matricula.FirstOrDefaultAsync(a => a.fk_prot == idProcolo);

            // Verifica se a autorização foi encontrada
            if (atestado_Matricula == null)
            {
                return NotFound("Autorização não encontrada.");
            }

            Protocolo protocolo = await _context.Protocolo.FirstOrDefaultAsync(p => p.idProtocolo == atestado_Matricula.fk_prot);

            // Verifica se o protocolo foi encontrado
            if (protocolo == null)
            {
                return NotFound("Protocolo não encontrado.");
            }

            Alunos alunos = await _context.aluno.FirstOrDefaultAsync(x => x.idAluno == protocolo.fk_aluno);

            // Verifica se o aluno foi encontrado
            if (alunos == null)
            {
                return NotFound("Aluno não encontrado.");
            }

            // Preenchendo o ViewModel
            var viewModel = new AlunoAtestadoMatricula
            {
                idAluno = alunos.idAluno,
                nomeAluno = alunos.nomeAluno,
                cpfAluno = alunos.cpfAluno,
                rgAluno = alunos.rgAluno,
                rmAluno = alunos.rmAluno,
                IdAtest_mat = atestado_Matricula.IdAtest_mat
            };

            using (var stream = new MemoryStream())
            {
                // Criação do PDF
                using (var writer = new PdfWriter(stream))
                {
                    using (var pdf = new PdfDocument(writer))
                    {
                        var document = new Document(pdf);
                        document.Add(new Paragraph("Documento de Autorização"));
                        document.Add(new Paragraph($"ID do Aluno: {viewModel.idAluno}"));
                        document.Add(new Paragraph($"Nome: {viewModel.nomeAluno}"));
                        document.Add(new Paragraph($"CPF: {viewModel.cpfAluno}"));
                        document.Add(new Paragraph($"RG: {viewModel.rgAluno}"));
                        document.Add(new Paragraph($"RM: {viewModel.rmAluno}"));
                        document.Add(new Paragraph($"ID da Autorização: {viewModel.IdAtest_mat}"));
                    }
                }

                // Retorne o PDF como um arquivo
                var fileName = $"Autorizacao_{viewModel.idAluno}.pdf";
                return File(stream.ToArray(), "application/pdf", fileName);
            }
        }

        // GET: AtestadoMatriculas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var atestadoMatricula = await _context.Atestado_Matricula.FindAsync(id);
            if (atestadoMatricula == null)
            {
                return NotFound();
            }
            return View(atestadoMatricula);
        }

        // POST: AtestadoMatriculas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdAtest_mat")] Atestado_Matricula atestadoMatricula)
        {
            if (id != atestadoMatricula.IdAtest_mat)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(atestadoMatricula);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AtestadoMatriculaExists(atestadoMatricula.IdAtest_mat))
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
            return View(atestadoMatricula);
        }

        // GET: AtestadoMatriculas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var atestadoMatricula = await _context.Atestado_Matricula
                .FirstOrDefaultAsync(m => m.IdAtest_mat == id);
            if (atestadoMatricula == null)
            {
                return NotFound();
            }

            return View(atestadoMatricula);
        }

        // POST: AtestadoMatriculas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var atestadoMatricula = await _context.Atestado_Matricula.FindAsync(id);
            if (atestadoMatricula != null)
            {
                _context.Atestado_Matricula.Remove(atestadoMatricula);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AtestadoMatriculaExists(int id)
        {
            return _context.Atestado_Matricula.Any(e => e.IdAtest_mat == id);
        }
    }
}
