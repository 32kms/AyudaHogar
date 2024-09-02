namespace AyudaHogar.AplicationModels
{
    public class UsuarioDTO
    {
        public int UsuId { get; set; }
        public string? AzureAduserId { get; set; }
        public string? UsuNom { get; set; }
        public string? UsuApe { get; set; }
        public string? UsuMail { get; set; }
        public string? UsuFotoPerfilUrl { get; set; }
        public string? UsuPhone { get; set; }
        public string? UsuRol { get; set; }
        // Agregar las colecciones de Comentarios y Reports
        public List<ComentarioDTO>? Comentarios { get; set; }
        public List<ReportDTO>? Reports { get; set; }
    }
}
