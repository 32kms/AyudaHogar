namespace AyudaHogar.AplicationModels
{
    public class ReportDTO
    {
        public int RepId { get; set; }
        public string? RepDescripcion { get; set; }
        public int? ErId { get; set; }
        public int? ServId { get; set; }
        public int? UsuId { get; set; }
        public EstadoreportDTO? Er { get; set; }
        public ServicioDTO? Serv { get; set; }
        public UsuarioDTO? Usu { get; set; }
    }
}
