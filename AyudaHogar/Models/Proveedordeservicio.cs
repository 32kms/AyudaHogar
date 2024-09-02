using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;

namespace AyudaHogar.Models;

public partial class Proveedordeservicio
{
    public int ProvId { get; set; }

    public string? ProvNom { get; set; }

    public string? ProvPhone { get; set; }

    public string? ProvFotoPerfilUrl { get; set; }

    public string? ProvRut { get; set; }

    public string? ProvFotoCarnet { get; set; }

    public int UsuId { get; set; }

    public virtual ICollection<Proveedorbloqueado> Proveedorbloqueados { get; set; } = new List<Proveedorbloqueado>();

    public virtual ICollection<Pspremium> Pspremia { get; set; } = new List<Pspremium>();

    public virtual ICollection<Servicio> Servicios { get; set; } = new List<Servicio>();

    public virtual ICollection<Solicitudpremium> Solicitudpremia { get; set; } = new List<Solicitudpremium>();

    [ValidateNever]
    public virtual Usuario Usu { get; set; } = null!;
}
