using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Data.Models
{
    public partial class POCContext : DbContext
    {
        public string _ConnectionString { get; set; }

        public POCContext() : base() { }
        public POCContext(string connectionString) : base() { _ConnectionString = connectionString; }
        public POCContext(DbContextOptions<POCContext> options) : base(options) { }

        public virtual DbSet<Escritos> Escritos { get; set; }
        public virtual DbSet<EscritosTexto> EscritosTexto { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Escritos>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.FechaCreacion)
                    .HasColumnName("fechaCreacion")
                    .HasColumnType("datetime");

                entity.Property(e => e.IdEstadoEscrito).HasColumnName("idEstadoEscrito");

                entity.Property(e => e.IdJuicio).HasColumnName("idJuicio");

                entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");

                entity.Property(e => e.Texto)
                    .IsRequired()
                    .HasColumnName("texto")
                    .HasColumnType("text");

                entity.Property(e => e.Titulo)
                    .IsRequired()
                    .HasColumnName("titulo")
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.ValidFrom).HasColumnType("datetime2(2)");

                entity.Property(e => e.ValidTo).HasColumnType("datetime2(2)");
            });

            modelBuilder.Entity<EscritosTexto>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Fecha)
                    .HasColumnName("fecha")
                    .HasColumnType("datetime");

                entity.Property(e => e.Last).HasColumnName("last");

                entity.Property(e => e.Texto)
                    .IsRequired()
                    .HasColumnName("texto")
                    .HasColumnType("text");

                entity.Property(e => e.Titulo)
                    .IsRequired()
                    .HasColumnName("titulo")
                    .HasMaxLength(1000)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
