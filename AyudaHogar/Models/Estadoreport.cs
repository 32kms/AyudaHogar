using System;
using System.Collections.Generic;

namespace AyudaHogar.Models;

public partial class Estadoreport
{
    public int ErId { get; set; }

    public string? ErNombre { get; set; }

    public virtual ICollection<Report> Reports { get; set; } = new List<Report>();
}
