using System;
using System.Collections.Generic;

namespace AyudaHogar.Models;

public partial class Solicitudnuevacategorium
{
    public int SncId { get; set; }

    public string? SncDescripcion { get; set; }

    public int? UsuId { get; set; }

    public virtual Usuario? Usu { get; set; }
}
