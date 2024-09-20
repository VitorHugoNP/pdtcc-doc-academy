using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using pdtcc_doc_academy.Models;
using pdtcc_doc_academy.Repositories;

namespace pdtcc_doc_academy.Areas.Funcionario.Controllers
{
    [Area("Funcionario")]
    public class FuncionariosComunicadosController : Controller
    {
        private readonly IComunicadosRepository _ComunicadosRepository;

        public FuncionariosComunicadosController(IComunicadosRepository comunicadosRepository)
        {
            _ComunicadosRepository = comunicadosRepository;
        }

        // GET: Funcionario/FuncionariosComunicados
        public async Task<IActionResult> Index()
        {
            return await _ComunicadosRepository.Getall() != null ?
            View(await _ComunicadosRepository.Getall()) :
            Problem("Entity set 'AppDBContext.Categorias'  is null.");
        }

        // GET: Funcionario/FuncionariosComunicados/Details/5
        public async Task<IActionResult> Detalhes(int id)
        {
            var categoria = await _ComunicadosRepository.GetById(id);
            if (categoria == null)
            {
                return NotFound();
            }

            return View(categoria);
        }

        // GET: Funcionario/FuncionariosComunicados/Create
        public IActionResult Cadastro()
        {
            return View();
        }

        // POST: Funcionario/FuncionariosComunicados/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Cadastro([Bind("idComunicados,dataComunicado,fkDoc")] Comunicados comunicados)
        {
            if (comunicados != null)
            {
                await _ComunicadosRepository.Add(comunicados);
                return RedirectToAction(nameof(Index));
            }
            return View(comunicados);
        }

        // GET: Funcionario/FuncionariosComunicados/Edit/5
        public async Task<IActionResult> Editar(int id)
        {
            var categoria = await _ComunicadosRepository.GetById(id);
            if (categoria == null)
            {
                return NotFound();
            }
            return View(categoria);
        }

        // POST: Funcionario/FuncionariosComunicados/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(int id, [Bind("idComunicados,dataComunicado,fkDoc")] Comunicados comunicados)
        {
            if (id != comunicados.idComunicados)
            {
                return NotFound();
            }

            if (comunicados != null)
            {
                try
                {
                    await _ComunicadosRepository.Update(comunicados);
                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(comunicados);
        }

        // GET: Funcionario/FuncionariosComunicados/Delete/5
        public async Task<IActionResult> Excluir(int id)
        {
            var categoria = await _ComunicadosRepository.GetById(id);
            if (categoria == null)
            {
                return NotFound();
            }

            return View(categoria);
        }

        // POST: Funcionario/FuncionariosComunicados/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var comunicados = await _ComunicadosRepository.GetById(id);
            if (comunicados != null)
            {
                await _ComunicadosRepository.Delete(comunicados);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
