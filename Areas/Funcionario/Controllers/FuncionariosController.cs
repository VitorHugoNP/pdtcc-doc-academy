using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using pdtcc_doc_academy.Models;
using pdtcc_doc_academy.Repositories;

namespace pdtcc_doc_academy.Areas.Funcionario.Controllers
{
    [Area("Funcionario")]
    public class FuncionariosController : Controller
    {
        private readonly IFuncionariosRepository _funcionariosRepository;

        public FuncionariosController(IFuncionariosRepository funcionariosRepository)
        {
            _funcionariosRepository = funcionariosRepository;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            var funcionario = await _funcionariosRepository.GetByUsernameAndPassword(email, password);

            if (funcionario != null)
            {
                return RedirectToAction("Dashboard");
            }

            ViewBag.ErrorMessage = "Nome de usuário ou senha inválidos";
            return View();
        }

        public IActionResult Dashboard()
        {
            return View();
        }

        // Cadastro de Funcionário
        [HttpGet]
        public IActionResult Cadastrar()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar(Funcionarios funcionario)
        {
            if (ModelState.IsValid)
            {
                // Chama o repositório para adicionar o funcionário
                await _funcionariosRepository.Add(funcionario);

                // Redireciona para a página de login após o cadastro bem-sucedido
                return RedirectToAction("Login");
            }

            // Se o modelo estiver inválido, retorna à página de cadastro com os erros
            return View(funcionario);
        }
    }
}
