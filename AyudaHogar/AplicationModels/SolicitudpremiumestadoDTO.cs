namespace AyudaHogar.AplicationModels
{
    public class SolicitudpremiumestadoDTO
    {
        public int SpeId { get; set; }
        public string? SpeDescripcion { get; set; }
        public List<SolicitudpremiumDTO>? Solicitudespremium { get; set; }

    }
}
