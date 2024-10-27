using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using pdtcc_doc_academy.Models;
using pdtcc_doc_academy.Repositories;

namespace pdtcc_doc_academy.Controllers
{
    public class AutorizacoesController : Controller
    {
        private readonly AppDBContext _context;

        public AutorizacoesController(AppDBContext context)
        {
            _context = context;
        }

        // GET: Autorizacaos
        public async Task<IActionResult> Index()
        {
            return View(await _context.Autorizacao.ToListAsync());
        }

        // GET: Autorizacaos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var autorizacao = await _context.Autorizacao
                .FirstOrDefaultAsync(m => m.idAutorizacao == id);
            if (autorizacao == null)
            {
                return NotFound();
            }

            return View(autorizacao);
        }

        // GET: Autorizacaos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Autorizacaos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> CreateAutorizacao(int fk_prot)
        {
            // Aqui você pode buscar os dados do usuário ou do protocolo
            var protocolo = await _context.Protocolo.FindAsync(fk_prot);
            if (protocolo == null)
            {
                return NotFound("Protocolo não encontrado.");
            }

            // Gerar o PDF
            byte[] pdfData = GeneratePdf(protocolo);

            // Criar a nova autorização
            var autorizacao = new Autorizacao
            {
                data_aut = DateTime.Now,
                fk_prot = fk_prot,
                PdfData = pdfData
            };

            // Adicionar ao contexto e salvar
            _context.Autorizacao.Add(autorizacao);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index"); // Redirecionar para onde você quiser
        }

        private byte[] GeneratePdf(Protocolo protocolo)
        {
            using (var memoryStream = new MemoryStream())
            {
                // Criar o PDF
                using (var writer = new PdfWriter(memoryStream))
                {
                    using (var pdf = new PdfDocument(writer))
                    {
                        var document = new Document(pdf);
                        document.Add(new Paragraph("Autorização"));
                        document.Add(new Paragraph($"ID do Protocolo: {protocolo.idProtocolo}"));
                        document.Add(new Paragraph($"Data: {DateTime.Now}"));
                        // Adicione mais informações conforme necessário
                    }
                }
                return memoryStream.ToArray(); // Retorna o PDF como um array de bytes
            }
        }

        // GET: Autorizacaos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var autorizacao = await _context.Autorizacao.FindAsync(id);
            if (autorizacao == null)
            {
                return NotFound();
            }
            return View(autorizacao);
        }

        // POST: Autorizacaos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("idAutorizacao,data_aut,fk_prot")] Autorizacao autorizacao)
        {
            if (id != autorizacao.idAutorizacao)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(autorizacao);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AutorizacaoExists(autorizacao.idAutorizacao))
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
            return View(autorizacao);
        }

        // GET: Autorizacaos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var autorizacao = await _context.Autorizacao
                .FirstOrDefaultAsync(m => m.idAutorizacao == id);
            if (autorizacao == null)
            {
                return NotFound();
            }

            return View(autorizacao);
        }

        // POST: Autorizacaos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var autorizacao = await _context.Autorizacao.FindAsync(id);
            if (autorizacao != null)
            {
                _context.Autorizacao.Remove(autorizacao);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AutorizacaoExists(int id)
        {
            return _context.Autorizacao.Any(e => e.idAutorizacao == id);
        }
    }
}
