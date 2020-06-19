using Dominio.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public partial class POCDbContext : AbstractDbContext
    {
        public POCDbContext() : base() { }
        public POCDbContext(DbContextOptions<DbContext> options) : base(options) { }

        public virtual DbSet<Escritos> Escritos { get; set; }
        public virtual DbSet<EscritosTexto> EscritosTexto { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //optionsBuilder.UseSqlServer("Data Source=DESKTOP-ON7GE0B\\SQLEXPRESS;Initial Catalog=POC;Integrated Security=True;App=EntityFramework;");
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

        public override void Initialize()
        {
            throw new System.NotImplementedException();
        }

    }
}
