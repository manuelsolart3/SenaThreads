using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;


//Descargamos 2 paquetes no olvidar eliminarlos, 
//Microsoft.Extensions.Configuration y Microsoft.Extensions.Configuration.json
namespace SenaThreads.Infrastructure;


public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        //Cadena de conexión
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        var connectionString = configuration.GetConnectionString("Database");
        var serverVersion = new MySqlServerVersion(new Version(8, 0, 2));

        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        optionsBuilder.UseMySql(connectionString, serverVersion);

        return new AppDbContext(optionsBuilder.Options);
    }

}
