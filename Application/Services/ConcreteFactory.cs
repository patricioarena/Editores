﻿using DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
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

        private Context _Type;
        public ConcreteFactory(IConfiguration configuration)
        {
            _Configuration = configuration;
            _Type = (Context)Enum.Parse(typeof(Context), configuration["Context"]); ;
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

        public DbContext Create()
        {
            switch (_Type)
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
