using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ColegioSanJose.Models;
using System.Linq;
using System.Threading.Tasks;

namespace ColegioSanJose.Controllers
{
    public class AlumnosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AlumnosController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Alumnos.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var alumno = await _context.Alumnos.FirstOrDefaultAsync(m => m.AlumnoId == id);
            if (alumno == null) return NotFound();

            return View(alumno);
        }

        public IActionResult Create()
        {
            return View();
        }

        //snippet debugger

        //fin snippet

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Alumno alumno)
        {
            Console.WriteLine("DEBUG: Entrando al método Create");

            // Remover la validación de la propiedad de navegación Expedientes
            ModelState.Remove("Expedientes");

            Console.WriteLine($"DEBUG: ModelState.IsValid = {ModelState.IsValid}");

            if (ModelState.IsValid)
            {
                try
                {
                    Console.WriteLine($"DEBUG: Datos del alumno - Nombre: {alumno.Nombre}, Apellido: {alumno.Apellido}, Fecha: {alumno.FechaNacimiento}, Grado: {alumno.Grado}");

                    _context.Add(alumno);
                    Console.WriteLine("DEBUG: Alumno agregado al contexto");

                    int result = await _context.SaveChangesAsync();
                    Console.WriteLine($"DEBUG: SaveChangesAsync retornó: {result} filas afectadas");

                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"ERROR: {ex.Message}");
                    Console.WriteLine($"ERROR Inner: {ex.InnerException?.Message}");
                    ModelState.AddModelError("", $"Error al guardar: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("DEBUG: ModelState no es válido");
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine($"DEBUG Error: {error.ErrorMessage}");
                }
            }

            return View(alumno);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var alumno = await _context.Alumnos.FindAsync(id);
            if (alumno == null) return NotFound();

            return View(alumno);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AlumnoId,Nombre,Apellido,FechaNacimiento,Grado")] Alumno alumno)
        {
            if (id != alumno.AlumnoId) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(alumno);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AlumnoExists(alumno.AlumnoId)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(alumno);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var alumno = await _context.Alumnos.FirstOrDefaultAsync(m => m.AlumnoId == id);
            if (alumno == null) return NotFound();

            return View(alumno);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var alumno = await _context.Alumnos.FindAsync(id);
            _context.Alumnos.Remove(alumno);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AlumnoExists(int id)
        {
            return _context.Alumnos.Any(e => e.AlumnoId == id);
        }
    }
}