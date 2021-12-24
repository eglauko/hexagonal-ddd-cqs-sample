
using HexaSamples.SeedWork.Persistence.Abstractions;
using HexaSamples.SeedWork.Entities;
using Microsoft.EntityFrameworkCore;

namespace HexaSamples.SeedWork.Persistence.EntityFrameworkCore;

public class Repository<TDbContext, TEntity> : IRepository<TEntity>
    where TDbContext : DbContext
    where TEntity : class
{
    private readonly TDbContext _db;

    public Repository(TDbContext dbContext)
    {
        _db = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    /// <inheritdoc/>
    public TEntity? Find(object id) => _db.Set<TEntity>().Find(id);
    
    /// <inheritdoc/>
    public ValueTask<TEntity?> FindAsync(object id, CancellationToken token = default)
        => _db.Set<TEntity>().FindAsync(new object[] { id }, token);
    
    /// <inheritdoc/>
    public void Add(TEntity entity) 
        => _db.Set<TEntity>()
            .Add(entity ?? throw new ArgumentNullException(nameof(entity)));

    /// <inheritdoc/>
    public bool Merge<TId>(IHasId<TId> model)
    {
        if (model is null)
            throw new ArgumentNullException(nameof(model));

        var entity = _db.Set<TEntity>().Find(model.Id);
        if (entity is null)
            return false;

        var entry = _db.Entry(entity);
        entry.CurrentValues.SetValues(model);

        return true;
    }

    /// <inheritdoc/>
    public async Task<bool> MergeAsync<TId>(IHasId<TId> model, CancellationToken token = default)
    {
        if (model is null)
            throw new ArgumentNullException(nameof(model));

        var entity = await _db.Set<TEntity>().FindAsync(new object[] { model.Id! }, token);
        if (entity is null)
            return false;

        var entry = _db.Entry(entity);
        entry.CurrentValues.SetValues(model);

        return true;
    }

    /// <inheritdoc/>
    public void Remove(TEntity entity)
    {
        var entry = _db.Entry(entity);
        entry.State = EntityState.Deleted;
    }

    /// <inheritdoc/>
    public TEntity? Delete(object id)
    {
        var entity = _db.Set<TEntity>().Find(id);

        if (entity is not null)
            _db.Entry(entity).State = EntityState.Deleted;

        return entity;
    }

    /// <inheritdoc/>
    public async Task<TEntity?> DeleteAsync(object id, CancellationToken token = default)
    {
        var entity = await _db.Set<TEntity>().FindAsync(new object[] { id }, token);

        if (entity != null)
            _db.Entry(entity).State = EntityState.Deleted;

        return entity;
    }
}
