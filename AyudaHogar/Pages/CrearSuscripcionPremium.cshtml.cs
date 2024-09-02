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
    public class CrearSuscripcionPremiumModel : PageModel
    {
        private readonly AyudaHogar.Models.AyudahogardbContext _context;

        public CrearSuscripcionPremiumModel(AyudaHogar.Models.AyudahogardbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["EpspId"] = new SelectList(_context.Estadoserviciopremia, "EpspId", "EpspId");
        ViewData["ProvId"] = new SelectList(_context.Proveedordeservicios, "ProvId", "ProvId");
            return Page();
        }

        [BindProperty]
        public Pspremium Pspremium { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Pspremia.Add(Pspremium);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
