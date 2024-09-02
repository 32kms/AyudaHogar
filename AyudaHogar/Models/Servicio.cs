using System;
using System.Collections.Generic;

namespace AyudaHogar.Models;

public partial class Servicio
{
    public int ServId { get; set; }

    public string? ServDetalle { get; set; }

    public int? CatId { get; set; }

    public int? ComunaId { get; set; }

    public int? ProvId { get; set; }

    public virtual ICollection<Bloquetiempo> Bloquetiempos { get; set; } = new List<Bloquetiempo>();

    public virtual Categoriadeservicio? Cat { get; set; }

    public virtual ICollection<Comentario> Comentarios { get; set; } = new List<Comentario>();

    public virtual Comuna? Comuna { get; set; }

    public virtual Proveedordeservicio? Prov { get; set; }

    public virtual ICollection<Report> Reports { get; set; } = new List<Report>();
}
