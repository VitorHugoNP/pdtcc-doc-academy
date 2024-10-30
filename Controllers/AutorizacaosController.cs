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


            using (var stream = new MemoryStream())
            {
                // Criação do PDF
                using (var writer = new PdfWriter(stream))
                {
                    using (var pdf = new PdfDocument(writer))
                    {
                        var document = new Document(pdf);
                        document.Add(new Paragraph("Documento de Autorização"));
                        document.Add(new Paragraph($"ID: {aluno.idAluno}"));
                        document.Add(new Paragraph($"Nome: {dados.Nome}"));
                        document.Add(new Paragraph($"Data: {dados.Data}"));
                        document.Add(new Paragraph($"Descrição: {dados.Descricao}"));
                    }
                }

                // Retorne o PDF como um arquivo
                var fileName = $"Autorizacao_{dados.idAutorizacao}.pdf";
                return File(stream.ToArray(), "application/pdf", fileName);
            }
        }
    }
}