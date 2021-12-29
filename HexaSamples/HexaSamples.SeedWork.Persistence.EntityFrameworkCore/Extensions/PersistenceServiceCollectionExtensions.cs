// ReSharper disable CheckNamespace

using HexaSamples.SeedWork.Application.Persistence;
using HexaSamples.SeedWork.Persistence.EntityFrameworkCore;
using HexaSamples.SeedWork.Persistence.EntityFrameworkCore.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Extensions methods for <see cref="IServiceCollection"/>.
/// </summary>
public static class PersistenceServiceCollectionExtensions
{
    public static IUnitOfWorkBuilder<TDbContext> AddUnitOfWork<TDbContext>(
        this IServiceCollection services,
        ServiceLifetime lifetime = ServiceLifetime.Scoped)
        where TDbContext : DbContext
    {
        services.Add(ServiceDescriptor.Describe(
            typeof(IUnitOfWorkContext), 
            typeof(UnitOfWorkContext<>).MakeGenericType(typeof(TDbContext)),
            lifetime));

        return new UnitOfWorkBuilder<TDbContext>(services, lifetime);
    }
}