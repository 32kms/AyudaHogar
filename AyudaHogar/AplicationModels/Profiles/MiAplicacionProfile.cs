using AutoMapper;
using AyudaHogar.Models;
using AyudaHogar.AplicationModels;

namespace AyudaHogar.AplicationModels.Profiles
{
    public class MiAplicacionProfile : Profile
    {
        public MiAplicacionProfile()
        {
            CreateMap<Categoriadeservicio, CategoriadeservicioDTO>();
            CreateMap<Comuna, ComunaDTO>();
            CreateMap<Region, RegionDTO>();

            CreateMap<Servicio, ServicioDTO>()
                .ForMember(dest => dest.EsPremium, opt => opt.MapFrom(src =>
                    src.Prov.Pspremia.Any(p => p.EpspId == 1 &&
                    p.PspFechaInicio <= DateTime.Now &&
                    (p.PspFechaInicio.Value.AddDays(p.PspDuracion.Value) >= DateTime.Now))))
                .ForMember(dest => dest.ProvFotoPerfilUrl, opt => opt.MapFrom(src => src.Prov.ProvFotoPerfilUrl))
                .ForMember(dest => dest.ProvId, opt => opt.MapFrom(src => src.ProvId))
                .ForMember(dest => dest.ProvNom, opt => opt.MapFrom(src => src.Prov.ProvNom))
                .ForMember(dest => dest.CatNom, opt => opt.MapFrom(src => src.Cat.CatNom))
                .ForMember(dest => dest.Cat, opt => opt.MapFrom(src => src.Cat))
                ;
        }
    }
}
