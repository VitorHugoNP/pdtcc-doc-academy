using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pdtcc_doc_academy.Models;
using pdtcc_doc_academy.Repositories;
using System.Linq;
using System.Threading.Tasks;

namespace pdtcc_doc_academy.Controllers
{
    [Authorize(Roles = "Funcionario")]
    public class AlunoController : Controller
    {
        private readonly AppDBContext _context;

        public AlunoController(AppDBContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var alunos = _context.aluno.ToList();
            return View("SelecionarAluno", alunos);
        }

        [HttpPost]
        public IActionResult Selecionar(int idAluno)
        {
            var alunoSelecionado = _context.aluno.Find(idAluno);
            if (alunoSelecionado != null)
            {
                // Aqui você pode fazer o que quiser com o aluno selecionado
                // Por exemplo, redirecionar para uma página de detalhes
                return RedirectToAction("Detalhes", new { id = alunoSelecionado.idAluno });
            }

            return NotFound();
        }

        public IActionResult Detalhes(int id)
        {
            var aluno = _context.aluno.Find(id);
            if (aluno == null)
            {
                return NotFound();
            }
            return View(aluno);
        }
    }
}