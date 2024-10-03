using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using pdtcc_doc_academy.Repositories;
using System.Security.Claims;

namespace pdtcc_doc_academy.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDBContext _context;
        private readonly IAlunoRepository _alunoRepository;

        public AccountController(AppDBContext context, IAlunoRepository alunoRepository)
        {
            _context = context;
            _alunoRepository = alunoRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string senha)
        {
            // Verificar se o usuário existe no banco de dados
            var usuario = _context.Usuarios.FirstOrDefault(u => u.email_Usuario == email && u.senha_Usuario == senha);

            if (usuario != null)
            {
                // Criar as Claims do usuário
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, usuario.nome_Usuario),
                    new Claim(ClaimTypes.Email, usuario.email_Usuario),
                    new Claim("idUsuario", usuario.idUsuario.ToString())  // Adicionar o ID do usuário como claim
                };

                // Criar o identity do usuário
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                // Autenticar o usuário
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));

                Console.WriteLine("asdasdasdassdasdasd");

                return RedirectToAction("Index");
            }

            ViewBag.Erro = "Usuário ou senha inválidos.";
            return View();
        }




        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
    }
}
