using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AyudaHogar.Models;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace AyudaHogar.Pages
{
    public class ReportesAdministradorModel : PageModel
    {
        private readonly AyudaHogar.Models.AyudahogardbContext _context;

        public ReportesAdministradorModel(AyudaHogar.Models.AyudahogardbContext context)
        {
            _context = context;
        }

        public IList<Report> ReportesEnviados { get; set; } = new List<Report>();
        public IList<Report> ReportesEnProceso { get; set; } = new List<Report>();
        public IList<Report> ReportesCerrados { get; set; } = new List<Report>();

        public async Task OnGetAsync()
        {
            // Obtiene la lista de IDs de proveedores bloqueados
            var proveedoresBloqueadosIds = await _context.Proveedorbloqueados
                                                          .Select(pb => pb.ProvId)
                                                          .ToListAsync();

            var reportes = await _context.Reports
                .Include(r => r.Er)
                .Include(r => r.Serv)
                    .ThenInclude(s => s.Prov) // Asegúrate de incluir el proveedor del servicio
                .Include(r => r.Usu)
                .Where(r => !proveedoresBloqueadosIds.Contains(r.Serv.Prov.ProvId)) // Excluye reportes de proveedores bloqueados
                .ToListAsync();

            // Asumiendo que los estados son: 1-Rechazado, 2-En proceso, 3-Aceptado, 4-Enviado
            ReportesEnviados = reportes.Where(r => r.Er?.ErId == 4).ToList();
            ReportesEnProceso = reportes.Where(r => r.Er?.ErId == 2).ToList();
            ReportesCerrados = reportes.Where(r => r.Er?.ErId == 1 || r.Er?.ErId == 3).ToList();
        }
        
    }
}
