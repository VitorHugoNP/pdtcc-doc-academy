using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pdtcc_doc_academy.Models;
using pdtcc_doc_academy.Repositories;
using System.Threading.Tasks;

namespace pdtcc_doc_academy.Controllers // Ajuste o namespace se necessário
{
    [Area("Escola")]
    public class EscolasController : Controller
    {
        private readonly IEscolaRepository _escolasRepository; // Substitua 'SeuDbContext' pelo nome do seu DbContext

        public EscolasController(IEscolaRepository escolaRepository)
        {
            _escolasRepository = escolaRepository;
        }

        // GET: Escolas
        public async Task<IActionResult> Index()
        {
            var escola = await _escolasRepository.Getall();
            return View(escola); // Passa a lista completa para a view
        }

        // GET: Escolas/Detalhes/5
        public async Task<IActionResult> Detalhes(int id)
        {
            var escola = await _escolasRepository.GetById(id);
            if (escola == null)
            {
                return NotFound();
            }

            return View(escola);
        }

        // GET: Escolas/Criar
        public IActionResult Criar()
        {
            return View();
        }

        // POST: Escolas/Criar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Criar([Bind("IdEscola,nomeEscola,enderecoEscola,emailEscola,telefoneEscola")] Escolas escola)
        {
            if (escola != null)
            {
                await _escolasRepository.Add(escola);
                return RedirectToAction(nameof(Index));
            }
            return View(escola);
        }

        // GET: Escolas/Editar/5
        public async Task<IActionResult> Editar(int id)
        {
            var escola = await _escolasRepository.GetById(id);
            if (escola == null)
            {
                return NotFound();
            }
            return View(escola);
        }

        // POST: Escolas/Editar/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(int id, [Bind("IdEscola,nomeEscola,enderecoEscola,emailEscola,telefoneEscola")] Escolas escola)
        {
            if (id != escola.idEscola)
            {
                return NotFound();
            }

            if (escola != null)
            {
                await _escolasRepository.Update(escola);
                return RedirectToAction(nameof(Index));
            }
            return View(escola);
        }

        // GET: Escolas/Excluir/5
        public async Task<IActionResult> Excluir(int id)
        {
            var escola = await _escolasRepository.GetById(id);
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
            var escola = await _escolasRepository.GetById(id);
            if (escola != null)
            {
                await _escolasRepository.Delete(escola);
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
        public async Task<IActionResult> Login(string nome, string endereco, string email, string senha)
        {
            var escola = await _escolasRepository.GetByDataForLogin(nome, endereco, email, senha);

            if (escola != null)
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
        public async Task<IActionResult> Cadastrar(Escolas escola)
        {
            if (ModelState.IsValid)
            {
                // Chama o repositório para adicionar o funcionário
                await _escolasRepository.Add(escola);


                // Redireciona para a página de login após o cadastro bem-sucedido
                return RedirectToAction("Login");
            }

            // Se o modelo estiver inválido, retorna à página de cadastro com os erros
            return View(escola);
        }

    }
}
