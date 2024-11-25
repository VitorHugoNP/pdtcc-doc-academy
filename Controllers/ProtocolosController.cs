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
using iText.Kernel.Pdf.Canvas.Draw;

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
        public async Task<IActionResult> createFuncionario()
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

        //GET
        public async Task<IActionResult> Create()
        {
            var claimAlunoId = User.Claims.FirstOrDefault(c => c.Type == "AlunoId");

            // Converte o valor do claim para int
            int alunoId = int.Parse(claimAlunoId.Value);

            var protocolos = await _context.Protocolo
                .Where(p => p.fk_aluno == alunoId) // Filtra os protocolos pelo ID do aluno
                .Include(p => p.aluno)
                .Include(p => p.funcionario)
                .ToListAsync();

            return View(protocolos); // Retorna a view com os protocolos encontrados
        }

        [HttpGet]
        public async Task<IActionResult> RequerimentosFuncionario()
        {
            ViewBag.Alunos = await _context.aluno.ToListAsync();
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Funcionario")]
        public async Task<IActionResult> RequerimentosFuncionario(int selectedOption, int idAluno, int idFunc)
        {
            // Obter o ID do funcionário a partir das claims
            var claimFuncionarioId = User.Claims.FirstOrDefault(c => c.Type == "idFuncionario");

            if (claimFuncionarioId == null)
            {
                return Unauthorized("Você precisa estar logado como funcionário para criar um protocolo.");
            }

            int idFuncionario = int.Parse(claimFuncionarioId.Value);

            // Criar um novo protocolo


            switch (selectedOption)
            {
                case 1: // Atestado de Matrícula
                    var protocolo1 = new Protocolo
                    {
                        fk_aluno = idAluno, // ID do aluno selecionado
                        fk_func = idFuncionario, // ID do funcionário logado
                        tipo_Doc = "Atestado Matricula" // Defina o tipo de documento conforme necessário
                    };
                    _context.Add(protocolo1);
                    await _context.SaveChangesAsync();

                    break;
                case 2: // Autorização
                    var protocolo2 = new Protocolo
                    {
                        fk_aluno = idAluno, // ID do aluno selecionado
                        fk_func = idFuncionario, // ID do funcionário logado
                        tipo_Doc = "Autorizacao" // Defina o tipo de documento conforme necessário
                    };
                    _context.Add(protocolo2);

                    await _context.SaveChangesAsync();

                    break;
                case 3: // Comunicado
                    var protocolo3 = new Protocolo
                    {
                        fk_aluno = idAluno, // ID do aluno selecionado
                        fk_func = idFuncionario, // ID do funcionário logado
                        tipo_Doc = "Comunicado" // Defina o tipo de documento conforme necessário
                    };
                    _context.Add(protocolo3);
                    await _context.SaveChangesAsync();
                    break;
                default:
                    ModelState.AddModelError("", "Opção inválida.");
                    return RedirectToAction("Index");
            }

            return RedirectToAction(nameof(Index)); // Redireciona para a lista de protocolos ou outra ação desejada 
        }

        // POST: Protocolos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateProtocolo(string selectedOption)
        {
            
            // Busca os IDs de funcionário a partir das claims
            var claimFuncionarioId = User.Claims.FirstOrDefault(c => c.Type == "FuncionarioId");
            int idFuncionario = claimFuncionarioId != null ? int.Parse(claimFuncionarioId.Value) : 1;

            var claimAlunoId = User.Claims.FirstOrDefault(cl => cl.Type == "AlunoId");
            int idAluno = claimAlunoId != null ? int.Parse(claimAlunoId.Value) : 1;


            // Lógica para lidar com o tipo de documento selecionado
            switch (selectedOption)
            {
                case "1": // Atestado de Matrícula
                    return await HandleAtestadoMatricula(idFuncionario, idAluno);
                case "2": // Autorização
                    return await HandleAutorizacao(idFuncionario, idAluno);
                case "3": // Comunicado
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
            // Busca o atestado de matrícula pelo protocolo
            Atestado_Matricula atestado_Matricula = await _context.Atestado_Matricula.FirstOrDefaultAsync(a => a.fk_prot == idProcolo);

            if (atestado_Matricula == null)
            {
                return NotFound("Atestado não encontrado.");
            }

            // Busca o protocolo associado ao atestado
            Protocolo protocolo = await _context.Protocolo.FirstOrDefaultAsync(p => p.idProtocolo == atestado_Matricula.fk_prot);
            if (protocolo == null)
            {
                return NotFound("Protocolo não encontrado.");
            }

            // Busca os dados do aluno associado ao protocolo
            Alunos alunos = await _context.aluno.FirstOrDefaultAsync(x => x.idAluno == protocolo.fk_aluno);
            if (alunos == null)
            {
                return NotFound("Aluno não encontrado.");
            }

            // Supondo que você quer buscar o primeiro AlunoCurso associado ao aluno
            Curso curso = await _context.Curso.FirstOrDefaultAsync(c => c.AlunoCursos == );

            Serie serie = await _context.Serie.FirstOrDefaultAsync(s => s.AlunoSeries == );


            //var curso = alunos.alunoCursos.FirstOrDefault().Curso; // Obtém o primeiro curso
            //var serie = await alunos.alunoSeries.FirstOrDefault().Serie; // Obtém a primeira série

            // Criação do ViewModel
            var viewModel = new AlunoAtestadoMatricula
            {
                idAluno = alunos.idAluno,
                nomeAluno = alunos.nomeAluno,
                cpfAluno = alunos.cpfAluno,
                rgAluno = alunos.rgAluno,
                rmAluno = alunos.rmAluno,

                IdAtest_mat = atestado_Matricula.IdAtest_mat,
            };

            // Geração do PDF
            using (var stream = new MemoryStream())
            {
                using (var writer = new PdfWriter(stream))
                {
                    using (var pdf = new PdfDocument(writer))
                    {
                        var document = new Document(pdf);

                        // Adicionando título
                        document.Add(new Paragraph("Documento de Atestado de Matrícula")
                            .SetFontSize(20)
                            .SetBold()
                            .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER));

                        // Adicionando informações do aluno
                        document.Add(new Paragraph($"ID do Aluno: {viewModel.idAluno}"));
                        document.Add(new Paragraph($"Nome: {viewModel.nomeAluno}"));
                        document.Add(new Paragraph($"CPF: {viewModel.cpfAluno}"));
                        document.Add(new Paragraph($"RG: {viewModel.rgAluno}"));
                        document.Add(new Paragraph($"RM: {viewModel.rmAluno}"));
                        document.Add(new Paragraph($"ID do Atestado: {viewModel.IdAtest_mat}"));

                        document.Add(new Paragraph($"Eu declaro para os devidos fins que {viewModel.nomeAluno}, RG.{viewModel.rgAluno}, está matriculado(a) regularmente na {viewModel.serieAluno}, do Ensino Medio, Curso {viewModel.cursoAluno}, no período Matutino, das 7:10 às 12:30, nesta escola Técnica Estadual de Santa Fé do Sul, situada à av. Conselheiro antonio prado, s/n, Bairro São Francisco. Afirmo ainda que o curso tem duração de 36 (trinta e seis) meses, com previsãod e termino para 15/12/2024."));
                        document.Add(new Paragraph($"Sem mais a declarar, ficamos a disposição para maiores esclarecimentos se necessário."));
                        // Adicionando uma linha horizontal
                        document.Add(new LineSeparator(new SolidLine()));

                        // Adicionando uma data
                        document.Add(new Paragraph($"Data: {DateTime.Now.ToString("dd/MM/yyyy")}"));

                        // Adicionando um rodapé
                        document.Add(new Paragraph("Este documento é gerado eletronicamente e não requer assinatura.")
                            .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                            .SetFontSize(10));
                    }
                }

                var fileName = $"Atestado_Matriculas_{viewModel.idAluno}.pdf";
                return File(stream.ToArray(), "application/pdf", fileName);
            }
        }

        // GET: Protocolos/PorAluno
        public async Task<IActionResult> PorAluno(int alunoId)
        {
            var protocolos = await _context.Protocolo
                .Where(p => p.fk_aluno == alunoId) // Filtra os protocolos pelo ID do aluno
                .Include(p => p.aluno)
                .Include(p => p.funcionario)
                .ToListAsync();

            return View(protocolos); // Retorna a view com os protocolos filtrados
        }
    }
}