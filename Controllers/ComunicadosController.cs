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
            return View(await _context.Comunicados.ToListAsync());
        }

        // GET: Comunicados/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comunicados = await _context.Comunicados
                .FirstOrDefaultAsync(m => m.idComunicados == id);
            if (comunicados == null)
            {
                return NotFound();
            }

            return View(comunicados);
        }

        

        // GET: Comunicados/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comunicados = await _context.Comunicados.FindAsync(id);
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

            var comunicados = await _context.Comunicados
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
            var comunicados = await _context.Comunicados.FindAsync(id);
            if (comunicados != null)
            {
                _context.Comunicados.Remove(comunicados);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ComunicadosExists(int id)
        {
            return _context.Comunicados.Any(e => e.idComunicados == id);
        }
    }
}
