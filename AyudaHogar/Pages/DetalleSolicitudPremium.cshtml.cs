using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AyudaHogar.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AyudaHogar.Pages
{
    public class DetalleSolicitudPremiumModel : PageModel
    {
        private readonly AyudaHogar.Models.AyudahogardbContext _context;

        public DetalleSolicitudPremiumModel(AyudaHogar.Models.AyudahogardbContext context)
        {
            _context = context;
        }

        public Solicitudpremium Solicitudpremium { get; set; } = default!;
        public List<Solicitudpremiumestado> EstadosSolicitud { get; set; } = new List<Solicitudpremiumestado>();

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Solicitudpremium = await _context.Solicitudpremia
                .Include(s => s.Prov)
                    .ThenInclude(p => p.Usu)
                .Include(s => s.Pp)
                .Include(s => s.Spe)
                .FirstOrDefaultAsync(m => m.SpId == id);

            EstadosSolicitud = await _context.Solicitudpremiumestados.ToListAsync();

            if (Solicitudpremium == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id, int NuevoEstadoId)
        {
            if (id == null)
            {
                return NotFound();
            }

            var solicitudToUpdate = await _context.Solicitudpremia.FindAsync(id);

            if (solicitudToUpdate != null)
            {
                solicitudToUpdate.SpeId = NuevoEstadoId;
                await _context.SaveChangesAsync();
                return RedirectToPage(new { id = solicitudToUpdate.SpId });
            }

            return Page();
        }

        public async Task<IActionResult> OnPostCrearSuscripcionAsync(int id)
        {
            var solicitud = await _context.Solicitudpremia
                .Include(s => s.Prov)
                .Include(s => s.Pp)
                .FirstOrDefaultAsync(m => m.SpId == id);

            if (solicitud == null)
            {
                return NotFound();
            }

            var nuevaSuscripcion = new Pspremium
            {
                ProvId = solicitud.ProvId,
                PspNom = solicitud.Prov?.ProvNom,
                PspDescripcion = solicitud.Pp?.PpDescripcion,
                PspDuracion = solicitud.Pp?.PpDuracion,
                PspFechaInicio = DateTime.Now,
                EpspId = 1 // Asumiendo que el estado "Activo" tiene el ID 1
            };

            _context.Pspremia.Add(nuevaSuscripcion);
            await _context.SaveChangesAsync();

            // Redirige a la página que consideres adecuada después de crear la suscripción
            return RedirectToPage("./Index");
        }
    }
}
