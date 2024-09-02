using System;
using System.Collections.Generic;

namespace AyudaHogar.Models;

public partial class Report
{
    public int RepId { get; set; }

    public string? RepDescripcion { get; set; }

    public int? ErId { get; set; }

    public int? ServId { get; set; }

    public int? UsuId { get; set; }

    public virtual Estadoreport? Er { get; set; }

    public virtual Servicio? Serv { get; set; }

    public virtual Usuario? Usu { get; set; }
}
