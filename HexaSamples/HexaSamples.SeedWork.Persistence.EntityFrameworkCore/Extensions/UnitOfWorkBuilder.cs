using HexaSamples.SeedWork.Persistence.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace HexaSamples.SeedWork.Persistence.EntityFrameworkCore.Extensions;

internal class UnitOfWorkBuilder<TDbContext> : IUnitOfWorkBuilder<TDbContext>
    where TDbContext : DbContext
{
    private readonly IServiceCollection services;
    private readonly ServiceLifetime lifetime;
    
    public UnitOfWorkBuilder(IServiceCollection services, ServiceLifetime lifetime)
    {
        this.services = services;
        this.lifetime = lifetime;
    }

    public IUnitOfWorkBuilder<TDbContext> ConfigureDbContextPool(Action<DbContextOptionsBuilder> configurer)
    {
        services.AddDbContextPool<TDbContext>(configurer);
        return this;
    }
    
    public IUnitOfWorkBuilder<TDbContext> ConfigureDbContextPool(Action<IServiceProvider, DbContextOptionsBuilder> configurer)
    {
        services.AddDbContextPool<TDbContext>(configurer);
        return this;
    }
    
    public IUnitOfWorkBuilder<TDbContext> ConfigureDbContext(Action<DbContextOptionsBuilder> configurer)
    {
        services.AddDbContext<TDbContext>(configurer, lifetime);
        return this;
    }
    
    public IUnitOfWorkBuilder<TDbContext> ConfigureDbContext(Action<IServiceProvider, DbContextOptionsBuilder> configurer)
    {
        services.AddDbContext<TDbContext>(configurer, lifetime);
        return this;
    }

    public IUnitOfWorkBuilder<TDbContext> AddRepository<TEntity>() where TEntity : class
    {
        services.Add(ServiceDescriptor.Describe(
            typeof(IRepository<>).MakeGenericType(typeof(TEntity)),
            typeof(Repository<,>).MakeGenericType(typeof(TDbContext), typeof(TEntity)),
            lifetime));

        return this;
    }
}