using System;
using System.Collections.Generic;

namespace AyudaHogar.Models;

public partial class Region
{
    public int RegId { get; set; }

    public string? RegNombre { get; set; }

    public virtual ICollection<Comuna> Comunas { get; set; } = new List<Comuna>();
}
