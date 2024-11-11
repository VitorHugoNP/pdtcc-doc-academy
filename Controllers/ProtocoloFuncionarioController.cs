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
    public class ProtocoloFuncionarioController : Controller
    {
        private readonly AppDBContext _context;

        public ProtocoloFuncionarioController(AppDBContext context)
        {
            _context = context;
        }

        // GET: ProtocoloFuncionario/Index
        public async Task<IActionResult> Index()
        {
            // Captura o ID do funcionário a partir das claims
            var claimFuncionarioId = User.Claims.FirstOrDefault(c => c.Type == "idFuncionario");
            if (claimFuncionarioId == null)
            {
                return Unauthorized("Você precisa estar logado como funcionário.");
            }

            int idFuncionario = int.Parse(claimFuncionarioId.Value);

            // Busca os protocolos onde fk_func é igual ao ID do funcionário
            var protocolos = await _context.Protocolo
                .Where(p => p.fk_func == idFuncionario)
                .ToListAsync();

            return View(protocolos); // Passa a lista de protocolos para a view
        }

        // POST: ProtocoloFuncionario/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id)
        {
            // Captura o ID do funcionário a partir das claims
            var claimFuncionarioId = User.Claims.FirstOrDefault(c => c.Type == "idFuncionario");
            if (claimFuncionarioId == null)
            {
                return Unauthorized("Você precisa estar logado como funcionário.");
            }

            int idFuncionario = int.Parse(claimFuncionarioId.Value);

            // Busca o protocolo pelo ID
            var protocolo = await _context.Protocolo.FindAsync(id);
            if (protocolo == null)
            {
                return NotFound("Protocolo não encontrado.");
            }

            // Substitui o fk_func pelo ID do funcionário que clicou no botão
            protocolo.fk_func = idFuncionario;

            // Salva as alterações no banco de dados
            _context.Update(protocolo);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index)); // Redireciona para a lista de protocolos
        }
    }
}