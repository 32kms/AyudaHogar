namespace AyudaHogar.AplicationModels
{
    public class EstadoreportDTO
    {
        public int ErId { get; set; }
        public string? ErNombre { get; set; }
        public List<ReportDTO> Reports { get; set; }
    }
}
