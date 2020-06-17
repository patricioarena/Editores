using NUnit.Framework;
using Application.Services;
using System;
using System.Collections.Generic;
using System.Text;
using DataAccess;
using EntityFrameworkCore.Testing.Moq;
using Dominio.Entities;
using Dominio.DTOs;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Moq;
using FluentAssertions;
using System.Data.SqlClient;
using EntityFrameworkCore.Testing.Moq.Extensions;
using AutoFixture;
using System.Threading.Tasks;
using Application.IFactory;
using Application.Factory;
using Autofac.Core;
using AutoMapper;
using Microsoft.Extensions.Logging;
using ApiBackend;

namespace Application.Services.Tests
{
    [TestFixture()]
    public class ServiceEscritosTextoTests
    {
        private IAbstractContextFactory _mockContex;
        private IAbstractServiceFactory _mockServiceFactory;

        [SetUp]
        public void Inicializar()
        {
            //Mapper real
            MapperConfiguration configuration = new MapperConfiguration(cfg => cfg.AddProfile(new AutoMapperProfileConfiguration()));
            IMapper mapper = new Mapper(configuration);

            //Mock DbContext
            POCDbContext mockedDbContext = Create.MockedDbContextFor<POCDbContext>(
                new DbContextOptionsBuilder<DbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options
            );

            //Mock Services
            ILogger<Startup> logger = Mock.Of<ILogger<Startup>>();

            Mock<IAbstractServiceFactory> mockServiceFactory = new Mock<IAbstractServiceFactory>();
            mockServiceFactory.Setup(a => a.Logger()).Returns(logger);
            mockServiceFactory.Setup(a => a.Mapper()).Returns(mapper);

            Mock<IAbstractContextFactory> mockContex = new Mock<IAbstractContextFactory>();
            mockContex.Setup(a => a.CreateContext()).Returns(mockedDbContext);

            _mockContex = mockContex.Object;
            _mockServiceFactory = mockServiceFactory.Object;

        }

        [Test()]
        public void SetEscritoTextoTest()
        {
            ServiceEscritosTexto services = new ServiceEscritosTexto(_mockContex, _mockServiceFactory);

            var testEntity = new EscritosTextoDto() { Titulo = "Por esparta !!", Texto = "Moq!!!!" };
            var result = services.SetEscritoTexto(testEntity);

            Assert.AreEqual(1, result);
        }

        [Test]
        public void GetAllEscritosTextosTest()
        {
            ServiceEscritosTexto services = new ServiceEscritosTexto(_mockContex, _mockServiceFactory);

            List<EscritosTextoDto> list = new Fixture().CreateMany<EscritosTextoDto>().ToList();
            list.ForEach(e => services.SetEscritoTexto(e));
            var result = services.GetAllEscritosTextos();

            Assert.Multiple(() =>
            {
                Assert.NotNull(result);
                Assert.Greater(result.Count(), 0);
            });

        }

        [Test]
        public void GetEscritosTextoByIdTest()
        {
            ServiceEscritosTexto services = new ServiceEscritosTexto(_mockContex, _mockServiceFactory);

            List<EscritosTextoDto> list = new Fixture().CreateMany<EscritosTextoDto>().ToList();
            list.ForEach(e => services.SetEscritoTexto(e));

            var result = services.GetAllEscritosTextos();
            var aEscrito = services.GetEscritosTextoById(result[2].Id);

            Assert.Multiple(() =>
            {
                Assert.NotNull(result);
                Assert.Greater(result.Count(), 0);

                Assert.NotNull(aEscrito);
                Assert.AreEqual(result[2], aEscrito);
            });
        }

        [Test()]
        public void GetUltimoEscritosTextoTest()
        {
            Assert.Ignore("Esto no se puede testear porque es un Store Procedure");
        }
    }
}

