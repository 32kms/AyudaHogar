using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using AyudaHogar.Models;
using AyudaHogar.Services;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AyudaHogar.Pages
{
    public class SolicitarProveedorModel : PageModel
    {
        private readonly AyudaHogar.Models.AyudahogardbContext _context;
        private readonly BlobService _blobService;
        private readonly UserService _userService;

        [BindProperty]
        public Proveedordeservicio Proveedordeservicio { get; set; }
        [BindProperty]
        public IFormFile? UploadedProfilePicture { get; set; }
        [BindProperty]
        public string? SolicitudDescripcion { get; set; }
        [BindProperty]
        public IFormFile? UploadedFile { get; set; }

        public SolicitarProveedorModel(AyudaHogar.Models.AyudahogardbContext context, BlobService blobService, UserService userService)
        {
            _context = context;
            _blobService = blobService;
            _userService = userService;
        }

        public IActionResult OnGet()
        {
            ViewData["UsuId"] = new SelectList(_context.Usuarios, "UsuId", "UsuNom");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // Obtener el usuario conectado y su UsuId
            var usuario = await _userService.AuthenticateUserAndGetUser(User);
            if (usuario == null)
            {
                // Manejar el caso en que el usuario no esté autenticado o no se encuentre en la base de datos
                return Unauthorized(); // O cualquier otra respuesta adecuada
            }

            Proveedordeservicio.UsuId = usuario.UsuId;
            
            if (!ModelState.IsValid || UploadedFile == null || UploadedProfilePicture == null)
            {
                return Page();
            }
            // Buscar un proveedor de servicios existente para este usuario
            var proveedorExistente = await _context.Proveedordeservicios
                                                   .FirstOrDefaultAsync(p => p.UsuId == usuario.UsuId);

            // Generar nombres únicos para los blobs usando el UsuId
            var carnetBlobName = $"{usuario.UsuId}_carnet";
            var profilePictureBlobName = $"{usuario.UsuId}_profile";

            // Subir las imágenes al Blob Storage
            await _blobService.UploadFileToBlob(carnetBlobName, UploadedFile.OpenReadStream(), UploadedFile.ContentType);
            await _blobService.UploadFileToBlob(profilePictureBlobName, UploadedProfilePicture.OpenReadStream(), UploadedProfilePicture.ContentType);

            if (proveedorExistente != null)
            {
                // Actualizar el proveedor de servicios existente
                proveedorExistente.ProvNom = Proveedordeservicio.ProvNom;
                proveedorExistente.ProvPhone = Proveedordeservicio.ProvPhone;
                proveedorExistente.ProvFotoPerfilUrl = profilePictureBlobName;
                proveedorExistente.ProvFotoCarnet = carnetBlobName;
                // Actualizar cualquier otra propiedad necesaria aquí
            }
            else
            {
                // Crear un nuevo proveedor de servicios si no existe
                Proveedordeservicio.UsuId = usuario.UsuId;
                Proveedordeservicio.ProvFotoPerfilUrl = profilePictureBlobName;
                Proveedordeservicio.ProvFotoCarnet = carnetBlobName;
                // Asegúrate de completar los demás campos de Proveedordeservicio con los datos del formulario aquí

                _context.Proveedordeservicios.Add(Proveedordeservicio);
            }

            // Crear la solicitud de proveedor de servicio
            var solicitud = new Solicitudproveedordeservicio
            {
                UsuId = usuario.UsuId,
                SpsDescripcion = SolicitudDescripcion,
                EspId = 1 // Estado "Pendiente"
            };

            _context.Solicitudproveedordeservicios.Add(solicitud);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }

}
