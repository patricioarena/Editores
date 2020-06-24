using Application.IFactory;
using ApplicationTests;
using AutoFixture;
using AutoMapper;
using DataAccess;
using Dominio.DTOs;
using EntityFrameworkCore.Testing.Moq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

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
            ILogger<Object> logger = Mock.Of<ILogger<Object>>();

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
            ServiceEscritosTexto service = new ServiceEscritosTexto(_mockContex, _mockServiceFactory);

            var testEntity = new EscritosTextoDto() { Titulo = "Por esparta !!", Texto = "Moq!!!!" };
            var result = service.SetEscritoTexto(testEntity);

            Assert.AreEqual(1, result);
        }

        [Test]
        public void GetAllEscritosTextosTest()
        {
            ServiceEscritosTexto service = new ServiceEscritosTexto(_mockContex, _mockServiceFactory);

            List<EscritosTextoDto> list = new Fixture().CreateMany<EscritosTextoDto>().ToList();
            list.ForEach(e => service.SetEscritoTexto(e));
            var result = service.GetAllEscritosTextos();

            Assert.Multiple(() =>
            {
                Assert.NotNull(result);
                Assert.Greater(result.Count(), 0);
            });

        }

        [Test]
        public void GetEscritosTextoByIdTest()
        {
            ServiceEscritosTexto service = new ServiceEscritosTexto(_mockContex, _mockServiceFactory);

            List<EscritosTextoDto> list = new Fixture().CreateMany<EscritosTextoDto>().ToList();
            list.ForEach(e => service.SetEscritoTexto(e));

            var result = service.GetAllEscritosTextos();
            var aEscrito = service.GetEscritosTextoById(result[2].Id);

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
            Assert.Ignore(Message.sp_NotAscertainable);
        }
    }
}

