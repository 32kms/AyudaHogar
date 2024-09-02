namespace AyudaHogar.AplicationModels
{
    public class SolicitudpremiumDTO
    {
        public int SpId { get; set; }
        public int? ProvId { get; set; }
        public string? SpDescripcion { get; set; }
        public int? SpeId { get; set; }
        public ProveedordeservicioDTO? Prov { get; set; }
        public SolicitudpremiumestadoDTO? Spe { get; set; }
    }
}
