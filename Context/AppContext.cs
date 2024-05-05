using microservices.Entities;
using microservices.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace microservices.Context
{
    public class AppDbContext : DbContext, IAppDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Categorias> Categorias { get; set; }

        /*el metodo OnConfiguring lo que hace es definir la ruta de conexion en caso que no haya sido definido 
         * anteriormente*/
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
               // @"Server=mssql;Database=microservicios;User Id=sa;Password=M1st2rPasswOrd!;TrustServerCertificate=true;");
            @"Server=10.0.0.7,14333;Database=microservicios;User Id=sa;Password=M1st2rPasswOrd!;TrustServerCertificate=true;");
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
       => SaveChangesAsync(acceptAllChangesOnSuccess: true, cancellationToken);

    }
}
