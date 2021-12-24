using HexaSamples.SeedWork.Entities.Events;

namespace HexaSamples.SeedWork.Entities;

/// <summary>
/// Entidade que raiz de agregado.
/// </summary>
public interface IAggregateRoot : IEntity, IHasEvents { }

/// <summary>
/// Entidade que raiz de agregado.
/// </summary>
/// <typeparam name="TId">Tipo de dado do Id da entidade.</typeparam>
public interface IAggregateRoot<TId> : IAggregateRoot, IEntity<TId> { }