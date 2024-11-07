using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pdtcc_doc_academy.Models;
using pdtcc_doc_academy.Repositories;
using System.Linq;
using System.Threading.Tasks;

namespace pdtcc_doc_academy.Controllers
{
    public class CursoController : Controller
    {
        private readonly AppDBContext _context;

        public CursoController(AppDBContext context)
        {
            _context = context;
        }

        // GET: Curso
        public async Task<IActionResult> Index()
        {
            return View(await _context.Curso.ToListAsync());
        }

        // GET: Curso/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var curso = await _context.Curso
                .FirstOrDefaultAsync(m => m.IdCurso == id);
            if (curso == null)
            {
                return NotFound();
            }

            return View(curso);
        }

        // GET: Curso/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Curso/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Escola")]
        public async Task<IActionResult> Create(Curso curso)
        {

            _context.Add(curso);
            await _context.SaveChangesAsync();
            

            var alunoCurso = new AlunoCurso
            {
                IdCurso = curso.IdCurso,
                fk_aluno = 1
            };
            _context.Add(alunoCurso);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
            return View(curso);
        }

        // GET: Curso/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var curso = await _context.Curso.FindAsync(id);
            if (curso == null)
            {
                return NotFound();
            }
            return View(curso);
        }

        // POST: Curso/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdCurso,nomecurso")] Curso curso)
        {
            if (id != curso.IdCurso)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(curso);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CursoExists(curso.IdCurso))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(curso);
        }

        // GET: Curso/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var curso = await _context.Curso
                .FirstOrDefaultAsync(m => m.IdCurso == id);
            if (curso == null)
            {
                return NotFound();
            }

            return View(curso);
        }

        // POST: Curso/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var curso = await _context.Curso.FindAsync(id);
            _context.Curso.Remove(curso);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CursoExists(int id)
        {
            return _context.Curso.Any(e => e.IdCurso == id);
        }
    }
}