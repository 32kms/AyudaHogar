namespace AyudaHogar.AplicationModels
{
    public class BloquetiempoDTO
    {
        public int BtId { get; set; }
        public DateOnly? BtDia { get; set; }
        public TimeOnly? BtHoraInicio { get; set; }
        public TimeOnly? BtHoraFin { get; set; }
        public int? ServId { get; set; }
        public ServicioDTO? Serv { get; set; }
    }
}
