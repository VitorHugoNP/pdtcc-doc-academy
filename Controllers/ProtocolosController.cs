using System;
using System.Collections.Generic;
using System.Drawing.Printing;
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
                var protocolo = new Protocolo
                {
                    tipo_Doc = "Atestado Matricula",
                    fk_aluno = 1,
                    fk_func = funcionario.idFuncionario
                };

                 _context.Add(protocolo);
                 await _context.SaveChangesAsync();

                var atestadoMatricula = new Atestado_Matricula
                {
                    fk_prot = protocolo.idProtocolo,
                };
                _context.Add(atestadoMatricula);
                await _context.SaveChangesAsync();
            } 
            else if (funcionario == null)
            {
                var protocolo = new Protocolo
                {
                    tipo_Doc = "Atestado Matricula",
                    fk_aluno = aluno.idAluno,
                    fk_func = 1
                };

                _context.Add(protocolo);
                await _context.SaveChangesAsync();

                var atestadoMatricula = new Atestado_Matricula
                {
                    fk_prot = protocolo.idProtocolo,
                };
                _context.Add(atestadoMatricula);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index", "Alunos");
        }

        private async Task<IActionResult> HandleAutorizacao(int idFuncionario, int idAluno)
        {

            var funcionario = await _context.Funcionario.FindAsync(idFuncionario);
            var aluno = await _context.aluno.FindAsync(idAluno);

            if (aluno == null)
            {
                var protocolo = new Protocolo
                {
                    tipo_Doc = "Atestado Matricula",
                    fk_aluno = 1,
                    fk_func = funcionario.idFuncionario
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

            }
            else if (funcionario == null)
            {
                var protocolo = new Protocolo
                {
                    tipo_Doc = "Atestado Matricula",
                    fk_aluno = aluno.idAluno,
                    fk_func = 1
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


            }

            return RedirectToAction("Index", "Alunos");

        }

        private async Task<IActionResult> HandleComunicado(int idFuncionario, int idAluno)
        {

            var funcionario = await _context.Funcionario.FindAsync(idFuncionario);
            var aluno = await _context.aluno.FindAsync(idAluno);
            if (aluno == null)
            {
                var protocolo = new Protocolo
                {
                    tipo_Doc = "Atestado Matricula",
                    fk_aluno = 1,
                    fk_func = funcionario.idFuncionario
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

            }
            else if (funcionario == null)
            {
                var protocolo = new Protocolo
                {
                    tipo_Doc = "Atestado Matricula",
                    fk_aluno = aluno.idAluno,
                    fk_func = 1
                };

                _context.Add(protocolo);
                await _context.SaveChangesAsync();

                var comunicado = new Comunicados
                {
                    fk_prot = protocolo.idProtocolo,
                    
                };
                _context.Add(comunicado);
                await _context.SaveChangesAsync();


            }

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










        //fazer documentos em pdf







        //AUTORIZAÇÃO
        [HttpGet("autorizacao/downloadPdf/{idProcolo}")]
        [Authorize(Roles = "Escola")]
        public async Task<IActionResult> DownloadAutorizacaoPdfAsync(int idProcolo)
        {
            // Buscando a autorização pelo ID do protocolo
            Autorizacao autorizacao = await _context.Autorizacao.FirstOrDefaultAsync(a => a.fk_prot == idProcolo);

            // Verifica se a autorização foi encontrada
            if (autorizacao == null)
            {
                return NotFound("Autorização não encontrada.");
            }

            Protocolo protocolo = await _context.Protocolo.FirstOrDefaultAsync(p => p.idProtocolo == autorizacao.fk_prot);

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
            var viewModel = new AlunoAutorizacao
            {
                idAluno = alunos.idAluno,
                nomeAluno = alunos.nomeAluno,
                cpfAluno = alunos.cpfAluno,
                rgAluno = alunos.rgAluno,
                rmAluno = alunos.rmAluno,
                idAutorizacao = autorizacao.idAutorizacao,
                data_aut = autorizacao.data_aut // Não precisa do operador de coalescência aqui, pois já verificamos que autorizacao não é null
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
                        document.Add(new Paragraph($"ID da Autorização: {viewModel.idAutorizacao}"));
                        document.Add(new Paragraph($"Data da Autorização: {viewModel.data_aut?.ToString("dd/MM/yyyy") ?? "N/A"}"));
                    }
                }

                // Retorne o PDF como um arquivo
                var fileName = $"Autorizacao_{viewModel.idAluno}.pdf";
                return File(stream.ToArray(), "application/pdf", fileName);
            }
        }

        //COMUNICADOS
        // GET: Comunicados/Create
        // Ação para gerar e baixar o PDF
        [HttpGet("autorizacao/downloadPdf/{idProcolo}")]
        [Authorize(Roles = "Escola")]
        public async Task<IActionResult> DownloadComunicadoPdfAsync(int idProcolo)
        {
            // Buscando a autorização pelo ID do protocolo
            Comunicados comunicado = await _context.Comunicados.FirstOrDefaultAsync(a => a.fk_prot == idProcolo);

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

        //ATESTA DO DE MATRICULA
        // Ação para gerar e baixar o PDF
        [HttpGet("autorizacao/downloadPdf/{idProcolo}")]
        [Authorize(Roles = "Escola")]
        public async Task<IActionResult> DownloadAtestadoMatriculaPdfAsync(int idProcolo)
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
    }
}