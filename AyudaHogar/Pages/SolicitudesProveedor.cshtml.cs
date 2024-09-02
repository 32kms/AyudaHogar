using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AyudaHogar.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AyudaHogar.Pages
{
    public class SolicitudesProveedorModel : PageModel
    {
        private readonly AyudaHogar.Models.AyudahogardbContext _context;

        public SolicitudesProveedorModel(AyudaHogar.Models.AyudahogardbContext context)
        {
            _context = context;
        }

        public IList<Solicitudproveedordeservicio> SolicitudesPendientes { get; set; } = default!;
        public IList<Solicitudproveedordeservicio> SolicitudesAceptadas { get; set; } = default!;
        public IList<Solicitudproveedordeservicio> SolicitudesRechazadas { get; set; } = default!;

        public async Task OnGetAsync()
        {
            var todasLasSolicitudes = _context.Solicitudproveedordeservicios
                .Include(s => s.Esp)
                .Include(s => s.Usu);

            SolicitudesPendientes = await todasLasSolicitudes
                .Where(s => s.Esp.EspId == 1 && s.Usu.UsuRol != "2")
                .ToListAsync();

            SolicitudesAceptadas = await todasLasSolicitudes
                .Where(s => s.Esp.EspId == 2)
                .ToListAsync();

            SolicitudesRechazadas = await todasLasSolicitudes
                .Where(s => s.Esp.EspId == 3)
                .ToListAsync();
        }
    }
}
