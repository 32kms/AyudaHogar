namespace AyudaHogar.AplicationModels
{
    public class ComunaDTO
    {
        public int ComunaId { get; set; }
        public string? ComunaNombre { get; set; }
        public int? ProvinciaId { get; set; }
        public ProvinciaDTO? Provincia { get; set; }
        public List<ServicioDTO> Servicios { get; set; }
    }
}
