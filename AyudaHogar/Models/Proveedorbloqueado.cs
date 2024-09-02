using System;
using System.Collections.Generic;

namespace AyudaHogar.Models;

public partial class Proveedorbloqueado
{
    public int PbId { get; set; }

    public int? ProvId { get; set; }

    public string? PbDescripcion { get; set; }

    public virtual Proveedordeservicio? Prov { get; set; }
}
