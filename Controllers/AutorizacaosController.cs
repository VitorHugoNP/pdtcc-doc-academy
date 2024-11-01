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

namespace pdtcc_doc_academy.Controllers
{
    public class AutorizacaoController : Controller
    {
        private readonly AppDBContext _context; // Substitua pelo seu contexto do Entity Framework

        public AutorizacaoController(AppDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> DownloadPdf()
        {
            var autorizacao = await _context.Autorizacao.ToListAsync();
            return View(autorizacao);
        }

        // Ação para gerar e baixar o PDF
        [HttpGet("autorizacao/download/{id}")]
        [Authorize(Roles = "Aluno")]
        public async Task<IActionResult> DownloadPdfAsync(int id)
        {
            // Buscando o aluno com base no ID
            var alunos = await _context.aluno.FindAsync(id);

            // Buscando a autorização com base no ID do aluno
            var autorizacaoEncontrada = await _context.Autorizacao.FirstOrDefaultAsync(a => a.fk_prot == id); // Ajuste conforme necessário

            // Verifica se o aluno ou a autorização foram encontrados
            if (alunos == null || autorizacaoEncontrada == null)
            {
                return NotFound();
            }

            // Prepara o modelo para o PDF
            var viewModel = new AlunoAutorizacao
            {
                idAluno = alunos.idAluno,
                nomeAluno = alunos.nomeAluno,
                cpfAluno = alunos.cpfAluno,
                rgAluno = alunos.rgAluno,
                rmAluno = alunos.rmAluno,
                idAutorizacao = autorizacaoEncontrada.idAutorizacao,
                data_aut = autorizacaoEncontrada.data_aut,
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
                        document.Add(new Paragraph($"ID: {viewModel.idAluno}"));
                        document.Add(new Paragraph($"Nome: {viewModel.nomeAluno}"));
                        document.Add(new Paragraph($"CPF: {viewModel.cpfAluno}"));
                        document.Add(new Paragraph($"RG: {viewModel.rgAluno}"));
                        document.Add(new Paragraph($"RM: {viewModel.rmAluno}"));
                        // Adicione mais informações do aluno aqui, se necessário
                    }
                }

                // Retorne o PDF como um arquivo
                var fileName = $"Autorizacao_{viewModel.idAluno}.pdf";
                return File(stream.ToArray(), "application/pdf", fileName);
            }
        }
    }
}