namespace AyudaHogar.AplicationModels
{
    public class ServicioDTO
    {
        public int ServId { get; set; }
        public string? ServDetalle { get; set; }
        public int? CatId { get; set; }
        public int? ComunaId { get; set; }
        public List<BloquetiempoDTO> Bloquetiempos { get; set; }
        public CategoriadeservicioDTO? Cat { get; set; }
        public List<ComentarioDTO> Comentarios { get; set; }
        public ComunaDTO? Comuna { get; set; }
        public List<ReportDTO> Reports { get; set; }
        public bool EsPremium { get; set; }
        public string? ProvFotoPerfilUrl { get; set; }
        public int? ProvId { get; set; }
        public string? ProvNom { get; set; } // Nombre del proveedor
        public string? CatNom { get; set; } // Nombre de la categoría

    }
}
