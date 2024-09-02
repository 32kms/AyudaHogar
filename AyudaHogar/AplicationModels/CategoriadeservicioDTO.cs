namespace AyudaHogar.AplicationModels
{
    public class CategoriadeservicioDTO
    {
        public int CatId { get; set; }
        public string? CatNom { get; set; }
        public string? CatDescripcion { get; set; }
        public List<ServicioDTO> Servicios { get; set; }
    }
}
