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
    public class EditarMiServicioModel : PageModel
    {
        private readonly AyudaHogar.Models.AyudahogardbContext _context;
        public string? ServDetalle { get; set; }

        public EditarMiServicioModel(AyudaHogar.Models.AyudahogardbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Servicio Servicio { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Servicio = await _context.Servicios
                .Include(s => s.Comuna)
                .ThenInclude(c => c.Region)
                .FirstOrDefaultAsync(m => m.ServId == id);

            if (Servicio == null)
            {
                return NotFound();
            }
            ViewData["CatId"] = new SelectList(_context.Categoriadeservicios, "CatId", "CatNom");
            ViewData["RegId"] = new SelectList(_context.Regions, "RegId", "RegNombre");
            ViewData["ComunaId"] = new SelectList(_context.Comunas, "ComunaId", "ComunaNombre");
            ViewData["CatId"] = new SelectList(_context.Categoriadeservicios, "CatId", "CatNom");
            ViewData["ProvId"] = new SelectList(_context.Proveedordeservicios, "ProvId", "ProvNom");

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Cargar el servicio existente de la base de datos para actualizarlo
            var servicioExistente = await _context.Servicios.FirstOrDefaultAsync(s => s.ServId == Servicio.ServId);

            if (servicioExistente == null)
            {
                return NotFound();
            }

            // Actualizar los campos necesarios del servicio existente
            servicioExistente.ComunaId = Servicio.ComunaId;
            servicioExistente.CatId = Servicio.CatId;
            servicioExistente.ServDetalle = Servicio.ServDetalle;
            // Asegúrate de actualizar aquí cualquier otro campo que el usuario pueda modificar

            // No es necesario marcar explícitamente las propiedades como modificadas con este enfoque
            // Entity Framework detectará los cambios en las propiedades de la entidad cargada

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ServicioExists(servicioExistente.ServId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./DetalleServicioProveedor", new { id = servicioExistente.ServId, provId = servicioExistente.ProvId });
        }

        private bool ServicioExists(int id)
        {
            return _context.Servicios.Any(e => e.ServId == id);
        }

        public async Task<JsonResult> OnGetComunasPorRegionAsync(int regionId)
        {
            var comunas = await _context.Comunas
                .Where(c => c.RegionId == regionId)
                .Select(c => new { c.ComunaId, c.ComunaNombre })
                .ToListAsync();

            return new JsonResult(comunas);
        }
    }
}
