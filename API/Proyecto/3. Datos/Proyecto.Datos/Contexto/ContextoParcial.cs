using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using System.Data.SqlClient;

namespace Proyecto.Datos.Contexto
{
    public partial class ContextoProyecto
    { 

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                 string connectionString = "Data Source=Localhost;Initial Catalog=Biblioteca;User ID=sa;Password=123456";

                // Crear una instancia de SqlConnection para configurar la validación del certificado
                SqlConnection connection = new SqlConnection(connectionString);

 

                // Configurar la conexión
                optionsBuilder.UseSqlServer((DbConnection)connection);

            }
        }
    }
}