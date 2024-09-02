namespace AyudaHogar.AplicationModels
{
    public class EstadoserviciopremiumDTO
    {
        public int EpspId { get; set; }
        public string? EpspNom { get; set; }
        public string? EpspDescripcion { get; set; }
        public List<PspremiumDTO> Pspremium { get; set; }
    }
}
