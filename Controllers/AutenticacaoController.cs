using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using pdtcc_doc_academy.Repositories;
using pdtcc_doc_academy.ViewModels;
using System.Security.Claims;
using pdtcc_doc_academy.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

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
        public async Task<IActionResult> Login(LoginViewModel model, string email, string senha)
        {
            if (ModelState.IsValid)
            {
                var usuario = _context.Usuario.SingleOrDefault(u => u.emailUsuario == model.Email);

                var alunos = await _context.aluno.FirstOrDefaultAsync(a => a.emailAluno == email && a.senhaAluno == senha);
                var escolas = await _context.Escola.FirstOrDefaultAsync(e => e.emailEscola == email && e.senhaEscola == senha);
                var funcionarios = await _context.Funcionario.FirstOrDefaultAsync(f => f.email_func == email && f.senha_func == senha);

                //Area login dos alunos

                if (usuario != null /*&& VerificarSenha(model.Senha, usuario.Senha)*/)
                {
                    //var claims = new List<Claim>
                    //{
                    //    new Claim(ClaimTypes.Name, usuario.emailUsuario),
                    //    new Claim(ClaimTypes.Role, "Aluno"), // Define o tipo de usuário como Aluno
                    //    new Claim(ClaimTypes.Role, "Funcionario"), // Define o tipo de usuário como funcionario
                    //    new Claim(ClaimTypes.Role, "Escola"), // Define o tipo de usuário como escola
                        
                    //    new Claim("AlunoId", alunos.idAluno.ToString()) // Inclui o ID do Aluno nas claims
                    //    new Claim("idFuncionario", alunos.idAluno.ToString()),
                    //    new Claim("idEscola", escolas.idEscola.ToString())
                    //};

                    //var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    //await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                    if (usuario.tipoUsuario == "Aluno")
                    {
                        var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name, usuario.emailUsuario),
                            new Claim(ClaimTypes.Role, "Aluno"),
                            new Claim("AlunoId", alunos.idAluno.ToString())

                        };
                        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                        return RedirectToAction("Index", "Alunos");
                    }
                    else if (usuario.tipoUsuario == "Funcionario")
                    {

                        var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name, usuario.emailUsuario),
                            new Claim(ClaimTypes.Role, "Funcionario"),
                            new Claim("idFuncionario", funcionarios.idFuncionario.ToString())

                        };
                        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                        return RedirectToAction("Index", "Funcionarios");
                    }
                    else if (usuario.tipoUsuario == "Escola")
                    {
                        var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name, usuario.emailUsuario),
                            new Claim(ClaimTypes.Role, "Escola"),
                            new Claim("idEscola", escolas.idEscola.ToString())

                        };
                        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

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