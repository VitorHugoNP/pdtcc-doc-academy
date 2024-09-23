using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using pdtcc_doc_academy.Models;
using pdtcc_doc_academy.Repositories;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

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
        public async Task<IActionResult> Login(string email, string senha)
        {
            var funcionario = await _funcionariosRepository.GetByEmailAndPassword(email, senha);

            if (funcionario != null)
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
