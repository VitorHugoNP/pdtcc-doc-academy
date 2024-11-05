using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using pdtcc_doc_academy.Models;
using pdtcc_doc_academy.Repositories;
using static iText.StyledXmlParser.Jsoup.Select.Evaluator;
using Org.BouncyCastle.Security;

namespace pdtcc_doc_academy.Controllers
{
    public class ComunicadosController : Controller
    {
        private readonly AppDBContext _context;

        public ComunicadosController(AppDBContext context)
        {
            _context = context;
        }

        // GET: Comunicados
        public async Task<IActionResult> Index()
        {
            return View(await _context.Comunicado.ToListAsync());
        }

        // GET: Comunicados/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comunicados = await _context.Comunicado
                .FirstOrDefaultAsync(m => m.idComunicados == id);
            if (comunicados == null)
            {
                return NotFound();
            }

            return View(comunicados);
        }

        // GET: Comunicados/Create
        // Ação para gerar e baixar o PDF
        [HttpGet("autorizacao/downloadPdf/{idProcolo}")]
        [Authorize(Roles = "Escola")]
        public async Task<IActionResult> DownloadComunicadoPdfAsync(int idProcolo)
        {
            // Buscando a autorização pelo ID do protocolo
            Comunicados comunicado = await _context.Comunicado.FirstOrDefaultAsync(a => a.fk_prot == idProcolo);

            // Verifica se a autorização foi encontrada
            if (comunicado == null)
            {
                return NotFound("Autorização não encontrada.");
            }

            Protocolo protocolo = await _context.Protocolo.FirstOrDefaultAsync(p => p.idProtocolo == comunicado.fk_prot);

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
            var viewModel = new AlunoComunicado
            {
                idAluno = alunos.idAluno,
                nomeAluno = alunos.nomeAluno,
                cpfAluno = alunos.cpfAluno,
                rgAluno = alunos.rgAluno,
                rmAluno = alunos.rmAluno,
                idComunicados = comunicado.idComunicados,
                data_comunicado = comunicado.data_comunicado // Não precisa do operador de coalescência aqui, pois já verificamos que autorizacao não é null
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
                        document.Add(new Paragraph($"ID da Autorização: {viewModel.idComunicados}"));
                        document.Add(new Paragraph($"Data da Autorização: {viewModel.data_comunicado.ToString("dd/MM/yyyy") ?? "N/A"}"));
                    }
                }

                // Retorne o PDF como um arquivo
                var fileName = $"Autorizacao_{viewModel.idAluno}.pdf";
                return File(stream.ToArray(), "application/pdf", fileName);
            }
        }

        // GET: Comunicados/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comunicados = await _context.Comunicado.FindAsync(id);
            if (comunicados == null)
            {
                return NotFound();
            }
            return View(comunicados);
        }

        // POST: Comunicados/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("idComunicados,data_comunicado,fk_Prot")] Comunicados comunicados)
        {
            if (id != comunicados.idComunicados)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(comunicados);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ComunicadosExists(comunicados.idComunicados))
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
            return View(comunicados);
        }

        // GET: Comunicados/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comunicados = await _context.Comunicado
                .FirstOrDefaultAsync(m => m.idComunicados == id);
            if (comunicados == null)
            {
                return NotFound();
            }

            return View(comunicados);
        }

        // POST: Comunicados/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var comunicados = await _context.Comunicado.FindAsync(id);
            if (comunicados != null)
            {
                _context.Comunicado.Remove(comunicados);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ComunicadosExists(int id)
        {
            return _context.Comunicado.Any(e => e.idComunicados == id);
        }
    }
}
