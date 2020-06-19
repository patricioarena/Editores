using Application.IFactory;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.Factory
{
    public enum Context
    {
        POCDbContext = 0,
        InMemoryDbContex = 1
    }

    public class ConcreteContextFactory : IAbstractContextFactory
    {
        public IConfiguration _Configuration;

        private Context _Type;
        private static DbContext _Instance;

        public ConcreteContextFactory(IConfiguration configuration)
        {
            _Configuration = configuration;
            _Type = (Context)Enum.Parse(typeof(Context), configuration["Context"]); ;
        }

        public POCDbContext CreatePOCDbContext()
        {
            var GetConnectionString = _Configuration.GetConnectionString("SQLServer");
            var dbContextOptionsBuilder = new DbContextOptionsBuilder<DbContext>();
            var optionsBuilder = dbContextOptionsBuilder.UseSqlServer(GetConnectionString);
            POCDbContext context = new POCDbContext(optionsBuilder.Options);
            return context;
        }

        public InMermoryDbContext CreateInMemoryDbContext()
        {
            var dbContextOptionsBuilder = new DbContextOptionsBuilder<DbContext>();
            var optionsBuilder = dbContextOptionsBuilder.UseInMemoryDatabase(databaseName: "InMermoryDBContext");
            InMermoryDbContext context = new InMermoryDbContext(optionsBuilder.Options);
            context.Initialize();
            return context;
        }

        public DbContext CreateContext()
        {
            switch (_Type)
            {
                case Context.InMemoryDbContex:
                    {
                        if (_Instance == null)
                        {
                            _Instance = CreateInMemoryDbContext();
                        }
                        return _Instance;
                    }
                case Context.POCDbContext:
                    {
                        if (_Instance == null)
                        {
                            _Instance = CreatePOCDbContext();
                        }
                        return _Instance;
                    }
            }
            return null;
        }

    }
}
