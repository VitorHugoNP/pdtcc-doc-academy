using Microsoft.AspNetCore.Mvc;
using pdtcc_doc_academy.Models;
using System.IO;
using System.Threading.Tasks;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using pdtcc_doc_academy.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using static iText.StyledXmlParser.Jsoup.Select.Evaluator;
using Org.BouncyCastle.Security;

namespace pdtcc_doc_academy.Controllers
{
    public class AutorizacaoController : Controller
    {
        private readonly AppDBContext _context; // Substitua pelo seu contexto do Entity Framework

        public AutorizacaoController(AppDBContext context)
        {
            _context = context;
        }

        // Ação para gerar e baixar o PDF
        [HttpGet("autorizacao/downloadPdf/{idProcolo}")]
        [Authorize(Roles = "Escola")]
        public async Task<IActionResult> DownloadPdfAsync(int idProcolo)
        {
            // Buscando o aluno pelo ID
            
            Autorizacao autorizacao = await _context.Autorizacao.FirstOrDefaultAsync(a => a.fk_prot == idProcolo);
            
            Protocolo protocolo = await _context.Protocolo.FirstOrDefaultAsync(p => p.idProtocolo == autorizacao.fk_prot);

            Alunos alunos = await _context.aluno.FirstOrDefaultAsync(x => x.idAluno == protocolo.fk_aluno);
            
            // Preenchendo o ViewModel
            var viewModel = new AlunoAutorizacao
            {
                idAluno = alunos.idAluno,
                nomeAluno = alunos.nomeAluno,
                cpfAluno = alunos.cpfAluno,
                rgAluno = alunos.rgAluno,
                rmAluno = alunos.rmAluno,
                idAutorizacao = autorizacao.idAutorizacao,
                data_aut = autorizacao?.data_aut
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
    }
}