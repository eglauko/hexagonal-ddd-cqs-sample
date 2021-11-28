namespace HexaSamples.Commons.Entities;

/// <summary>
/// Interface para determinar as entidades.
/// </summary>
public interface IEntity { }

/// <summary>
/// Interface para determinar as entidades.
/// </summary>
/// <typeparam name="TId">Tipo de dado do Id da entidade.</typeparam>
public interface IEntity<TId> : IEntity, IHasId<TId> { }
