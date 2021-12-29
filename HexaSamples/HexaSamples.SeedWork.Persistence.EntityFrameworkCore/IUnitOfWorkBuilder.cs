using Microsoft.EntityFrameworkCore;

namespace HexaSamples.SeedWork.Persistence.EntityFrameworkCore;

public interface IUnitOfWorkBuilder<TDbContext>
    where TDbContext : DbContext
{
    IUnitOfWorkBuilder<TDbContext> ConfigureDbContextPool(Action<DbContextOptionsBuilder> configurer);
    
    IUnitOfWorkBuilder<TDbContext> ConfigureDbContextPool(Action<IServiceProvider, DbContextOptionsBuilder> configurer);
    
    IUnitOfWorkBuilder<TDbContext> ConfigureDbContext(Action<DbContextOptionsBuilder> configurer);
    
    IUnitOfWorkBuilder<TDbContext> ConfigureDbContext(Action<IServiceProvider, DbContextOptionsBuilder> configurer);
    
    IUnitOfWorkBuilder<TDbContext> AddRepository<TEntity>()
        where TEntity : class;
}