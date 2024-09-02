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
    public class ProveedoresAdminModel : PageModel
    {
        private readonly AyudaHogar.Models.AyudahogardbContext _context;

        public ProveedoresAdminModel(AyudaHogar.Models.AyudahogardbContext context)
        {
            _context = context;
        }

        public IList<Proveedordeservicio> ProveedoresPremium { get; set; } = default!;
        public IList<Proveedordeservicio> ProveedoresBloqueados { get; set; } = default!;
        public IList<Proveedordeservicio> ProveedoresNormales { get; set; } = default!;

        public async Task OnGetAsync()
        {
            var todosLosProveedores = await _context.Proveedordeservicios
                .Include(p => p.Usu)
                .Include(p => p.Pspremia)
                .Include(p => p.Proveedorbloqueados)
                .ToListAsync();

            ProveedoresPremium = todosLosProveedores
                .Where(p => p.Pspremia.Any(pp => pp.EpspId == 1))
                .ToList();

            ProveedoresBloqueados = todosLosProveedores
                .Where(p => p.Proveedorbloqueados.Any())
                .ToList();

            // Excluye los proveedores que ya están en las listas de premium y bloqueados
            ProveedoresNormales = todosLosProveedores
                .Except(ProveedoresPremium)
                .Except(ProveedoresBloqueados)
                .ToList();
        }
    }
}
