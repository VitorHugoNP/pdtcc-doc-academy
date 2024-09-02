using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace pdtcc_doc_academy.Areas.Funcionario.Controllers
{
    public class FuncionarioDocumentoController : Controller
    {
        // GET: FuncionarioDocumentoController
        public ActionResult Index()
        {
            return View();
        }

        // GET: FuncionarioDocumentoController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: FuncionarioDocumentoController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FuncionarioDocumentoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: FuncionarioDocumentoController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: FuncionarioDocumentoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: FuncionarioDocumentoController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: FuncionarioDocumentoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
