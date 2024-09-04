using Microsoft.AspNetCore.Mvc;

namespace pdtcc_doc_academy.Areas.Funcionario.Controllers
{
    public class FuncionarioController : Controller
    {
        [Area("Funcionario")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
