using DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services
{
    public enum Context
    {
        POCDbContext = 0,
        InMemoryDbContex = 1
    }

    public class ConcreteFactory : IAbstractFactory
    {
        public IConfiguration _Configuration;
        public ConcreteFactory(IConfiguration configuration)
        {
            _Configuration = configuration;
        }

        public POCDbContext CreatePOCDbContext()
        {
            var GetConnectionString = _Configuration.GetConnectionString("SQLServer2");
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

        public DbContext Create(Context type)
        {
            switch (type)
            {
                case Context.InMemoryDbContex:
                    {
                        return CreateInMemoryDbContext();
                    }
                case Context.POCDbContext:
                    {
                        return CreatePOCDbContext();
                    }
            }
            return null;
        }
    }
}
