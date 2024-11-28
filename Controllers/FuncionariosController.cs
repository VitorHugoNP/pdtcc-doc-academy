using System;
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
    public class FuncionariosController : Controller
    {
        private readonly AppDBContext _context;

        public FuncionariosController(AppDBContext context)
        {
            _context = context;
        }

        // GET: Funcionarios
        public async Task<IActionResult> Index()
        {
            //return View(await _context.Funcionario.ToListAsync());
            //return View(await _context.Escola.ToListAsync());

            // Obtém o ID da escola a partir das claims do usuário autenticado
            var idFuncionarioClaim = User.Claims.FirstOrDefault(c => c.Type == "idFuncionario")?.Value;

            if (idFuncionarioClaim == null)
            {
                return NotFound("Escola não encontrada.");
            }

            // Converte o ID da escola para int
            int idFuncionario = int.Parse(idFuncionarioClaim);

            // Busca a escola logada no banco de dados
            var funcionario = await _context.Funcionario
                .FirstOrDefaultAsync(f => f.idFuncionario == idFuncionario);

            if (funcionario == null)
            {
                return NotFound("Escola não encontrada.");
            }

            return View(funcionario); // Retorna a view com a escola logada
        }

        // GET: Funcionarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var funcionario = await _context.Funcionario
                .FirstOrDefaultAsync(m => m.idFuncionario == id);
            if (funcionario == null)
            {
                return NotFound();
            }

            return View(funcionario);
        }

        // GET: Funcionarios/Create
        public IActionResult CadastrarFuncionario()
        {
            return View();
        }

        // POST: Funcionarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CadastrarFuncionario([Bind("IdFuncionario,nome_func,email_func,senha_func,fk_escola")] Funcionario funcionario)
        {
            if (ModelState != null)
            {                
                var usuario = new Usuario // pega os dados para acesso e salva na tabela usuario já com o tipo especifico
                {
                    emailUsuario = funcionario.email_func,
                    senhaUsuario = funcionario.senha_func,
                    tipoUsuario = "Funcionario"
                };

                _context.Add(usuario);
                var result = await _context.SaveChangesAsync();
                var func = new Funcionario
                {
                    nome_func = funcionario.nome_func,
                    email_func = funcionario.email_func,
                    senha_func = funcionario.senha_func,
                    fk_escola = funcionario.fk_escola,
                    fk_usuario = usuario.idUsuario
                };
                _context.Add(func);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Escolas");
            }
            return RedirectToAction("Index", "Escolas");
        }

        public async Task<IActionResult> VerProtocoloFunc()
        {
            var protocolos = await _context.Protocolo
                .Include(p => p.aluno)
                .Include(p => p.funcionario)
                .ToListAsync();

            return View(protocolos); // Passa a lista de protocolos para a view
        }

        // GET: Funcionarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var funcionario = await _context.Funcionario.FindAsync(id);
            if (funcionario == null)
            {
                return NotFound();
            }
            return View(funcionario);
        }

        // POST: Funcionarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdFuncionario,nome_func,email_func,senha_func,fk_escola")] Funcionario funcionario)
        {
            if (id != funcionario.idFuncionario)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(funcionario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FuncionarioExists(funcionario.idFuncionario))
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
            return View(funcionario);
        }

        // GET: Funcionarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var funcionario = await _context.Funcionario
                .FirstOrDefaultAsync(m => m.idFuncionario == id);
            if (funcionario == null)
            {
                return NotFound();
            }

            return View(funcionario);
        }

        // POST: Funcionarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var funcionario = await _context.Funcionario.FindAsync(id);
            if (funcionario != null)
            {
                _context.Funcionario.Remove(funcionario);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FuncionarioExists(int id)
        {
            return _context.Funcionario.Any(e => e.idFuncionario == id);
        }
    }
}
