namespace AyudaHogar.AplicationModels
{
    public class RegionDTO
    {
        public int RegId { get; set; }
        public string? RegNombre { get; set; }
        public List<ProvinciaDTO> Provincias { get; set; }
    }
}
