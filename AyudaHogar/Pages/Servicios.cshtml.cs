using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AyudaHogar.Models;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using AutoMapper;
using AyudaHogar.AplicationModels;

namespace AyudaHogar.Pages
{
    public class ServiciosModel : PageModel
    {
        private readonly AyudahogardbContext _context;
        private readonly BlobService _blobService;
        private readonly IMapper _mapper;

        public ServiciosModel(AyudahogardbContext context, BlobService blobService, IMapper mapper)
        {
            _context = context;
            _blobService = blobService;
            _mapper = mapper;
        }

        public IList<ServicioDTO> Servicio { get; set; } = default!;
        public Dictionary<int, string> FotoPerfilUrls { get; set; } = new Dictionary<int, string>();
        public IList<Comuna> Comunas { get; set; } = default!;
        public IList<Region> Regiones { get; set; } = default!;

        public async Task OnGetAsync(int? categoriaId, int? comunaId, int? regionId)
        {
            var proveedoresBloqueadosIds = _context.Proveedorbloqueados.Select(pb => pb.ProvId).Distinct();

            IQueryable<Servicio> query = _context.Servicios
                .Include(s => s.Cat)
                .Include(s => s.Comuna).ThenInclude(c => c.Region)
                .Include(s => s.Prov)
                .Where(s => !proveedoresBloqueadosIds.Contains(s.ProvId));

            if (categoriaId.HasValue)
            {
                query = query.Where(s => s.CatId == categoriaId);
            }

            if (comunaId.HasValue)
            {
                query = query.Where(s => s.ComunaId == comunaId);
            }
            else if (regionId.HasValue)
            {
                query = query.Where(s => s.Comuna.RegionId == regionId);
            }

            var servicios = await query.ToListAsync();

            // Mapear a DTO
            Servicio = _mapper.Map<List<ServicioDTO>>(servicios);

            // Cargar comunas y regiones para los filtros de la vista
            Comunas = await _context.Comunas.OrderBy(c => c.ComunaNombre).ToListAsync();
            Regiones = await _context.Regions.OrderBy(r => r.RegNombre).ToListAsync();

            // Generar URLs de fotos de perfil para cada servicio
            foreach (var servicio in Servicio)
            {
                var blobName = servicio.ProvFotoPerfilUrl;
                if (!string.IsNullOrEmpty(blobName) && servicio.ProvId.HasValue)
                {
                    var sasUrl = _blobService.GenerateSasTokenForBlob(blobName);
                    FotoPerfilUrls.Add(servicio.ProvId.Value, sasUrl);
                }
            }
        }

        public async Task<JsonResult> OnGetComunasAsync(int regionId)
        {
            var comunas = await _context.Comunas
                                        .Where(c => c.RegionId == regionId)
                                        .OrderBy(c => c.ComunaNombre)
                                        .Select(c => new
                                        {
                                            comunaId = c.ComunaId,
                                            comunaNombre = c.ComunaNombre
                                        })
                                        .ToListAsync();

            return new JsonResult(comunas);
        }
    }
}
