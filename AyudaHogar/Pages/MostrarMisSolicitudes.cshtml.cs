using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AyudaHogar.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AyudaHogar.Pages
{
    public class MostrarMisSolicitudesModel : PageModel
    {
        private readonly AyudaHogar.Models.AyudahogardbContext _context;

        public MostrarMisSolicitudesModel(AyudaHogar.Models.AyudahogardbContext context)
        {
            _context = context;
        }

        public IList<Solicitudproveedordeservicio> Solicitudes { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Solicitudes = await _context.Solicitudproveedordeservicios
                .Include(s => s.Esp)
                .Include(s => s.Usu)
                .Where(m => m.UsuId == id)
                .ToListAsync();

            if (Solicitudes == null || Solicitudes.Count == 0)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
