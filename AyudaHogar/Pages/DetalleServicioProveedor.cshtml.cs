using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AyudaHogar.Models;

namespace AyudaHogar.Pages
{
    public class DetalleServicioProveedorModel : PageModel
    {
        private readonly AyudaHogar.Models.AyudahogardbContext _context;
        public IList<Comentario> Comentarios { get; set; } = new List<Comentario>();
        public double PromedioPuntuacion { get; set; }
        public Servicio Servicio { get; set; } = default!;

        public DetalleServicioProveedorModel(AyudaHogar.Models.AyudahogardbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync(int? id, int? provId)
        {
            if (id == null || provId == null)
            {
                return NotFound();
            }

            Servicio = await _context.Servicios
                .Include(s => s.Cat)
                .Include(s => s.Comuna)
                .Include(s => s.Prov)
                .Include(s => s.Comentarios) // Asegúrate de incluir los comentarios
                .FirstOrDefaultAsync(m => m.ServId == id && m.ProvId == provId);

            if (Servicio == null)
            {
                return NotFound();
            }

            Comentarios = Servicio.Comentarios.ToList();
            PromedioPuntuacion = Comentarios.Any() ? Comentarios.Average(c => c.ComPuntuacion ?? 0) : 0;

            return Page(); // Retorna la página con los datos cargados
        }
        public async Task<IActionResult> OnGetEliminarAsync(int? servId)
        {
            if (servId == null)
            {
                return NotFound();
            }

            Servicio = await _context.Servicios.FindAsync(servId);

            if (Servicio != null)
            {
                _context.Servicios.Remove(Servicio);
                await _context.SaveChangesAsync();
                // Redirige a la página que prefieras después de eliminar el servicio
                return RedirectToPage("./MisServiciosProveedor");
            }

            return NotFound();
        }

    }

}
