using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DataAccess
{
    public partial class InMermoryDbContext : DbContextAbstract
    {
        public InMermoryDbContext() : base() { }
        public InMermoryDbContext(DbContextOptions<DbContext> options) : base(options) { }

        public virtual DbSet<Escritos> Escritos { get; set; }
        public virtual DbSet<EscritosTexto> EscritosTexto { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseInMemoryDatabase(databaseName: "InMermoryDBContext");
            }
        }

        public override void Initialize()
        {
            using (var context = new InMermoryDbContext())
            {
                if (context.EscritosTexto.Any())
                {
                    return;   // Data was already seeded
                }
                else
                {
                    context.EscritosTexto.AddRange(
                        new EscritosTexto
                        {
                            Id = 1,
                            Titulo = "Texto en Memoria",
                            Texto = "Texto en Memoria"
                        },
                           new EscritosTexto
                           {
                               Id = 2,
                               Titulo = "Texto en Memoria",
                               Texto = "Texto en Memoria"
                           },
                        new EscritosTexto
                        {
                            Id = 3,
                            Titulo = "Texto en Memoria",
                            Texto = "Texto en Memoria"
                        },
                        new EscritosTexto
                        {
                            Id = 4,
                            Titulo = "Texto en Memoria",
                            Texto = "Texto en Memoria"
                        }
                    );
                    context.SaveChanges();
                }
            }
        }
    }
}