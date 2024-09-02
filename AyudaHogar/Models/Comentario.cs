using System;
using System.Collections.Generic;

namespace AyudaHogar.Models;

public partial class Comentario
{
    public int ComId { get; set; }

    public string? ComDetalle { get; set; }

    public int? ComPuntuacion { get; set; }

    public string? ComFotoUrl { get; set; }

    public int? ServId { get; set; }

    public int? UsuId { get; set; }

    public virtual Servicio? Serv { get; set; }

    public virtual Usuario? Usu { get; set; }
}
