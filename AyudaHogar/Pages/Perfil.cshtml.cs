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
    public class PerfilModel : PageModel
    {
        private readonly AyudaHogar.Models.AyudahogardbContext _context;

        public PerfilModel(AyudaHogar.Models.AyudahogardbContext context)
        {
            _context = context;
        }

        public Usuario Usuario { get; set; } = default!;
        public string NombreRol { get; set; } = string.Empty;
        public bool EsProveedor { get; set; } = false;
        public bool EsProveedorPromocionado { get; set; } = false;

        public async Task<IActionResult> OnGetAsync()
        {
            var userId = User.FindFirst("http://schemas.microsoft.com/identity/claims/objectidentifier")?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                return NotFound();
            }

            Usuario = await _context.Usuarios.Include(u => u.Proveedordeservicios).FirstOrDefaultAsync(m => m.AzureAduserId == userId);

            if (Usuario == null)
            {
                return NotFound();
            }

            NombreRol = ConvertirCodigoRolANombre(Usuario.UsuRol);
            EsProveedor = Usuario.UsuRol == "2";
            EsProveedorPromocionado = Usuario.Proveedordeservicios.Any(p => p.Pspremia.Any());

            return Page();
        }

        private string ConvertirCodigoRolANombre(string? codigoRol)
        {
            switch (codigoRol)
            {
                case "1":
                    return "Usuario";
                case "2":
                    return "Proveedor de servicios";
                case "3":
                    return "Administrador";
                default:
                    return "Desconocido";
            }
        }
    }
}
