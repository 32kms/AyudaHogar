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
    public class DetalleReporteModel : PageModel
    {
        private readonly AyudaHogar.Models.AyudahogardbContext _context;

        public DetalleReporteModel(AyudaHogar.Models.AyudahogardbContext context)
        {
            _context = context;
        }

        public Report Report { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id, int? servId)
        {
            if (id == null || servId == null)
            {
                return NotFound();
            }

            Report = await _context.Reports
                .Include(r => r.Serv)
                    .ThenInclude(s => s.Prov) // Carga el proveedor del servicio si es necesario
                .FirstOrDefaultAsync(m => m.RepId == id && m.ServId == servId);

            if (Report == null)
            {
                return NotFound();
            }

            // No es necesario hacer algo más con servId aquí, ya que estamos cargando el servicio a través de la relación en Report

            return Page();
        }


        public async Task<IActionResult> OnPostCambiarEstadoAsync(int repId, int nuevoEstado)
        {
            var reporte = await _context.Reports.FindAsync(repId);
            if (reporte == null)
            {
                return NotFound();
            }

            reporte.ErId = nuevoEstado; // Asume que nuevoEstado es un ID de estado válido
            await _context.SaveChangesAsync();

            // Redirige al usuario a la página ReportesAdministrador
            return RedirectToPage("./ReportesAdministrador");
        }
        public async Task<IActionResult> OnPostBloquearProveedorAsync(int repId, int provId)
        {
            var proveedorBloqueado = new Proveedorbloqueado
            {
                ProvId = provId,
                PbDescripcion = null // La descripción puede ser nula por ahora
            };

            _context.Proveedorbloqueados.Add(proveedorBloqueado);
            await _context.SaveChangesAsync();

            // Redirige al usuario a la página deseada después de bloquear el proveedor
            return RedirectToPage("./ReportesAdministrador");
        }
    }
}
