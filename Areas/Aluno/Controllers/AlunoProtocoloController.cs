using Microsoft.AspNetCore.Mvc;
using pdtcc_doc_academy.Models;
using pdtcc_doc_academy.Repositories;
using System.Threading.Tasks;

namespace pdtcc_doc_academy.Areas.Aluno.Controllers
{
    [Area("Aluno")]
    public class AlunoProtocoloController : Controller
    {
        private readonly IProtocoloRepository _protocoloRepository;
        private readonly IAlunoRepository _alunoRepository;
        private readonly IComunicadoRepository _comunicadoRepository;
        private readonly IAutorizacaoRepository _autorizacaoRepository;

        public AlunoProtocoloController(IProtocoloRepository protocoloRepository, IAlunoRepository alunoRepository, IComunicadoRepository comunicadoRepository, IAutorizacaoRepository autorizacaoRepository)
        {
            _protocoloRepository = protocoloRepository;
            _alunoRepository = alunoRepository;
            _comunicadoRepository = comunicadoRepository;
            _autorizacaoRepository = autorizacaoRepository;
        }

        public IActionResult Requisitar()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Requisitar(int selectedOption, int idaluno)
        {
            switch (selectedOption)
            {
                case 1:
                    return await HandleAtestadoMatricula(idaluno);
                case 2:
                    return await HandleAutorizacao();
                case 3:
                    return await HandleComunicado();
                default:
                    // Lógica para opção inválida
                    return RedirectToAction("Requisitar");
            }
        }

        private async Task<IActionResult> HandleAtestadoMatricula(int id)
        {
            var protocolo = new Protocolo
            {
                tipo_Doc = "Atestado Matricula"
                //fk_aluno = _alunoRepository.GetById(id),
                // Preencha outros campos conforme necessário
            };
            await _protocoloRepository.Add(protocolo);

            return View("Requisitar", protocolo);
        }

        private async Task<IActionResult> HandleAutorizacao()
        {
            var protocolo = new Protocolo
            {
                tipo_Doc = "Autorização"
                // Preencha outros campos conforme necessário
            };
            await _protocoloRepository.Add(protocolo);

            return View("AutorizacaoView");
        }

        private async Task<IActionResult> HandleComunicado()
        {
            var protocolo = new Protocolo
            {
                tipo_Doc = "Comunicado"
                // Preencha outros campos conforme necessário
            };
            await _protocoloRepository.Add(protocolo);

            return View("ComunicadoView");
        }
    }
}