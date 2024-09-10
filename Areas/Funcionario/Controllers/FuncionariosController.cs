using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using pdtcc_doc_academy.Models;
using pdtcc_doc_academy.Repositories;
using ZstdSharp.Unsafe;

namespace pdtcc_doc_academy.Areas.Funcionario.Controllers
{
    [Area("Funcionario")]
    public class FuncionariosController : Controller
    {
        //private readonly AppDBContext _context;
        private readonly IFuncionariosRepository _funcionariosRepository;

        public FuncionariosController(IFuncionariosRepository funcionariosRepository)
        {
            _funcionariosRepository = funcionariosRepository;
        }
        //public FuncionariosController(AppDBContext context)
        //{
        //    _context = context;
        //}

        // GET: Funcionario/Funcionarios
        public async Task<IActionResult> Index()
        {
            return await _funcionariosRepository.Getall() != null ?
            View(await _funcionariosRepository.Getall()) :
            Problem("Entity set 'AppDBContext.Categorias'  is null.");
        }

        // GET: Funcionario/Funcionarios/Details/5
        public async Task<IActionResult> Detalhes(int id)
        {
            var categoria = await _funcionariosRepository.GetById(id);
            if (categoria == null)
            {
                return NotFound();
            }

            return View(categoria);
        }

        // GET: Funcionario/Funcionarios/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Funcionario/Funcionarios/Cadastro
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Cadastro([Bind("idFunc,nomeFunc,emailFunc,senhaFunc")] Funcionarios funcionarios)
        {
            if (funcionarios != null)
            {
                await _funcionariosRepository.Add(funcionarios);
                return RedirectToAction(nameof(Index));
            }
            return View(funcionarios);
        }

        // GET: Funcionario/Funcionarios/Edit/5
        public async Task<IActionResult> Editar(int id)
        {
            var funcionario = _funcionariosRepository.GetById(id);

            if (funcionario == null)
            {
                return NotFound();
            }

            var funcionarios = await _funcionariosRepository.GetById(id);
            if (funcionarios == null)
            {
                return NotFound();
            }
            return View(funcionarios);
        }

        // POST: Funcionario/Funcionarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("idFunc,nomeFunc,emailFunc,senhaFunc")] Funcionarios funcionarios)
        {
            if (id != funcionarios.idFunc)
            {
                return NotFound();
            }

            if (funcionarios != null)
            {
                await _funcionariosRepository.Update(funcionarios);
                return RedirectToAction(nameof(Index));
            }
            return View(funcionarios);
        }

        // GET: Funcionario/Funcionarios/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var funcionario = await _funcionariosRepository.GetById(id);
            if (funcionario == null)
            {
                return NotFound();
            }
            return View(funcionario);
        }

        // POST: Funcionario/Funcionarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var funcionario = await _funcionariosRepository.GetById(id);
            if (funcionario != null)
            {
                await _funcionariosRepository.Delete(funcionario);
            }
            return View(funcionario);
        }

    }
}
