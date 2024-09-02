using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AyudaHogar.Models;

namespace AyudaHogar.Pages
{
    public class SolicitudesPremiumModel : PageModel
    {
        private readonly AyudaHogar.Models.AyudahogardbContext _context;

        public SolicitudesPremiumModel(AyudaHogar.Models.AyudahogardbContext context)
        {
            _context = context;
        }

        public IList<Solicitudpremium> Solicitudpremium { get; set; } = default!;

        public async Task OnGetAsync()
        {
            Solicitudpremium = await _context.Solicitudpremia
                .Include(s => s.Pp)
                .Include(s => s.Prov)
                .Include(s => s.Spe)
                .ToListAsync();
        }
    }
}
