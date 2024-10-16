﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using pdtcc_doc_academy.Models;
using pdtcc_doc_academy.Repositories;

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
        public IActionResult Create()
        {
            return View();
        }

        // POST: Comunicados/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("idComunicados,data_comunicado,fk_Prot")] Comunicados comunicados)
        {
            if (ModelState.IsValid)
            {
                _context.Add(comunicados);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
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