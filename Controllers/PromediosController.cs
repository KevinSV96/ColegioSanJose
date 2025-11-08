using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ColegioSanJose.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ColegioSanJose.Controllers
{
    public class PromediosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PromediosController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var promedios = await _context.Alumnos
                .Select(a => new
                {
                    Alumno = a,
                    Promedio = _context.Expedientes
                        .Where(e => e.AlumnoId == a.AlumnoId)
                        .Average(e => (double?)e.NotaFinal) ?? 0
                })
                .Select(x => new PromedioAlumnoViewModel
                {
                    AlumnoId = x.Alumno.AlumnoId,
                    NombreCompleto = x.Alumno.NombreCompleto,
                    Grado = x.Alumno.Grado,
                    PromedioNotas = (decimal)x.Promedio
                })
                .ToListAsync();

            return View(promedios);
        }
    }

    public class PromedioAlumnoViewModel
    {
        public int AlumnoId { get; set; }
        public string NombreCompleto { get; set; }
        public string Grado { get; set; }
        public decimal PromedioNotas { get; set; }
    }
}