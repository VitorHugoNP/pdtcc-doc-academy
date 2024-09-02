using Microsoft.AspNetCore.Mvc;

namespace pdtcc_doc_academy.Areas.Funcionario.Controllers
{
    [Area("Funcionario")]
    public class FuncionarioController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}