
namespace HexaSamples.SeedWork.Entities;

/// <summary>
/// <para>
///     Implementação base para entidades.
/// </para>
/// </summary>
/// <typeparam name="TId">Tipo do ID da entidade.</typeparam>
public abstract class Entity<TId> : IEntity<TId>
{
    /// <summary>
    /// Id da entidade.
    /// </summary>
    public TId Id { get; protected set; }
}

/// <summary>
/// <para>
///     Implementação base para entidades com código.
/// </para>
/// </summary>
/// <typeparam name="TId">Tipo do ID da entidade.</typeparam>
/// <typeparam name="TCode">Tipo do Código da entidade</typeparam>
public abstract class Entity<TId, TCode> : Entity<TId>, IHasCodigo<TCode>
{
    /// <summary>
    /// Código do modelo de dados.
    /// </summary>
    public TCode Codigo { get; protected set; }
}