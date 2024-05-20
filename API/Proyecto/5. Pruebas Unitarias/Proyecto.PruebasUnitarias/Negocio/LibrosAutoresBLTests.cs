

namespace Proyecto.PruebasUnitarias.Negocio
{
    using Newtonsoft.Json;
    using Proyecto.Datos.Clases.DO.Consulta;
    using Proyecto.Datos.Contexto.Repositorio;
    using Proyecto.IC.DTO.Repositorio;
    using Proyecto.Negocio.Clases.BL;
    using System.Collections.Generic;
    using Xunit;

    public class LibrosAutoresBLTests
    {
        private readonly string ruta = Directory.GetCurrentDirectory();

        [Fact]
        public void ConsultarLibrosAutores_DebeRetornarListaCorrecta()
        {
            // Arrange 

           

            List<IAutoresDTO> listaAutores = new List<IAutoresDTO>
            {
                new Autores
                {
                    AutorID = Guid.Parse("0372923A-2C22-4035-B494-02E43213CECB"),
                    Nombre = "Gabriel",
                    Apellido = "García Márquez",
                    FechaNacimiento = new DateTime(1927, 3, 6),
                    Nacionalidad = "Colombiano"
                },
                new Autores
                {
                    AutorID = Guid.Parse("0D7C69ED-F55B-447F-B91A-5807F3151C55"),
                    Nombre = "Isabel",
                    Apellido = "Allende",
                    FechaNacimiento = new DateTime(1942, 8, 2),
                    Nacionalidad = "Chilena"
                }
            };

            List<ILibrosDTO> listalibros = new List<ILibrosDTO>
            {
                new Libros
                {
                    LibroID = Guid.Parse("6C0802F8-636C-4067-835A-5F1B73F954C4"),
                    Titulo = "Cien Años de Soledad",
                    FechaPublicacion = new DateTime(1967, 5, 30),
                    Genero = "Novela",
                    ISBN = "9780307474728"
                },
                new Libros
                {
                    LibroID = Guid.Parse("D794ADF8-6A9F-4753-970D-67426ED0B6C6"),
                    Titulo = "Rayuela",
                    FechaPublicacion = new DateTime(1963, 6, 28),
                    Genero = "Novela",
                    ISBN = "9788497935502"
                },
                new Libros
                {
                    LibroID = Guid.Parse("FF6C337E-5147-4D71-9E89-BDB270B9ED83"),
                    Titulo = "La Casa de los Espíritus",
                    FechaPublicacion = new DateTime(1982, 10, 15),
                    Genero = "Novela",
                    ISBN = "9780553383805"
                }
            };
            List<ILibrosAutoresConsultaDTO> listalibrosAutoresConsultaEsperada = new List<ILibrosAutoresConsultaDTO>
            {
                new LibrosAutoresConsultaDO
                {
                    LibroAutorID = Guid.Parse("ff6c337e-5147-4d71-9e89-bdb270b9ed83"),
                    LibroID = Guid.Parse("ff6c337e-5147-4d71-9e89-bdb270b9ed83"),
                    AutorID = Guid.Parse("0d7c69ed-f55b-447f-b91a-5807f3151c55"),
                    Titulo = "La Casa de los Espíritus",
                    Autor = "Isabel Allende"
                },
                new LibrosAutoresConsultaDO
                {
                    LibroAutorID = Guid.Parse("6c0802f8-636c-4067-835a-5f1b73f954c4"),
                    LibroID = Guid.Parse("6C0802F8-636C-4067-835A-5F1B73F954C4"),
                    AutorID = Guid.Parse("0372923a-2c22-4035-b494-02e43213cecb"),
                    Titulo = "Cien Años de Soledad",
                    Autor = "Gabriel García Márquez"
                }
            };

            List<ILibrosAutoresDTO> listalibrosAutores = new List<ILibrosAutoresDTO>
            {
                new LibrosAutores
                {
                    LibroAutorID = Guid.Parse("C269D4BC-CD50-4329-A5C6-A82AA2EF1A99"),
                    LibroID = Guid.Parse("FF6C337E-5147-4D71-9E89-BDB270B9ED83"),
                    AutorID = Guid.Parse("0D7C69ED-F55B-447F-B91A-5807F3151C55")
                },
                new LibrosAutores
                {
                    LibroAutorID = Guid.Parse("7BB98613-704D-414E-82AB-FA835825E5CF"),
                    LibroID = Guid.Parse("6C0802F8-636C-4067-835A-5F1B73F954C4"),
                    AutorID = Guid.Parse("0372923A-2C22-4035-B494-02E43213CECB")
                }
            };


            LibrosAutoresBL librosAutores = new LibrosAutoresBL();
            // Act
            List<ILibrosAutoresConsultaDTO> librosAutoresConsulta = librosAutores.ConsultarLibrosAutores(listalibrosAutores,listaAutores,listalibros);

            string resultadoConsulta = JsonConvert.SerializeObject(librosAutoresConsulta);
            string resultadoEsperado = JsonConvert.SerializeObject(listalibrosAutoresConsultaEsperada);
            // Assert
            bool respuesta = resultadoConsulta.Equals(resultadoEsperado);
            Assert.True(respuesta);


        }
    }
}
