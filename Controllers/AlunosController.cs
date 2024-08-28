using Microsoft.AspNetCore.Mvc;
using pdtcc_doc_academy.Models;

namespace pdtcc_doc_academy.Controllers
{
    [Area("Alunos")]
    public class AlunosController : Controller
    {

        private readonly IAlunoRepository _alunoRepository;

        public AdminCategoriasController(IAlunoRepository alunoRepository)
        {
            _alunoRepository = alunoRepository;
        }

        // GET: Admin/AdminCategorias
        public async Task<IActionResult> Index()
        {
            return await _alunoRepository.GetAll() != null ?
                        View(await _alunoRepository.GetAll()) :
                        Problem("Entity set 'AppDBContext.Alunos'  is null.");
        }

        // GET: Admin/AdminCategorias/Detalhes/5
        public async Task<IActionResult> Detalhes(int id)
        {
            var aluno = await _alunoRepository.GetById(id);
            if (aluno == null)
            {
                return NotFound();
            }

            return View(aluno);
        }

        // GET: Admin/AdminCategorias/Cadastro
        public IActionResult Cadastro()
        {
            return View();
        }

        // POST: Admin/AdminCategorias/Cadastro
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Cadastro([Bind("IdAluno,nomeAluno,cpfAluno,cursoAluno,rmAluno,senhaAluno")] Aluno aluno)
        {
            if (Aluno != null)
            {
                await _alunoRepository.Add(aluno);
                return RedirectToAction(nameof(Index));
            }
            return View(aluno);
        }

        // GET: Admin/Categorias/Edicao/5
        public async Task<IActionResult> Edicao(int id)
        {
            var aluno = await _alunoRepository.GetById(id);
            if (aluno == null)
            {
                return NotFound();
            }
            return View(aluno);
        }

        // POST: Admin/Categorias/Edicao/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edicao(int id, [Bind("IdAluno,nomeAluno,cpfAluno,cursoAluno,rmAluno,senhaAluno")] Aluno aluno)
        {
            if (id != Aluno.Id)
            {
                return NotFound();
            }

            if (aluno != null)
            {
                try
                {
                    await _alunoRepository.Update(aluno);
                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(aluno);
        }

        // GET: Admin/Categorias/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var aluno = await _alunoRepository.GetById(id);
            if (aluno == null)
            {
                return NotFound();
            }

            return View(aluno);
        }

        // POST: Admin/Categorias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var aluno = await _alunoRepository.GetById(id);
            if (aluno != null)
            {
                await _alunoRepository.Delete(aluno);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}