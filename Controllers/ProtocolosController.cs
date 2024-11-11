using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using iText.Kernel.Pdf;
using iText.Layout.Element;
using iText.Layout;
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
        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var protocolo = await _context.Protocolo
                .Include(p => p.fk_aluno == id)
                .Include(p => p.fk_func == id)
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int selectedOption, int idFuncionario, int idAluno)
        {
            // Busca os IDs de funcionário a partir das claims
            var claimFuncionarioId = User.Claims.FirstOrDefault(c => c.Type == "FuncionarioId");
            int idFunc = claimFuncionarioId != null ? int.Parse(claimFuncionarioId.Value) : 1;

            // Verifica se o aluno existe
            var aluno = await _context.aluno.FindAsync(idAluno);
            if (aluno == null)
            {
                ModelState.AddModelError("", "Aluno não encontrado.");
                return RedirectToAction("Index");
            }

            // Lógica para lidar com o tipo de documento selecionado
            switch (selectedOption)
            {
                case 1: // Atestado de Matrícula
                    return await HandleAtestadoMatricula(idFunc, idAluno);
                case 2: // Autorização
                    return await HandleAutorizacao(idFunc, idAluno);
                case 3: // Comunicado
                    return await HandleComunicado(idFunc, idAluno);
                default:
                    ModelState.AddModelError("", "Opção inválida.");
                    return RedirectToAction("Index");
            }
        }

        private async Task<IActionResult> HandleAtestadoMatricula(int idFuncionario, int idAluno)
        {
            var funcionario = await _context.Funcionario.FindAsync(idFuncionario);
            var aluno = await _context.aluno.FindAsync(idAluno);

            var protocolo = new Protocolo
            {
                tipo_Doc = "Atestado Matricula",
                fk_aluno = aluno?.idAluno ?? 1, // Use 1 se aluno for null
                fk_func = funcionario?.idFuncionario ?? 1 // Use 1 se funcionário for null
            };

            _context.Add(protocolo);
            await _context.SaveChangesAsync();

            var atestadoMatricula = new Atestado_Matricula
            {
                fk_prot = protocolo.idProtocolo,
            };
            _context.Add(atestadoMatricula);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Alunos");
        }

        private async Task<IActionResult> HandleAutorizacao(int idFuncionario, int idAluno)
        {
            var funcionario = await _context.Funcionario.FindAsync(idFuncionario);
            var aluno = await _context.aluno.FindAsync(idAluno);

            var protocolo = new Protocolo
            {
                tipo_Doc = "Autorizacao",
                fk_aluno = aluno?.idAluno ?? 1, // Use 1 se aluno for null
                fk_func = funcionario?.idFuncionario ?? 1 // Use 1 se funcionário for null
            };

            _context.Add(protocolo);
            await _context.SaveChangesAsync();

            var autorizacao = new Autorizacao
            {
                fk_prot = protocolo.idProtocolo,
                data_aut = DateTime.UtcNow
            };
            _context.Add(autorizacao);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Alunos");
        }

        private async Task<IActionResult> HandleComunicado(int idFuncionario, int idAluno)
        {
            var funcionario = await _context.Funcionario.FindAsync(idFuncionario);
            var aluno = await _context.aluno.FindAsync(idAluno);

            var protocolo = new Protocolo
            {
                tipo_Doc = "Comunicado",
                fk_aluno = aluno?.idAluno ?? 1, // Use 1 se aluno for null
                fk_func = funcionario?.idFuncionario ?? 1 // Use 1 se funcionário for null
            };

            _context.Add(protocolo);
            await _context.SaveChangesAsync();

            var comunicados = new Comunicados
            {
                fk_prot = protocolo.idProtocolo,
                data_comunicado = DateTime.UtcNow
            };
            _context.Add(comunicados);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Alunos");
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

        // Download PDF
        [HttpGet("downloadPdf/{idProcolo}")]
        public async Task<IActionResult> DownloadPdfAsync(int idProcolo)
        {
            Protocolo protocolo = await _context.Protocolo.FirstOrDefaultAsync(p => p.idProtocolo == idProcolo);

            if (protocolo == null)
            {
                return NotFound("Protocolo não encontrado.");
            }

            var claimFuncionarioId = User.Claims.FirstOrDefault(c => c.Type == "FuncionarioId");
            var claimAlunoId = User.Claims.FirstOrDefault(c => c.Type == "AlunoId");

            switch (protocolo.tipo_Doc)
            {
                case "Autorizacao":
                    return await DownloadAutorizacaoPdfAsync(idProcolo);
                case "Comunicado":
                    return await DownloadComunicadoPdfAsync(idProcolo);
                case "Atestado Matricula":
                    return await DownloadAtestadoMatriculaPdfAsync(idProcolo);
                default:
                    return NotFound("Tipo de documento não suportado.");
            }
        }

        // AUTORIZAÇÃO
        [HttpGet("autorizacao/downloadPdf/{idProcolo}")]
        public async Task<IActionResult> DownloadAutorizacaoPdfAsync(int idProcolo)
        {
            Autorizacao autorizacao = await _context.Autorizacao.FirstOrDefaultAsync(a => a.fk_prot == idProcolo);
            Protocolo protocolo = await _context.Protocolo.FirstOrDefaultAsync(p => p.idProtocolo == autorizacao.fk_prot);

            if (protocolo == null)
            {
                return NotFound("Protocolo não encontrado.");
            }

            Alunos alunos = await _context.aluno.FirstOrDefaultAsync(x => x.idAluno == protocolo.fk_aluno);
            Funcionario funcionario = await _context.Funcionario.FirstOrDefaultAsync(f => f.idFuncionario == protocolo.fk_func);

            var viewModel = new AlunoAutorizacao
            {
                nomeAluno = alunos.nomeAluno,
                cpfAluno = alunos.cpfAluno,
                rgAluno = alunos.rgAluno,
                rmAluno = alunos.rmAluno,
                idAutorizacao = autorizacao.idAutorizacao,
                data_aut = autorizacao.data_aut
            };

            using (var stream = new MemoryStream())
            {
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
                        document.Add(new Paragraph($"ID da Autorização: {viewModel.idAutorizacao}"));
                        document.Add(new Paragraph($"Data da Autorização: {viewModel.data_aut?.ToString("dd/MM/yyyy") ?? "N/A"}"));
                    }
                }

                var fileName = $"Autorizacao_{viewModel.idAluno}.pdf";
                return File(stream.ToArray(), "application/pdf", fileName);
            }
        }

        // COMUNICADOS
        [HttpGet("comunicado/downloadPdf/{idProcolo}")]
        public async Task<IActionResult> DownloadComunicadoPdfAsync(int idProcolo)
        {
            Comunicados comunicado = await _context.Comunicados.FirstOrDefaultAsync(a => a.fk_prot == idProcolo);

            if (comunicado == null)
            {
                return NotFound("Comunicado não encontrado.");
            }

            Protocolo protocolo = await _context.Protocolo.FirstOrDefaultAsync(p => p.idProtocolo == comunicado.fk_prot);
            Alunos alunos = await _context.aluno.FirstOrDefaultAsync(x => x.idAluno == protocolo.fk_aluno);

            var viewModel = new AlunoComunicado
            {
                idAluno = alunos.idAluno,
                nomeAluno = alunos.nomeAluno,
                cpfAluno = alunos.cpfAluno,
                rgAluno = alunos.rgAluno,
                rmAluno = alunos.rmAluno,
                idComunicados = comunicado.idComunicados,
                data_comunicado = comunicado.data_comunicado
            };

            using (var stream = new MemoryStream())
            {
                using (var writer = new PdfWriter(stream))
                {
                    using (var pdf = new PdfDocument(writer))
                    {
                        var document = new Document(pdf);
                        document.Add(new Paragraph("Documento de Comunicado"));
                        document.Add(new Paragraph($"ID do Aluno: {viewModel.idAluno}"));
                        document.Add(new Paragraph($"Nome: {viewModel.nomeAluno}"));
                        document.Add(new Paragraph($"CPF: {viewModel.cpfAluno}"));
                        document.Add(new Paragraph($"RG: {viewModel.rgAluno}"));
                        document.Add(new Paragraph($"RM: {viewModel.rmAluno}"));
                        document.Add(new Paragraph($"ID do Comunicado: {viewModel.idComunicados}"));
                        document.Add(new Paragraph($"Data do Comunicado: {viewModel.data_comunicado.ToString("dd/MM/yyyy") ?? "N/A"}"));
                    }
                }
                var fileName = $"Comunicado_{viewModel.idAluno}.pdf";
                return File(stream.ToArray(), "application/pdf", fileName);
            }
        }

        // ATESTADO ```csharp
        // ATESTADO DE MATRICULA
        [HttpGet("atestado/downloadPdf/{idProcolo}")]
        public async Task<IActionResult> DownloadAtestadoMatriculaPdfAsync(int idProcolo)
        {
            Atestado_Matricula atestado_Matricula = await _context.Atestado_Matricula.FirstOrDefaultAsync(a => a.fk_prot == idProcolo);

            if (atestado_Matricula == null)
            {
                return NotFound("Atestado não encontrado.");
            }

            Protocolo protocolo = await _context.Protocolo.FirstOrDefaultAsync(p => p.idProtocolo == atestado_Matricula.fk_prot);
            Alunos alunos = await _context.aluno.FirstOrDefaultAsync(x => x.idAluno == protocolo.fk_aluno);

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
                using (var writer = new PdfWriter(stream))
                {
                    using (var pdf = new PdfDocument(writer))
                    {
                        var document = new Document(pdf);
                        document.Add(new Paragraph("Documento de Atestado Matricula"));
                        document.Add(new Paragraph($"ID do Aluno: {viewModel.idAluno}"));
                        document.Add(new Paragraph($"Nome: {viewModel.nomeAluno}"));
                        document.Add(new Paragraph($"CPF: {viewModel.cpfAluno}"));
                        document.Add(new Paragraph($"RG: {viewModel.rgAluno}"));
                        document.Add(new Paragraph($"RM: {viewModel.rmAluno}"));
                        document.Add(new Paragraph($"ID do Atestado: {viewModel.IdAtest_mat}"));
                    }
                }

                var fileName = $"Atestado_Matriculas_{viewModel.idAluno}.pdf";
                return File(stream.ToArray(), "application/pdf", fileName);
            }
        }
    }
}