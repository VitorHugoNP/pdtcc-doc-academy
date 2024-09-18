using Microsoft.AspNetCore.Mvc;

namespace pdtcc_doc_academy.Areas.Funcionario.Controllers
{
    public class FuncionariosController : Controller
    {
        [Area("Funcionario")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
