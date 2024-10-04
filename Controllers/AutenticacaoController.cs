using Microsoft.AspNetCore.Mvc;
using pdtcc_doc_academy.Repositories;
using pdtcc_doc_academy.ViewModels;

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
        [ValidateAntiForgeryToken] // Protege contra ataques CSRF
        //método que recebe os dados do formulário de login, foi criado uma viewmodel para identificar os campos de formulários
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)//verifica se os campos que estão vindo da view são validos
            {
                // Procurar o usuário no banco de dados pelo e-mail 
                var usuario = _context.Usuario.SingleOrDefault(u => u.emailUsuario == model.Email);

                if (usuario != null && (model.Senha == usuario.senhaUsuario))//verifica se usuário não é nulo e a senha digitada é igual a salva no banco de dados
                {
                    // Verifica o tipo de usuário e redireciona conforme o tipo para suas rotas especificas
                    if (usuario.tipoUsuario == "Aluno")
                    {
                        return RedirectToAction("Index", "Alunos");//direciona para controller alunos 
                    }
                    else if (usuario.tipoUsuario == "Funcionario")
                    {
                        return RedirectToAction("Index", "Funcionarios");//direciona para controller funcionários
                    }
                    else if (usuario.tipoUsuario == "Escola")
                    {
                        return RedirectToAction("Index", "Escolas");//direciona para controller escolas
                    }
                }

                // Caso não encontre o usuário ou a senha seja inválida
                ModelState.AddModelError(string.Empty, "E-mail ou senha incorretos.");
            }

            // Retorna a view com os erros de validação
            return View(model);
        }
    }
}
