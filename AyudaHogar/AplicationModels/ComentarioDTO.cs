namespace AyudaHogar.AplicationModels
{
    public class ComentarioDTO
    {
        public int ComId { get; set; }
        public string? ComDetalle { get; set; }
        public int? ComPuntuacion { get; set; }
        public string? ComFotoUrl { get; set; }
        public int? ServId { get; set; }
        public int? UsuId { get; set; }
        public ServicioDTO? Serv { get; set; }
        public UsuarioDTO? Usu { get; set; }
    }
}
