namespace AyudaHogar.AplicationModels
{
    public class ProvinciaDTO
    {
        public int ProvinciaId { get; set; }
        public string? ProvinciaNombre { get; set; }
        public int? RegId { get; set; }
        public List<ComunaDTO> Comunas { get; set; } 
        public RegionDTO? Reg { get; set; }
    }
}
