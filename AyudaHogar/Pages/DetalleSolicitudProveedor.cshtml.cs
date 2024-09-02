using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AyudaHogar.Models;
using AyudaHogar.Services; // Asegúrate de incluir el espacio de nombres para BlobService

namespace AyudaHogar.Pages
{
    public class DetalleSolicitudProveedorModel : PageModel
    {
        private readonly AyudaHogar.Models.AyudahogardbContext _context;
        private readonly BlobService _blobService; // Inyecta BlobService

        public DetalleSolicitudProveedorModel(AyudaHogar.Models.AyudahogardbContext context, BlobService blobService)
        {
            _context = context;
            _blobService = blobService; // Inicializa BlobService
        }

        public Solicitudproveedordeservicio Solicitudproveedordeservicio { get; set; } = default!;
        public Proveedordeservicio ProveedorDeServicio { get; set; } = default!; // Agrega propiedad para ProveedorDeServicio

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Solicitudproveedordeservicio = await _context.Solicitudproveedordeservicios
                .Include(s => s.Esp)
                .Include(s => s.Usu)
                .Include(s => s.Usu.Proveedordeservicios)
                .FirstOrDefaultAsync(m => m.SpsId == id);

            if (Solicitudproveedordeservicio == null)
            {
                return NotFound();
            }

            ProveedorDeServicio = Solicitudproveedordeservicio.Usu.Proveedordeservicios.FirstOrDefault();

            // Asume que ProveedorDeServicio no es null y que las propiedades siempre contienen el nombre del blob
            ProveedorDeServicio.ProvFotoPerfilUrl = _blobService.GenerateSasTokenForBlob(ProveedorDeServicio.ProvFotoPerfilUrl);
            ProveedorDeServicio.ProvFotoCarnet = _blobService.GenerateSasTokenForBlob(ProveedorDeServicio.ProvFotoCarnet);

            return Page();
        }
        public async Task<IActionResult> OnPostAsync(int id, int EstadoSolicitud)
        {
            var solicitud = await _context.Solicitudproveedordeservicios.FindAsync(id);

            if (solicitud == null)
            {
                return NotFound();
            }

            // Actualiza el estado de la solicitud
            solicitud.EspId = EstadoSolicitud;
            await _context.SaveChangesAsync();

            // Si la solicitud es aceptada, cambia el rol del usuario a 2
            if (EstadoSolicitud == 2) // Asumiendo que 2 es el estado "Aceptado"
            {
                var usuario = await _context.Usuarios.FindAsync(solicitud.UsuId);
                if (usuario != null)
                {
                    usuario.UsuRol = "2"; // Asumiendo que "2" es el rol de proveedor de servicios
                    await _context.SaveChangesAsync();
                }
            }

            return RedirectToPage("./SolicitudesProveedor"); // Redirige a la página que consideres adecuada
        }
    }
}