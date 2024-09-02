using System;
using System.Collections.Generic;

namespace AyudaHogar.Models;

public partial class Estadoserviciopremium
{
    public int EpspId { get; set; }

    public string? EpspNom { get; set; }

    public string? EpspDescripcion { get; set; }

    public virtual ICollection<Pspremium> Pspremia { get; set; } = new List<Pspremium>();
}
