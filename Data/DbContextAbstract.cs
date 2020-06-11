using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace DataAccess
{
    //Esta clase se debe usar para agreagar store procedure
    public abstract class DbContextAbstract : DbContext
    {
        protected DbContextAbstract() { }
        public DbContextAbstract(DbContextOptions<DbContext> options) : base(options) { }

        // DbContext hará que Entity Framework Core arroje una excepción cuando realiza una evaluación del lado del cliente
        // https://elanderson.net/2019/01/entity-framework-core-client-side-evaluation/
        [System.Obsolete]
#pragma warning disable CS0809 // El miembro obsoleto invalida un miembro no obsoleto
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#pragma warning restore CS0809 // El miembro obsoleto invalida un miembro no obsoleto
        {
            optionsBuilder
                .ConfigureWarnings(w => w.Throw(RelationalEventId.QueryClientEvaluationWarning));
        }
        public abstract void Initialize();

    }

    
}
