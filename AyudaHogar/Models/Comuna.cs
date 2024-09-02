using System;
using System.Collections.Generic;

namespace AyudaHogar.Models;

public partial class Comuna
{
    public int ComunaId { get; set; }

    public string? ComunaNombre { get; set; }

    public int? RegionId { get; set; }

    public virtual Region? Region { get; set; }

    public virtual ICollection<Servicio> Servicios { get; set; } = new List<Servicio>();
}
