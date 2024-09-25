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
            var escolas = await _escolasRepository.Getall();
            return View(escolas); // Passa a lista completa para a view
        }

        // GET: Escolas/Detalhes/5
        public async Task<IActionResult> Detalhes(int id)
        {
            var categoria = await _escolasRepository.GetById(id);
            if (categoria == null)
            {
                return NotFound();
            }

            return View(categoria);
        }

        // GET: Escolas/Criar
        public IActionResult Criar()
        {
            return View();
        }

        // POST: Escolas/Criar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Criar([Bind("IdEscola,nomeEscola,enderecoEscola,emailEscola,telefoneEscola")] Escolas escolas)
        {
            if (escolas != null)
            {
                await _escolasRepository.Add(escolas);
                return RedirectToAction(nameof(Index));
            }
            return View(escolas);
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
        public async Task<IActionResult> Editar(int id, [Bind("IdEscola,nomeEscola,enderecoEscola,emailEscola,telefoneEscola")] Escolas escolas)
        {
            if (id != escolas.IdEscola)
            {
                return NotFound();
            }

            if (escolas != null)
            {
                await _escolasRepository.Update(escolas);
                return RedirectToAction(nameof(Index));
            }
            return View(escolas);
        }

        // GET: Escolas/Excluir/5
        public async Task<IActionResult> Excluir(int id)
        {
            var escolas = await _escolasRepository.GetById(id);
            if (escolas == null)
            {
                return NotFound();
            }

            return View(escolas);
        }

        // POST: Escolas/Excluir/5
        [HttpPost, ActionName("Excluir")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ExcluirConfirmado(int id)
        {
            var escolas = await _escolasRepository.GetById(id);
            if (escolas != null)
            {
                await _escolasRepository.Delete(escolas);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
