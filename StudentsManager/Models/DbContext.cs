using Microsoft.EntityFrameworkCore;
using StudentsManager.Models;
using System.IO;

public class ApplicationDbContext : DbContext
{
    // Constructor para la inyección de dependencias en tiempo de ejecución
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    // Constructor para las herramientas de Entity Framework en tiempo de diseño
    public ApplicationDbContext()
    {
    }

    public DbSet<Student> Students { get; set; }
    public DbSet<Homework> Homeworks { get; set; }

    // Este método es crucial para el constructor sin parámetros
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            // Obtener la cadena de conexión desde appsettings.json
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}