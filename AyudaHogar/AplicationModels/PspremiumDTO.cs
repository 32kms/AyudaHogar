namespace AyudaHogar.AplicationModels
{
    public class PspremiumDTO
    {
        public int PspId { get; set; }
        public string? PspNom { get; set; }
        public string? PspDescripcion { get; set; }
        public int? PspDuracion { get; set; }
        public DateTime? PspFechaInicio { get; set; }
        public int? ProvId { get; set; }
        public int? EpspId { get; set; }
        public EstadoserviciopremiumDTO? Epsp { get; set; }
        public ProveedordeservicioDTO? Prov { get; set; }
    }
}
