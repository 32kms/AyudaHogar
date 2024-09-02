using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AyudaHogar.Models;
using System.Security.Claims;
using AyudaHogar.Services;

namespace AyudaHogar.Pages
{
    public class ServicioDetalleModel : PageModel
    {
        private readonly AyudaHogar.Models.AyudahogardbContext _context;
        private readonly BlobService _blobService;
        private readonly UserService _userService;
        public ServicioDetalleModel(AyudaHogar.Models.AyudahogardbContext context, BlobService blobService, UserService userService)
        {
            _context = context;
            _blobService = blobService;
            _userService = userService;
        }

        public Servicio Servicio { get; set; }
        public string FotoPerfilUrl { get; set; }
        public string DiaSemana(int dia)
        {
            string[] dias = { "Lunes", "Martes", "Miércoles", "Jueves", "Viernes", "Sábado", "Domingo" };
            return dias[dia - 1];
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Servicio = await _context.Servicios
            .Include(s => s.Prov)
            .Include(s => s.Bloquetiempos) // Asegúrate de incluir los bloques de tiempo aquí
            .Include(s => s.Comentarios)
            .FirstOrDefaultAsync(m => m.ServId == id);

            if (Servicio == null)
            {
                return NotFound();
            }

            ViewData["Title"] = "Detalle del Servicio: " + Servicio.ServDetalle;

            // Verifica si el nombre del blob es null o vacío antes de intentar generar el token SAS
            if (string.IsNullOrWhiteSpace(Servicio.Prov.ProvFotoPerfilUrl))
            {
                // Asigna un valor predeterminado que indique que no hay imagen disponible
                FotoPerfilUrl = "Sin imagen disponible";
            }
            else
            {
                // Si hay un nombre de blob, intenta generar el token SAS
                try
                {
                    FotoPerfilUrl = _blobService.GenerateSasTokenForBlob(Servicio.Prov.ProvFotoPerfilUrl);
                }
                catch (ArgumentNullException)
                {
                    // En caso de un error, asigna un valor predeterminado
                    FotoPerfilUrl = "Sin imagen disponible";
                }
            }

            return Page();
        }



        public async Task<IActionResult> OnPostAddCommentAsync(int ServId, string CommentText, int ComPuntuacion)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Utiliza UserService para obtener el usuario actual
            var usuario = await _userService.AuthenticateUserAndGetUser(User);
            if (usuario == null)
            {
                // Considera manejar este caso de manera más específica, por ejemplo, redirigiendo al usuario a una página de error o de inicio de sesión
                return Page();
            }

            var comentario = new Comentario
            {
                ServId = ServId,
                ComDetalle = CommentText,
                UsuId = usuario.UsuId,
                ComPuntuacion = ComPuntuacion,
            };

            _context.Comentarios.Add(comentario);
            await _context.SaveChangesAsync();

            return RedirectToPage(new { id = ServId });
        }
    }
}
