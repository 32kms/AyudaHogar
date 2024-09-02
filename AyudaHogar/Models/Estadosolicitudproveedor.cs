using System;
using System.Collections.Generic;

namespace AyudaHogar.Models;

public partial class Estadosolicitudproveedor
{
    public int EspId { get; set; }

    public string? EspDescripcion { get; set; }

    public virtual ICollection<Solicitudproveedordeservicio> Solicitudproveedordeservicios { get; set; } = new List<Solicitudproveedordeservicio>();
}
