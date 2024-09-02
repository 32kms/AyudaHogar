namespace AyudaHogar.AplicationModels
{
    public class ProveedorbloqueadoDTO
    {
        public int PbId { get; set; }
        public int? ProvId { get; set; }
        public string? PbDescripcion { get; set; }
        public ProveedordeservicioDTO? Prov { get; set; }
    }
}
