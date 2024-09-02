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
    public class CategoriasServicioModel : PageModel
    {
        private readonly AyudaHogar.Models.AyudahogardbContext _context;

        public CategoriasServicioModel(AyudaHogar.Models.AyudahogardbContext context)
        {
            _context = context;
        }

        public IList<Categoriadeservicio> Categoriadeservicio { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Categoriadeservicio = await _context.Categoriadeservicios.ToListAsync();
        }
        public async Task<IActionResult> OnPostDeleteAsync(int catId)
        {
            var categoria = await _context.Categoriadeservicios.FindAsync(catId);
            if (categoria != null)
            {
                _context.Categoriadeservicios.Remove(categoria);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage();
        }
        public async Task<IActionResult> OnGetEliminarAsync(int? catId)
        {
            if (catId == null)
            {
                return NotFound();
            }

            var categoria = await _context.Categoriadeservicios.FindAsync(catId);
            if (categoria != null)
            {
                _context.Categoriadeservicios.Remove(categoria);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage();
        }

    }
}
