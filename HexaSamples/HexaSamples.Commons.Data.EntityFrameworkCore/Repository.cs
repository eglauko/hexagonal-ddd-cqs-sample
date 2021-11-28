
using HexaSamples.Commons.Data.Abstractions;
using HexaSamples.Commons.Entities;
using Microsoft.EntityFrameworkCore;

namespace HexaSamples.Commons.Data.EntityFrameworkCore;

public class Repository<TDbContext, TEntity> : IRepository<TEntity>
    where TDbContext : DbContext
    where TEntity : class
{
    private readonly TDbContext db;

    public Repository(TDbContext dbContext)
    {
        db = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    /// <inheritdoc/>
    public TEntity? Find(object id) => db.Set<TEntity>().Find(id);
    
    /// <inheritdoc/>
    public ValueTask<TEntity?> FindAsync(object id, CancellationToken token = default)
        => db.Set<TEntity>().FindAsync(new object[] { id }, token);
    
    /// <inheritdoc/>
    public void Add(TEntity entity) 
        => db.Set<TEntity>()
            .Add(entity ?? throw new ArgumentNullException(nameof(entity)));

    /// <inheritdoc/>
    public bool Merge<TId>(IHasId<TId> model)
    {
        if (model is null)
            throw new ArgumentNullException(nameof(model));

        var entity = db.Set<TEntity>().Find(model.Id);
        if (entity is null)
            return false;

        var entry = db.Entry(entity);
        entry.CurrentValues.SetValues(model);

        return true;
    }

    /// <inheritdoc/>
    public async Task<bool> MergeAsync<TId>(IHasId<TId> model, CancellationToken token = default)
    {
        if (model is null)
            throw new ArgumentNullException(nameof(model));

        var entity = await db.Set<TEntity>().FindAsync(new object[] { model.Id! }, token);
        if (entity is null)
            return false;

        var entry = db.Entry(entity);
        entry.CurrentValues.SetValues(model);

        return true;
    }

    /// <inheritdoc/>
    public void Remove(TEntity entity)
    {
        var entry = db.Entry(entity);
        entry.State = EntityState.Deleted;
    }

    /// <inheritdoc/>
    public TEntity? Delete(object id)
    {
        var entity = db.Set<TEntity>().Find(id);

        if (entity is not null)
            db.Entry(entity).State = EntityState.Deleted;

        return entity;
    }

    /// <inheritdoc/>
    public async Task<TEntity?> DeleteAsync(object id, CancellationToken token = default)
    {
        var entity = await db.Set<TEntity>().FindAsync(new object[] { id }, token);

        if (entity != null)
            db.Entry(entity).State = EntityState.Deleted;

        return entity;
    }
}
