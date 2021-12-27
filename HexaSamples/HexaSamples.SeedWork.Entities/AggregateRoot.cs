using HexaSamples.SeedWork.Entities.Events;

namespace HexaSamples.SeedWork.Entities;

/// <summary>
/// <para>
///     Implementação base, abstrata, para <see cref="IAggregateRoot{TId}"/>.
/// </para>
/// </summary>
/// <typeparam name="TId">Tipo do ID da entidade.</typeparam>
public abstract class AggregateRoot<TId> : Entity<TId>, IAggregateRoot<TId>
{
    /// <summary>
    /// Coleção de eventos.
    /// </summary>
    public IDomainEventCollection? DomainEvents { get; set; }

    /// <summary>
    /// Adiciona um evento de domínio a coleção, validando se a coleção não é nula e criando uma
    /// coleção se necessário.
    /// </summary>
    /// <param name="evt">Evento a ser adicionado.</param>
    protected void AddEvent(IDomainEvent evt)
    {
        DomainEvents ??= new DomainEventCollection();
        DomainEvents.Add(evt);
    }
}

/// <summary>
/// Implementação base, abstrata, para <see cref="IAggregateRoot{TId}"/>.
/// </summary>
/// <typeparam name="TId">Tipo do ID da entidade.</typeparam>
/// <typeparam name="TCode">Tipo do código da entidade.</typeparam>
public abstract class AggregateRoot<TId, TCode> : AggregateRoot<TId>, IHasCodigo<TCode>
{
    /// <summary>
    /// Código do modelo de dados.
    /// </summary>
    public TCode Codigo { get; protected set; }
}
