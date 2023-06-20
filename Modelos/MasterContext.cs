using System;
using System.Collections.Generic;
using BackEndAPI.Modelos;
using Microsoft.EntityFrameworkCore;

namespace BackEndAPI.Modelos;

public partial class MasterContext : DbContext
{
    public MasterContext()
    {
    }

    public MasterContext(DbContextOptions<MasterContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Empleado> Empleados { get; set; }

    public virtual DbSet<Persona> Personas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Empleado>(entity =>
        {
            entity.HasKey(e => e.EmpleadoId).HasName("PK__Empleado__958BE6F0CE4BB8D7");

            entity.ToTable("Empleado");

            entity.Property(e => e.EmpleadoId).HasColumnName("EmpleadoID");
            entity.Property(e => e.Cargo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FechaContrato).HasColumnType("datetime");
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.PersonaId).HasColumnName("PersonaID");

            entity.HasOne(d => d.Persona).WithMany(p => p.Empleados)
                .HasForeignKey(d => d.PersonaId)
                .HasConstraintName("FK__Empleado__Person__3118447E");
        });

        modelBuilder.Entity<Persona>(entity =>
        {
            entity.HasKey(e => e.PersonaId).HasName("PK__Persona__7C34D323AD054E97");

            entity.ToTable("Persona");

            entity.Property(e => e.PersonaId).HasColumnName("PersonaID");
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
