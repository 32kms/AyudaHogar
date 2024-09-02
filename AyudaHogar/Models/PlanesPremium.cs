using System;
using System.Collections.Generic;

namespace AyudaHogar.Models;

public partial class PlanesPremium
{
    public int PpId { get; set; }

    public string? PpDescripcion { get; set; }

    public int? PpDuracion { get; set; }

    public int? PpPrecio { get; set; }

    public virtual ICollection<Solicitudpremium> Solicitudpremia { get; set; } = new List<Solicitudpremium>();
}
