using Microsoft.AspNetCore.Mvc;
using pdtcc_doc_academy.Models;
using System.IO;
using System.Threading.Tasks;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using pdtcc_doc_academy.Repositories;
using Microsoft.EntityFrameworkCore;

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

        // Exemplo de método que gera um PDF
        [HttpGet("autorizacao/download/{id}")]
        public async Task<IActionResult> DownloadPdf(int id)
        {
            // Aqui você pode buscar dados do banco de dados se necessário
            // var autorizacao = _context.Autorizacoes.Find(id);
            // Se você não precisa de dados do banco, pode criar um PDF genérico

            using (var stream = new MemoryStream())
            {
                // Criação do PDF
                using (var writer = new PdfWriter(stream))
                {
                    using (var pdf = new PdfDocument(writer))
                    {
                        var document = new Document(pdf);
                        document.Add(new Paragraph("Este é um PDF gerado dinamicamente."));
                        document.Add(new Paragraph($"ID da Autorização: {id}"));
                        // Adicione mais conteúdo conforme necessário
                    }
                }

                // Retorne o PDF como um arquivo
                var fileName = $"Autorizacao_{id}.pdf";
                return File(stream.ToArray(), "application/pdf", fileName);
            }
        }
    }
}