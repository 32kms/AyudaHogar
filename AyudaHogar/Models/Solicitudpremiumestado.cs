using System;
using System.Collections.Generic;

namespace AyudaHogar.Models;

public partial class Solicitudpremiumestado
{
    public int SpeId { get; set; }

    public string? SpeDescripcion { get; set; }

    public virtual ICollection<Solicitudpremium> Solicitudpremia { get; set; } = new List<Solicitudpremium>();
}
