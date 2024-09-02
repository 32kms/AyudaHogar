using System;
using System.Collections.Generic;

namespace AyudaHogar.Models;

public partial class Categoriadeservicio
{
    public int CatId { get; set; }

    public string? CatNom { get; set; }

    public string? CatDescripcion { get; set; }

    public virtual ICollection<Servicio> Servicios { get; set; } = new List<Servicio>();
}
