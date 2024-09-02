using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace AyudaHogar.Models;

public partial class AyudahogardbContext : DbContext
{
    public AyudahogardbContext()
    {
    }

    public AyudahogardbContext(DbContextOptions<AyudahogardbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Bloquetiempo> Bloquetiempos { get; set; }

    public virtual DbSet<Categoriadeservicio> Categoriadeservicios { get; set; }

    public virtual DbSet<Comentario> Comentarios { get; set; }

    public virtual DbSet<Comuna> Comunas { get; set; }

    public virtual DbSet<Estadoreport> Estadoreports { get; set; }

    public virtual DbSet<Estadoserviciopremium> Estadoserviciopremia { get; set; }

    public virtual DbSet<Estadosolicitudproveedor> Estadosolicitudproveedors { get; set; }

    public virtual DbSet<PlanesPremium> PlanesPremia { get; set; }

    public virtual DbSet<Proveedorbloqueado> Proveedorbloqueados { get; set; }

    public virtual DbSet<Proveedordeservicio> Proveedordeservicios { get; set; }

    public virtual DbSet<Pspremium> Pspremia { get; set; }

    public virtual DbSet<Region> Regions { get; set; }

    public virtual DbSet<Report> Reports { get; set; }

    public virtual DbSet<Servicio> Servicios { get; set; }

    public virtual DbSet<Solicitudnuevacategorium> Solicitudnuevacategoria { get; set; }

    public virtual DbSet<Solicitudpremium> Solicitudpremia { get; set; }

    public virtual DbSet<Solicitudpremiumestado> Solicitudpremiumestados { get; set; }

    public virtual DbSet<Solicitudproveedordeservicio> Solicitudproveedordeservicios { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=tcp:ayudahogarsdb.database.windows.net,1433;Initial Catalog=ayudahogardb;Persist Security Info=False;User ID=ayudahogar;Password=94897563An;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Bloquetiempo>(entity =>
        {
            entity.HasKey(e => e.BtId).HasName("PK_bloquetiempo_bt_id");

            entity.ToTable("bloquetiempo", "ayuda_hogar");

            entity.HasIndex(e => e.ServId, "bt_servicio_idx");

            entity.Property(e => e.BtId).HasColumnName("bt_id");
            entity.Property(e => e.BtDia)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("bt_dia");
            entity.Property(e => e.BtHoraFin)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("bt_hora_fin");
            entity.Property(e => e.BtHoraInicio)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("bt_hora_inicio");
            entity.Property(e => e.ServId)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("serv_id");

            entity.HasOne(d => d.Serv).WithMany(p => p.Bloquetiempos)
                .HasForeignKey(d => d.ServId)
                .HasConstraintName("bloquetiempo$bt_servicio");
        });

        modelBuilder.Entity<Categoriadeservicio>(entity =>
        {
            entity.HasKey(e => e.CatId).HasName("PK_categoriadeservicio_cat_id");

            entity.ToTable("categoriadeservicio", "ayuda_hogar");

            entity.Property(e => e.CatId).HasColumnName("cat_id");
            entity.Property(e => e.CatDescripcion)
                .HasMaxLength(45)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("cat_descripcion");
            entity.Property(e => e.CatNom)
                .HasMaxLength(45)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("cat_nom");
        });

        modelBuilder.Entity<Comentario>(entity =>
        {
            entity.HasKey(e => e.ComId).HasName("PK_comentarios_com_id");

            entity.ToTable("comentarios", "ayuda_hogar");

            entity.HasIndex(e => e.ServId, "com_serv_idx");

            entity.HasIndex(e => e.UsuId, "com_usu_idx");

            entity.Property(e => e.ComId).HasColumnName("com_id");
            entity.Property(e => e.ComDetalle)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("com_detalle");
            entity.Property(e => e.ComFotoUrl)
                .HasMaxLength(200)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("com_foto_url");
            entity.Property(e => e.ComPuntuacion)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("com_puntuacion");
            entity.Property(e => e.ServId)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("serv_id");
            entity.Property(e => e.UsuId)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("usu_id");

            entity.HasOne(d => d.Serv).WithMany(p => p.Comentarios)
                .HasForeignKey(d => d.ServId)
                .HasConstraintName("comentarios$com_serv");

            entity.HasOne(d => d.Usu).WithMany(p => p.Comentarios)
                .HasForeignKey(d => d.UsuId)
                .HasConstraintName("comentarios$com_usu");
        });

        modelBuilder.Entity<Comuna>(entity =>
        {
            entity.HasKey(e => e.ComunaId).HasName("PK_comuna_comuna_id");

            entity.ToTable("comuna", "ayuda_hogar");

            entity.HasIndex(e => e.RegionId, "com_prov_idx");

            entity.Property(e => e.ComunaId).HasColumnName("comuna_id");
            entity.Property(e => e.ComunaNombre)
                .HasMaxLength(45)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("comuna_nombre");
            entity.Property(e => e.RegionId)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("region_id");

            entity.HasOne(d => d.Region).WithMany(p => p.Comunas)
                .HasForeignKey(d => d.RegionId)
                .HasConstraintName("comuna$com_reg");
        });

        modelBuilder.Entity<Estadoreport>(entity =>
        {
            entity.HasKey(e => e.ErId).HasName("PK_estadoreport_er_id");

            entity.ToTable("estadoreport", "ayuda_hogar");

            entity.Property(e => e.ErId).HasColumnName("er_id");
            entity.Property(e => e.ErNombre)
                .HasMaxLength(45)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("er_nombre");
        });

        modelBuilder.Entity<Estadoserviciopremium>(entity =>
        {
            entity.HasKey(e => e.EpspId).HasName("PK_estadoserviciopremium_epsp_id");

            entity.ToTable("estadoserviciopremium", "ayuda_hogar");

            entity.Property(e => e.EpspId).HasColumnName("epsp_id");
            entity.Property(e => e.EpspDescripcion)
                .HasMaxLength(45)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("epsp_descripcion");
            entity.Property(e => e.EpspNom)
                .HasMaxLength(45)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("epsp_nom");
        });

        modelBuilder.Entity<Estadosolicitudproveedor>(entity =>
        {
            entity.HasKey(e => e.EspId);

            entity.ToTable("estadosolicitudproveedor", "ayuda_hogar");

            entity.Property(e => e.EspId).HasColumnName("esp_id");
            entity.Property(e => e.EspDescripcion)
                .IsUnicode(false)
                .HasColumnName("esp_descripcion");
        });

        modelBuilder.Entity<PlanesPremium>(entity =>
        {
            entity.HasKey(e => e.PpId);

            entity.ToTable("PlanesPremium", "ayuda_hogar");

            entity.Property(e => e.PpId).HasColumnName("pp_id");
            entity.Property(e => e.PpDescripcion).HasColumnName("pp_descripcion");
            entity.Property(e => e.PpDuracion).HasColumnName("pp_duracion");
            entity.Property(e => e.PpPrecio).HasColumnName("pp_precio");
        });

        modelBuilder.Entity<Proveedorbloqueado>(entity =>
        {
            entity.HasKey(e => e.PbId).HasName("PK_proveedorbloqueado_pb_id");

            entity.ToTable("proveedorbloqueado", "ayuda_hogar");

            entity.HasIndex(e => e.ProvId, "pb_prov_idx");

            entity.Property(e => e.PbId).HasColumnName("pb_id");
            entity.Property(e => e.PbDescripcion)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("pb_descripcion");
            entity.Property(e => e.ProvId)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("prov_id");

            entity.HasOne(d => d.Prov).WithMany(p => p.Proveedorbloqueados)
                .HasForeignKey(d => d.ProvId)
                .HasConstraintName("proveedorbloqueado$pb_prov");
        });

        modelBuilder.Entity<Proveedordeservicio>(entity =>
        {
            entity.HasKey(e => e.ProvId).HasName("PK_proveedordeservicios_prov_id");

            entity.ToTable("proveedordeservicios", "ayuda_hogar");

            entity.Property(e => e.ProvId).HasColumnName("prov_id");
            entity.Property(e => e.ProvFotoCarnet)
                .HasMaxLength(200)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("prov_foto_carnet");
            entity.Property(e => e.ProvFotoPerfilUrl)
                .HasMaxLength(200)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("prov_foto_perfil_url");
            entity.Property(e => e.ProvNom)
                .HasMaxLength(45)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("prov_nom");
            entity.Property(e => e.ProvPhone)
                .HasMaxLength(45)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("prov_phone");
            entity.Property(e => e.ProvRut)
                .HasMaxLength(45)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("prov_rut");
            entity.Property(e => e.UsuId).HasColumnName("usu_id");

            entity.HasOne(d => d.Usu).WithMany(p => p.Proveedordeservicios)
                .HasForeignKey(d => d.UsuId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_proveedordeservicios_usuario");
        });

        modelBuilder.Entity<Pspremium>(entity =>
        {
            entity.HasKey(e => e.PspId).HasName("PK_pspremium_psp_id");

            entity.ToTable("pspremium", "ayuda_hogar");

            entity.HasIndex(e => e.EpspId, "psp_epsp_idx");

            entity.HasIndex(e => e.ProvId, "psp_prov_idx");

            entity.Property(e => e.PspId).HasColumnName("psp_id");
            entity.Property(e => e.EpspId)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("epsp_id");
            entity.Property(e => e.ProvId)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("prov_id");
            entity.Property(e => e.PspDescripcion)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("psp_descripcion");
            entity.Property(e => e.PspDuracion)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("psp_duracion");
            entity.Property(e => e.PspFechaInicio)
                .HasPrecision(0)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("psp_fecha_inicio");
            entity.Property(e => e.PspNom)
                .HasMaxLength(45)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("psp_nom");

            entity.HasOne(d => d.Epsp).WithMany(p => p.Pspremia)
                .HasForeignKey(d => d.EpspId)
                .HasConstraintName("pspremium$psp_epsp");

            entity.HasOne(d => d.Prov).WithMany(p => p.Pspremia)
                .HasForeignKey(d => d.ProvId)
                .HasConstraintName("pspremium$psp_prov");
        });

        modelBuilder.Entity<Region>(entity =>
        {
            entity.HasKey(e => e.RegId).HasName("PK_region_reg_id");

            entity.ToTable("region", "ayuda_hogar");

            entity.Property(e => e.RegId).HasColumnName("reg_id");
            entity.Property(e => e.RegNombre)
                .HasMaxLength(100)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("reg_nombre");
        });

        modelBuilder.Entity<Report>(entity =>
        {
            entity.HasKey(e => e.RepId).HasName("PK_report_rep_id");

            entity.ToTable("report", "ayuda_hogar");

            entity.HasIndex(e => e.ErId, "rep_er_idx");

            entity.HasIndex(e => e.ServId, "rep_serv_idx");

            entity.HasIndex(e => e.UsuId, "rep_usu_idx");

            entity.Property(e => e.RepId).HasColumnName("rep_id");
            entity.Property(e => e.ErId)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("er_id");
            entity.Property(e => e.RepDescripcion)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("rep_descripcion");
            entity.Property(e => e.ServId)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("serv_id");
            entity.Property(e => e.UsuId)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("usu_id");

            entity.HasOne(d => d.Er).WithMany(p => p.Reports)
                .HasForeignKey(d => d.ErId)
                .HasConstraintName("report$rep_er");

            entity.HasOne(d => d.Serv).WithMany(p => p.Reports)
                .HasForeignKey(d => d.ServId)
                .HasConstraintName("report$rep_serv");

            entity.HasOne(d => d.Usu).WithMany(p => p.Reports)
                .HasForeignKey(d => d.UsuId)
                .HasConstraintName("report$rep_usu");
        });

        modelBuilder.Entity<Servicio>(entity =>
        {
            entity.HasKey(e => e.ServId).HasName("PK_servicio_serv_id");

            entity.ToTable("servicio", "ayuda_hogar");

            entity.HasIndex(e => e.CatId, "serv_cat_idx");

            entity.HasIndex(e => e.ComunaId, "serv_com_idx");

            entity.Property(e => e.ServId).HasColumnName("serv_id");
            entity.Property(e => e.CatId)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("cat_id");
            entity.Property(e => e.ComunaId)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("comuna_id");
            entity.Property(e => e.ProvId).HasColumnName("prov_id");
            entity.Property(e => e.ServDetalle)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("serv_detalle");

            entity.HasOne(d => d.Cat).WithMany(p => p.Servicios)
                .HasForeignKey(d => d.CatId)
                .HasConstraintName("servicio$serv_cat");

            entity.HasOne(d => d.Comuna).WithMany(p => p.Servicios)
                .HasForeignKey(d => d.ComunaId)
                .HasConstraintName("servicio$serv_com");

            entity.HasOne(d => d.Prov).WithMany(p => p.Servicios)
                .HasForeignKey(d => d.ProvId)
                .HasConstraintName("servicio$serv_prov");
        });

        modelBuilder.Entity<Solicitudnuevacategorium>(entity =>
        {
            entity.HasKey(e => e.SncId).HasName("PK_ayuda_hogar.solicitudnuevacategoria");

            entity.ToTable("solicitudnuevacategoria", "ayuda_hogar");

            entity.Property(e => e.SncId).HasColumnName("snc_id");
            entity.Property(e => e.SncDescripcion)
                .IsUnicode(false)
                .HasColumnName("snc_descripcion");
            entity.Property(e => e.UsuId).HasColumnName("usu_id");

            entity.HasOne(d => d.Usu).WithMany(p => p.Solicitudnuevacategoria)
                .HasForeignKey(d => d.UsuId)
                .HasConstraintName("FK_solicitudnuevacategoria_usuario");
        });

        modelBuilder.Entity<Solicitudpremium>(entity =>
        {
            entity.HasKey(e => e.SpId).HasName("PK_solicitudpremium_sp_id");

            entity.ToTable("solicitudpremium", "ayuda_hogar");

            entity.HasIndex(e => e.ProvId, "sol_prov_idx");

            entity.HasIndex(e => e.SpeId, "sol_spe_idx");

            entity.Property(e => e.SpId).HasColumnName("sp_id");
            entity.Property(e => e.PpId).HasColumnName("pp_id");
            entity.Property(e => e.ProvId)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("prov_id");
            entity.Property(e => e.SpDescripcion)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("sp_descripcion");
            entity.Property(e => e.SpeId)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("spe_id");

            entity.HasOne(d => d.Pp).WithMany(p => p.Solicitudpremia)
                .HasForeignKey(d => d.PpId)
                .HasConstraintName("FK_solicitudpremium_PlanesPremium");

            entity.HasOne(d => d.Prov).WithMany(p => p.Solicitudpremia)
                .HasForeignKey(d => d.ProvId)
                .HasConstraintName("solicitudpremium$sol_prov");

            entity.HasOne(d => d.Spe).WithMany(p => p.Solicitudpremia)
                .HasForeignKey(d => d.SpeId)
                .HasConstraintName("solicitudpremium$sol_spe");
        });

        modelBuilder.Entity<Solicitudpremiumestado>(entity =>
        {
            entity.HasKey(e => e.SpeId).HasName("PK_solicitudpremiumestado_spe_id");

            entity.ToTable("solicitudpremiumestado", "ayuda_hogar");

            entity.Property(e => e.SpeId).HasColumnName("spe_id");
            entity.Property(e => e.SpeDescripcion)
                .HasMaxLength(45)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("spe_descripcion");
        });

        modelBuilder.Entity<Solicitudproveedordeservicio>(entity =>
        {
            entity.HasKey(e => e.SpsId);

            entity.ToTable("solicitudproveedordeservicio", "ayuda_hogar");

            entity.Property(e => e.SpsId).HasColumnName("sps_id");
            entity.Property(e => e.EspId).HasColumnName("esp_id");
            entity.Property(e => e.SpsDescripcion)
                .IsUnicode(false)
                .HasColumnName("sps_descripcion");
            entity.Property(e => e.UsuId).HasColumnName("usu_id");

            entity.HasOne(d => d.Esp).WithMany(p => p.Solicitudproveedordeservicios)
                .HasForeignKey(d => d.EspId)
                .HasConstraintName("FK_solicitudproveedordeservicio_estadosolicitudproveedor");

            entity.HasOne(d => d.Usu).WithMany(p => p.Solicitudproveedordeservicios)
                .HasForeignKey(d => d.UsuId)
                .HasConstraintName("FK_solicitudproveedordeservicio_usuario");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.UsuId).HasName("PK_usuario_usu_id");

            entity.ToTable("usuario", "ayuda_hogar");

            entity.Property(e => e.UsuId).HasColumnName("usu_id");
            entity.Property(e => e.AzureAduserId)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("azureADUserID");
            entity.Property(e => e.UsuApe)
                .HasMaxLength(45)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("usu_ape");
            entity.Property(e => e.UsuFotoPerfilUrl)
                .HasMaxLength(45)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("usu_foto_perfil_url");
            entity.Property(e => e.UsuMail)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("usu_mail");
            entity.Property(e => e.UsuNom)
                .HasMaxLength(45)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("usu_nom");
            entity.Property(e => e.UsuPhone)
                .HasMaxLength(200)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("usu_phone");
            entity.Property(e => e.UsuRol)
                .HasMaxLength(45)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("usu_rol");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
