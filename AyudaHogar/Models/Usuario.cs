using System;
using System.Collections.Generic;

namespace AyudaHogar.Models;

public partial class Usuario
{
    public int UsuId { get; set; }

    public string? AzureAduserId { get; set; }

    public string? UsuNom { get; set; }

    public string? UsuApe { get; set; }

    public string? UsuMail { get; set; }

    public string? UsuFotoPerfilUrl { get; set; }

    public string? UsuPhone { get; set; }

    public string? UsuRol { get; set; }

    public virtual ICollection<Comentario> Comentarios { get; set; } = new List<Comentario>();

    public virtual ICollection<Proveedordeservicio> Proveedordeservicios { get; set; } = new List<Proveedordeservicio>();

    public virtual ICollection<Report> Reports { get; set; } = new List<Report>();

    public virtual ICollection<Solicitudnuevacategorium> Solicitudnuevacategoria { get; set; } = new List<Solicitudnuevacategorium>();

    public virtual ICollection<Solicitudproveedordeservicio> Solicitudproveedordeservicios { get; set; } = new List<Solicitudproveedordeservicio>();
}
