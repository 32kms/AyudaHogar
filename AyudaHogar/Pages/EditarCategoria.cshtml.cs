using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AyudaHogar.Models;

namespace AyudaHogar.Pages
{
    public class EditarCategoriaModel : PageModel
    {
        private readonly AyudaHogar.Models.AyudahogardbContext _context;

        public EditarCategoriaModel(AyudaHogar.Models.AyudahogardbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Categoriadeservicio Categoriadeservicio { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoriadeservicio =  await _context.Categoriadeservicios.FirstOrDefaultAsync(m => m.CatId == id);
            if (categoriadeservicio == null)
            {
                return NotFound();
            }
            Categoriadeservicio = categoriadeservicio;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Categoriadeservicio).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoriadeservicioExists(Categoriadeservicio.CatId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool CategoriadeservicioExists(int id)
        {
            return _context.Categoriadeservicios.Any(e => e.CatId == id);
        }
    }
}
