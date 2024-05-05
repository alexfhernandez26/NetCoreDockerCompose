using microservices.Entities;
using Microsoft.EntityFrameworkCore;

namespace microservices.Interfaces
{
    public interface IAppDbContext
    {
        DbSet<Categorias> Categorias { get; set; }
        DbSet<Post> Posts { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}