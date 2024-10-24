using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using pdtcc_doc_academy.Repositories;
using pdtcc_doc_academy.ViewModels;
using System.Security.Claims;

namespace pdtcc_doc_academy.Controllers
{
    public class AutenticacaoController : Controller
    {
        private readonly AppDBContext _context;

        public AutenticacaoController(AppDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login() //método para abrir a view de login
        {
            return View(); // Retorna a view do formulário de login
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var usuario = _context.Usuario.SingleOrDefault(u => u.emailUsuario == model.Email);

                if (usuario != null /*&& VerificarSenha(model.Senha, usuario.Senha)*/)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Email, usuario.emailUsuario),
                        new Claim(ClaimTypes.Role, usuario.tipoUsuario), // Aqui adicionamos a role com base no tipo de usuário
                        
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var authProperties = new AuthenticationProperties
                    {
                        // Propriedades de autenticação como redirecionamento
                    };

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

                    if (usuario.tipoUsuario == "Aluno")
                    {
                        return RedirectToAction("Index", "Alunos");
                    }
                    else if (usuario.tipoUsuario == "Funcionario")
                    {
                        return RedirectToAction("Index", "Funcionarios");
                    }
                    else if (usuario.tipoUsuario == "Escola")
                    {
                        return RedirectToAction("Index", "Escolas");
                    }
                }

                ModelState.AddModelError(string.Empty, "E-mail ou senha incorretos.");
            }

            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }

    }
}