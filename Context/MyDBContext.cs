using System;
using System.Collections.Generic;
using CRUD_NET8.Models;
using Microsoft.EntityFrameworkCore;

namespace Context;

public partial class myDBContext : DbContext
{
    public myDBContext()
    {
    }

    public myDBContext(DbContextOptions<myDBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TipoUsuario> TipoUsuarios { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TipoUsuario>(entity =>
        {
            entity.HasKey(e => e.TusuId).HasName("PRIMARY");

            entity.ToTable("tipo_usuario");

            entity.Property(e => e.TusuId)
                .HasColumnType("int(11)")
                .HasColumnName("tusu_id");
            entity.Property(e => e.TemporalId)
                .HasColumnType("int(11)")
                .HasColumnName("temporal_id");
            entity.Property(e => e.TemporalText)
                .HasMaxLength(2000)
                .HasColumnName("temporal_text");
            entity.Property(e => e.TusuActivo)
                .HasDefaultValueSql("'1'")
                .HasColumnName("tusu_activo");
            entity.Property(e => e.TusuDesc)
                .HasMaxLength(100)
                .HasColumnName("tusu_desc");
            entity.Property(e => e.TusuFc)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("tusu_fc");
            entity.Property(e => e.TusuFm)
                .HasColumnType("datetime")
                .HasColumnName("tusu_fm");
            entity.Property(e => e.TusuObs)
                .HasColumnType("text")
                .HasColumnName("tusu_obs");
            entity.Property(e => e.TusuUc)
                .HasMaxLength(25)
                .HasColumnName("tusu_uc");
            entity.Property(e => e.TusuUm)
                .HasMaxLength(25)
                .HasColumnName("tusu_um");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.UsuId).HasName("PRIMARY");

            entity.ToTable("usuario");

            entity.HasIndex(e => e.TusuId, "tusu_id");

            entity.HasIndex(e => e.UsuLogin, "usu_login").IsUnique();

            entity.Property(e => e.UsuId)
                .HasColumnType("int(11)")
                .HasColumnName("usu_id");
            entity.Property(e => e.TemporalId)
                .HasColumnType("int(11)")
                .HasColumnName("temporal_id");
            entity.Property(e => e.TemporalText)
                .HasMaxLength(2000)
                .HasColumnName("temporal_text");
            entity.Property(e => e.TusuId)
                .HasComment("tabla tipo_usuario")
                .HasColumnType("int(11)")
                .HasColumnName("tusu_id");
            entity.Property(e => e.UsuActivo)
                .HasDefaultValueSql("'1'")
                .HasColumnName("usu_activo");
            entity.Property(e => e.UsuApellido)
                .HasMaxLength(100)
                .HasColumnName("usu_apellido");
            entity.Property(e => e.UsuBusqueda)
                .HasMaxLength(2000)
                .HasColumnName("usu_busqueda");
            entity.Property(e => e.UsuFc)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("usu_fc");
            entity.Property(e => e.UsuFm)
                .HasColumnType("datetime")
                .HasColumnName("usu_fm");
            entity.Property(e => e.UsuLogin)
                .HasMaxLength(25)
                .HasColumnName("usu_login");
            entity.Property(e => e.UsuNombre)
                .HasMaxLength(100)
                .HasColumnName("usu_nombre");
            entity.Property(e => e.UsuPwd)
                .HasMaxLength(250)
                .HasColumnName("usu_pwd");
            entity.Property(e => e.UsuUc)
                .HasMaxLength(25)
                .HasColumnName("usu_uc");
            entity.Property(e => e.UsuUm)
                .HasMaxLength(25)
                .HasColumnName("usu_um");

            entity.HasOne(d => d.Tusu).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.TusuId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("usuario_ibfk_1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
