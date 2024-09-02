using System;
using System.Collections.Generic;

namespace AyudaHogar.Models;

public partial class Solicitudpremium
{
    public int SpId { get; set; }

    public int? ProvId { get; set; }

    public string? SpDescripcion { get; set; }

    public int? SpeId { get; set; }

    public int? PpId { get; set; }

    public virtual PlanesPremium? Pp { get; set; }

    public virtual Proveedordeservicio? Prov { get; set; }

    public virtual Solicitudpremiumestado? Spe { get; set; }
}
