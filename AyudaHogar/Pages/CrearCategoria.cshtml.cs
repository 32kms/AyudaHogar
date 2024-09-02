using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using AyudaHogar.Models;

namespace AyudaHogar.Pages
{
    public class CrearCategoriaModel : PageModel
    {
        private readonly AyudaHogar.Models.AyudahogardbContext _context;

        public CrearCategoriaModel(AyudaHogar.Models.AyudahogardbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Categoriadeservicio Categoriadeservicio { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Categoriadeservicios.Add(Categoriadeservicio);
            await _context.SaveChangesAsync();

            // Cambio aquí: Redirigir a CategoriasServicio en lugar de Index
            return RedirectToPage("./CategoriasServicio");
        }

    }
}
