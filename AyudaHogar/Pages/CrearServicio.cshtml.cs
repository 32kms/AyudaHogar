using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AyudaHogar.Models;
using AyudaHogar.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AyudaHogar.Pages.Servicios
{
    public class CrearServicioModel : PageModel
    {
        private readonly AyudahogardbContext _context;
        private readonly UserService _userService;
        public int? SelectedRegionId { get; set; }

        public CrearServicioModel(AyudahogardbContext context, UserService userService)
        {
            _context = context;
            _userService = userService;
        }

        [BindProperty]
        public Servicio Servicio { get; set; }

        public List<SelectListItem> Categorias { get; set; }
        public List<SelectListItem> Regiones { get; set; }
        public List<SelectListItem> Comunas { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            Categorias = await _context.Categoriadeservicios
                .Select(c => new SelectListItem { Value = c.CatId.ToString(), Text = c.CatNom })
                .ToListAsync();

            Regiones = await _context.Regions
                .Select(r => new SelectListItem { Value = r.RegId.ToString(), Text = r.RegNombre })
                .ToListAsync();

            // Las comunas se cargarán dinámicamente en base a la región seleccionada

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                await LoadSelectLists();
                return Page();
            }

            try
            {
                var user = await _userService.AuthenticateUserAndGetUser(User);
                var proveedor = await _context.Proveedordeservicios.FirstOrDefaultAsync(p => p.UsuId == user.UsuId);

                if (proveedor == null)
                {
                    ModelState.AddModelError(string.Empty, "No se encontró el proveedor de servicios asociado al usuario.");
                    await LoadSelectLists();
                    return Page();
                }

                Servicio.ProvId = proveedor.ProvId;
                _context.Servicios.Add(Servicio);
                await _context.SaveChangesAsync();

                // Redirige a MisServiciosProveedor con el ProvId como parámetro
                return RedirectToPage("./MisServiciosProveedor", new { provId = proveedor.ProvId });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Ocurrió un error al crear el servicio: " + ex.Message);
                await LoadSelectLists();
                return Page();
            }
        }

        private async Task LoadSelectLists()
        {
            Categorias = await _context.Categoriadeservicios
                .Select(c => new SelectListItem { Value = c.CatId.ToString(), Text = c.CatNom })
                .ToListAsync();

            Regiones = await _context.Regions
                .Select(r => new SelectListItem { Value = r.RegId.ToString(), Text = r.RegNombre })
                .ToListAsync();
        }



        // Método para cargar comunas basado en la región seleccionada (podría ser llamado vía AJAX)
        public async Task<JsonResult> OnGetComunasAsync(int regionId)
        {
            var comunas = await _context.Comunas
                .Where(c => c.RegionId == regionId)
                .Select(c => new { c.ComunaId, c.ComunaNombre })
                .ToListAsync();

            return new JsonResult(comunas);
        }
    }
}
