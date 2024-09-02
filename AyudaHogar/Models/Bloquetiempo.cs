using System;
using System.Collections.Generic;

namespace AyudaHogar.Models;

public partial class Bloquetiempo
{
    public int BtId { get; set; }

    public int? BtDia { get; set; }

    public TimeOnly? BtHoraInicio { get; set; }

    public TimeOnly? BtHoraFin { get; set; }

    public int? ServId { get; set; }

    public virtual Servicio? Serv { get; set; }
}
