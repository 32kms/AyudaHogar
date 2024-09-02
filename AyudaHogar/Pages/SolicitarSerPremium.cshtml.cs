using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using AyudaHogar.Models;
using AyudaHogar.Services;

namespace AyudaHogar.Pages
{
    public class SolicitarSerPremiumModel : PageModel
    {
        private readonly AyudaHogar.Models.AyudahogardbContext _context;
        private readonly UserService _userService;

        public SolicitarSerPremiumModel(AyudaHogar.Models.AyudahogardbContext context, UserService userService)
        {
            _context = context;
            _userService = userService;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var usuario = await _userService.VerifyOrAddUserAndGetUser(User);
            var proveedor = _context.Proveedordeservicios.FirstOrDefault(p => p.UsuId == usuario.UsuId); // Asumiendo que existe una relación entre usuarios y proveedores

            if (proveedor == null)
            {
                // Manejar el caso en que el usuario no es un proveedor
                return RedirectToPage("./Error");
            }

            ViewData["PpId"] = new SelectList(_context.PlanesPremia.Select(x => new { PpId = x.PpId, Descripcion = $"{x.PpDescripcion} - ${x.PpPrecio}" }), "PpId", "Descripcion");
            Solicitudpremium = new Solicitudpremium
            {
                ProvId = proveedor.ProvId, // Usar el ID del proveedor
                SpeId = 1 // Estado "Enviado" por defecto
            };
            return Page();
        }

        [BindProperty]
        public Solicitudpremium Solicitudpremium { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Solicitudpremia.Add(Solicitudpremium);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Perfil"); // Asegúrate de redirigir a la página correcta después de guardar
        }
    }
}
