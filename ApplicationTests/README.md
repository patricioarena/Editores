## Testing de Application

Se utilizo **NUnit** ya que era requisito para la utilizacion de la libreria **EntityFrameworkCore.Testing.Moq**

- **Nugets**:
    - **AutoFixture:** Permite crear colecciones de objetos apartir del modelo dado. [Más información](https://github.com/AutoFixture/AutoFixture)
    - **Moq:** Biblioteca de mocks EntityFrameworkCore [Más información](https://github.com/Moq/moq4/wiki/Quickstart)
    - **EntityFrameworkCore.Testing.Moq:** Biblioteca para Moq y NSubstitute. Está diseñada para usarse junto con el proveedor en memoria de Microsoft , ampliando su funcionalidad. [Más información](https://github.com/rgvlee/EntityFrameworkCore.Testing)
    *Si bien no permite realizar un test sobre los sp si es util para mockear lo que un sp va a devolver y realizar pruebas sobre el comportamiento del codigo que realiza alguna funcionalidad utilizando los parametros obtenidos del sp y ver si el resultado es el esperado.*
    - **FluentAssertions:** Para escribir aserciones más sencillas y claras. [Más información](https://fluentassertions.com/)

###### Ejemplo de utilización
**1.** Como primer paso se crea el ambiente de pruebas con todos los actores intervinientes, es decir contexto, interfaces que deben ser mockeadas y mapeadores necesarios.
> En este caso el servicio real utiliza, **IAbstractContextFactory**  y **IAbstractServiceFactory** por lo tanto ambos deben ser mockeados, dado que este ultimo utiliza otros servicios como lo son **ILogger** y **Mapper** debemos evaluar cual debe ser mockeado y cual no, para este caso  **ILogger mock** y utilizamos el **Mapper real**.
```csharp
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
```
**2.** Se crea un metodo con un nombre correspondiente al metodo del servicio que queremos testear y se utiliza del mismo modo que se utilizaria en el contexto real de la aplicación.
> Se instancia el **servicio real** pero utilizando **AbstractContextFactory mock** y **AbstractServiceFactory mock**.
```csharp
        [Test()]
        public void SetEscritoTextoTest()
        {
            ServiceEscritosTexto service = new ServiceEscritosTexto(_mockContex, _mockServiceFactory);

            var testEntity = new EscritosTextoDto() { Titulo = "Por esparta !!", Texto = "Moq!!!!" };
            var result = service.SetEscritoTexto(testEntity);

            Assert.AreEqual(1, result);
        }
```
```csharp
        [Test]
        public void GetAllEscritosTextosTest()
        {
            ServiceEscritosTexto service = new ServiceEscritosTexto(_mockContex, _mockServiceFactory);

            // Se crea una lista de objetos aleatorios
            List<EscritosTextoDto> list = new Fixture().CreateMany<EscritosTextoDto>().ToList();
            list.ForEach(e => service.SetEscritoTexto(e));
            var result = service.GetAllEscritosTextos();

            Assert.Multiple(() =>
            {
                Assert.NotNull(result);
                Assert.Greater(result.Count(), 0);
            });

        }
```
```csharp
[Test]
        public void GetEscritosTextoByIdTest()
        {
            ServiceEscritosTexto service = new ServiceEscritosTexto(_mockContex, _mockServiceFactory);

            // Se crea una lista de objetos aleatorios
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
```
* Hay casos donde no se testearan los sp dado que no es posible o no es relavante mockearlos para esos casos se recomienda la siquiente implementacion en dichos casos, con el fin de dejar constancia y matener el la consistencia de los test.

> Para agregar mensajes o utilizar mensajes se agrego una clase **Message**.
```csharp
        [Test()]
        public void GetUltimoEscritosTextoTest()
        {
            Assert.Ignore(Message.sp_NotAscertainable);
        }
```
#### Salida en Visual Studio a correr el test

![][image_ref_a32ff4ads]

[image_ref_a32ff4ads]:https://github.com/patricioarena/Editores/blob/develop/Documentation/SalidaVisualStudio.jpg
#### Salida en powershell al correr el test utilizando el comando: dotnet test

![][image_ref_a33ff4ads]

[image_ref_a33ff4ads]:https://github.com/patricioarena/Editores/blob/develop/Documentation/SalidaPowershell.jpg

---

## Extra

Podemos correr los test desde el proyecto Angular utilizando el comando: **yarn dtt**

#### Salida en git bash al correr el test desde vs code utilizando el comando.
![][image_ref_a31ff4ads]

[image_ref_a31ff4ads]:https://github.com/patricioarena/Editores/blob/develop/Documentation/SalidaGitBash.png
