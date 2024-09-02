namespace AyudaHogar.AplicationModels
{
    public class ProveedordeservicioDTO
    {
        public int ProvId { get; set; }
        public string? ProvNom { get; set; }
        public string? ProvPhone { get; set; }
        public string? ProvFotoPerfilUrl { get; set; }
        public string? ProvRut { get; set; }
        public string? ProvFotoCarnet { get; set; }
        public List<ProveedorbloqueadoDTO> Proveedoresbloqueados { get; set; }
        public List<PspremiumDTO> Pspremia { get; set; }
        public List<SolicitudpremiumDTO> Solicitudespremium { get; set; }
        // Nueva propiedad para indicar si el proveedor tiene una suscripción premium activa
    }
}