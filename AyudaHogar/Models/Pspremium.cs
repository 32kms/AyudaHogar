using System;
using System.Collections.Generic;

namespace AyudaHogar.Models;

public partial class Pspremium
{
    public int PspId { get; set; }

    public string? PspNom { get; set; }

    public string? PspDescripcion { get; set; }

    public int? PspDuracion { get; set; }

    public DateTime? PspFechaInicio { get; set; }

    public int? ProvId { get; set; }

    public int? EpspId { get; set; }

    public virtual Estadoserviciopremium? Epsp { get; set; }

    public virtual Proveedordeservicio? Prov { get; set; }
}
