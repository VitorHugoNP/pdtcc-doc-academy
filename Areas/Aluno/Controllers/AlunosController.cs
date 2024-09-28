using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using pdtcc_doc_academy.Models;
using pdtcc_doc_academy.Repositories;

namespace pdtcc_doc_academy.Areas.Aluno.Controllers
{
    [Area("Aluno")]
    public class AlunoController : Controller
    {
        private readonly IAlunoRepository _alunosRepository;

        public AlunoController(IAlunoRepository alunoRepository)
        {
            _alunosRepository = alunoRepository;
        }

        // GET: Escolas
        public async Task<IActionResult> Index()
        {
            var aluno = await _alunosRepository.GetAll();
            return View(aluno); // Passa a lista completa para a view
        }

        // GET: Escolas/Excluir/5
        public async Task<IActionResult> Excluir(int id)
        {
            var escola = await _alunosRepository.GetById(id);
            if (escola == null)
            {
                return NotFound();
            }

            return View(escola);
        }

        // POST: Escolas/Excluir/5
        [HttpPost, ActionName("Excluir")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ExcluirConfirmado(int id)
        {
            var aluno = await _alunosRepository.GetById(id);
            if (aluno != null)
            {
                await _alunosRepository.Delete(aluno);
            }
            return RedirectToAction(nameof(Index));
        }



        //AREA LOGIN
        //AREA CADASTRO


        [HttpGet]
        public IActionResult Login()
        {
            return View();

        }

        [HttpPost]
        public async Task<IActionResult> Login(string nome, int cpf, string curso, int rm, string senha)
        {
            var aluno = await _alunosRepository.GetByDataForLogin(nome, cpf, curso, rm, senha);

            if (aluno != null)
            {
                // Autenticação bem-sucedida - Redirecione para o Dashboard
                return RedirectToAction("Dashboard");
            }
            else
            {
                // Login inválido - Exiba a mensagem de erro na view Login
                ViewBag.ErrorMessage = "Nome de usuário ou senha inválidos";
                return View(); // Retorna a mesma view para exibir a mensagem
            }
        }

        public IActionResult Dashboard()
        {
            return View("Index");
        }

        // Cadastro de Funcionário
        [HttpGet]
        public IActionResult Cadastrar()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar(Alunos alunos)
        {
            if (ModelState.IsValid)
            {
                // Chama o repositório para adicionar o funcionário
                await _alunosRepository.Add(alunos);


                // Redireciona para a página de login após o cadastro bem-sucedido
                return RedirectToAction("Login");
            }

            // Se o modelo estiver inválido, retorna à página de cadastro com os erros
            return View(alunos);
        }

    }
}
