using DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Application.IFactory
{
    public interface IAbstractContextFactory
    {
        POCDbContext CreatePOCDbContext();
        InMermoryDbContext CreateInMemoryDbContext();
        DbContext CreateContext();
    }
}