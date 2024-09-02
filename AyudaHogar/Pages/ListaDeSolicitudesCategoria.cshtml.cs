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
    public class ListaDeSolicitudesCategoriaModel : PageModel
    {
        private readonly AyudaHogar.Models.AyudahogardbContext _context;

        public ListaDeSolicitudesCategoriaModel(AyudaHogar.Models.AyudahogardbContext context)
        {
            _context = context;
        }

        public IList<Solicitudnuevacategorium> Solicitudnuevacategorium { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Solicitudnuevacategorium = await _context.Solicitudnuevacategoria
                .Include(s => s.Usu).ToListAsync();
        }
    }
}
