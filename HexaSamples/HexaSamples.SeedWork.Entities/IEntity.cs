namespace HexaSamples.SeedWork.Entities;

/// <summary>
/// <para>
///     Interface para determinar as entidades.
/// </para>
/// </summary>
public interface IEntity { }

/// <summary>
/// <para>
///     Interface para determinar as entidades com um tipo de Id definido.
/// </para>
/// </summary>
/// <typeparam name="TId">Tipo de dado do Id da entidade.</typeparam>
public interface IEntity<TId> : IEntity, IHasId<TId> { }
