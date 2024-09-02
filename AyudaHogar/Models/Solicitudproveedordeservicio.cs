using System;
using System.Collections.Generic;

namespace AyudaHogar.Models;

public partial class Solicitudproveedordeservicio
{
    public int SpsId { get; set; }

    public string? SpsDescripcion { get; set; }

    public int? UsuId { get; set; }

    public int? EspId { get; set; }

    public virtual Estadosolicitudproveedor? Esp { get; set; }

    public virtual Usuario? Usu { get; set; }
}
