using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MyApi.Data;

public partial class MydbContext : DbContext
{
    public MydbContext()
    {
    }

    public MydbContext(DbContextOptions<MydbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Kuva> Kuvas { get; set; }

    public virtual DbSet<Matka> Matkas { get; set; }

    public virtual DbSet<Matkaaja> Matkaajas { get; set; }

    public virtual DbSet<Matkakohde> Matkakohdes { get; set; }

    public virtual DbSet<Tarina> Tarinas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlite("Datasource=mydb.db");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Kuva>(entity =>
        {
            entity.HasKey(e => e.Idkuva);

            entity.ToTable("kuva");

            entity.HasIndex(e => e.Idtarina, "fk_kuva_tarina1_idx");

            entity.Property(e => e.Idkuva).HasColumnName("idkuva");
            entity.Property(e => e.Idtarina).HasColumnName("idtarina");
            entity.Property(e => e.Kuva1).HasColumnName("kuva");

            entity.HasOne(d => d.IdtarinaNavigation).WithMany(p => p.Kuvas)
                .HasForeignKey(d => d.Idtarina)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Matka>(entity =>
        {
            entity.HasKey(e => e.Idmatka);

            entity.ToTable("matka");

            entity.HasIndex(e => e.Idmatkaaja, "fk_matkaaja_has_matkakohde_matkaaja_idx");

            entity.Property(e => e.Idmatka).HasColumnName("idmatka");
            entity.Property(e => e.Alkupvm)
                .HasColumnType("DATETIME")
                .HasColumnName("alkupvm");
            entity.Property(e => e.Idmatkaaja).HasColumnName("idmatkaaja");
            entity.Property(e => e.Loppupvm)
                .HasColumnType("DATETIME")
                .HasColumnName("loppupvm");
            entity.Property(e => e.Yksityinen).HasColumnName("yksityinen");

            entity.HasOne(d => d.IdmatkaajaNavigation).WithMany(p => p.Matkas)
                .HasForeignKey(d => d.Idmatkaaja)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Matkaaja>(entity =>
        {
            entity.HasKey(e => e.Idmatkaaja);

            entity.ToTable("matkaaja");

            entity.Property(e => e.Idmatkaaja).HasColumnName("idmatkaaja");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.Esittely).HasColumnName("esittely");
            entity.Property(e => e.Etunimi).HasColumnName("etunimi");
            entity.Property(e => e.Kuva).HasColumnName("kuva");
            entity.Property(e => e.Nimimerkki).HasColumnName("nimimerkki");
            entity.Property(e => e.Paikkakunta).HasColumnName("paikkakunta");
            entity.Property(e => e.Password).HasColumnName("password");
            entity.Property(e => e.Sukunimi).HasColumnName("sukunimi");
        });

        modelBuilder.Entity<Matkakohde>(entity =>
        {
            entity.HasKey(e => e.Idmatkakohde);

            entity.ToTable("matkakohde");

            entity.Property(e => e.Idmatkakohde).HasColumnName("idmatkakohde");
            entity.Property(e => e.Kohdenimi).HasColumnName("kohdenimi");
            entity.Property(e => e.Kuva).HasColumnName("kuva");
            entity.Property(e => e.Kuvausteksti).HasColumnName("kuvausteksti");
            entity.Property(e => e.Maa).HasColumnName("maa");
            entity.Property(e => e.Paikkakunta).HasColumnName("paikkakunta");
        });

        modelBuilder.Entity<Tarina>(entity =>
        {
            entity.HasKey(e => e.Idtarina);

            entity.ToTable("tarina");

            entity.Property(e => e.Idtarina).HasColumnName("idtarina");
            entity.Property(e => e.Idmatka).HasColumnName("idmatka");
            entity.Property(e => e.Idmatkakohde).HasColumnName("idmatkakohde");
            entity.Property(e => e.Pvm)
                .HasColumnType("DATETIME")
                .HasColumnName("pvm");
            entity.Property(e => e.Teksti).HasColumnName("teksti");

            entity.HasOne(d => d.IdmatkaNavigation).WithMany(p => p.Tarinas)
                .HasForeignKey(d => d.Idmatka)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.IdmatkakohdeNavigation).WithMany(p => p.Tarinas)
                .HasForeignKey(d => d.Idmatkakohde)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
