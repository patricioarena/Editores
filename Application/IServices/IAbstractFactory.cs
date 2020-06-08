using DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Application.Services
{
    public interface IAbstractFactory
    {
        POCDbContext CreatePOCDbContext();
        InMermoryDbContext CreateInMemoryDbContext();
        DbContext Create();
    }
}