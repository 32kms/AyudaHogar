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
    public class DetalleProveedorUsuarioModel : PageModel
{
    private readonly AyudaHogar.Models.AyudahogardbContext _context;
    private readonly BlobService _blobService;

    public DetalleProveedorUsuarioModel(AyudaHogar.Models.AyudahogardbContext context, BlobService blobService)
    {
        _context = context;
        _blobService = blobService;
    }

    public Proveedordeservicio Proveedordeservicio { get; set; } = default!;
    public List<Servicio> Servicios { get; set; } = new List<Servicio>();
    public string FotoPerfilUrl { get; set; }

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var proveedordeservicio = await _context.Proveedordeservicios
            .Include(p => p.Servicios)
                .ThenInclude(s => s.Cat)
            .FirstOrDefaultAsync(m => m.ProvId == id);

        if (proveedordeservicio == null)
        {
            return NotFound();
        }
        else
        {
            Proveedordeservicio = proveedordeservicio;
            Servicios = Proveedordeservicio.Servicios.ToList();
            // Genera la URL SAS para la imagen del perfil
            FotoPerfilUrl = _blobService.GenerateSasTokenForBlob(Proveedordeservicio.ProvFotoPerfilUrl);
        }
        return Page();
    }

    private string ObtenerNombreBlobDeUrl(string url)
    {
        // Este es un ejemplo básico, asegúrate de adaptarlo a tu caso específico
        var uri = new Uri(url);
        var blobName = uri.Segments.Last(); // Obtiene el último segmento de la URL, que sería el nombre del blob
        return blobName;
    }
}

}
