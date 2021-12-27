using HexaSamples.SeedWork.Entities.Events;

// ReSharper disable CheckNamespace

namespace HexaSamples.SeedWork.Entities;

/// <summary>
/// <para>
///     Extensions methods for <see cref="IHasEvents"/>.
/// </para>
/// </summary>
public static class HasEventsExtensions
{
    /// <summary>
    /// <para>
    ///     Verifica se existe um evento de um determinado tipo.
    /// </para>
    /// </summary>
    /// <typeparam name="TEvent">Tipo do evento.</typeparam>
    /// <param name="hasEvents">Objeto que contém uma coleção de eventos.</param>
    /// <returns>
    /// <para>
    ///     Verdadeiro se existe, falso caso contrário.
    /// </para></returns>
    public static bool HasEvent<TEvent>(this IHasEvents hasEvents)
        where TEvent : class, IDomainEvent
    {
        if (hasEvents is null)
            throw new ArgumentNullException(nameof(hasEvents));

        return hasEvents.DomainEvents?.OfType<TEvent>().Any() ?? false;
    }

    /// <summary>
    /// <para>
    ///     Tenta obter o primeiro evento de um determinado tipo.
    /// </para>
    /// </summary>
    /// <typeparam name="TEvent">Tipo do evento.</typeparam>
    /// <param name="hasEvents">Objeto que contém uma coleção de eventos.</param>
    /// <param name="event">Referência de retorno.</param>
    /// <returns>
    /// <para>
    ///     Verdadeiro se encontrou um objeto do tipo determinado, falso caso contrário.
    /// </para></returns>
    public static bool TryGetEvent<TEvent>(this IHasEvents hasEvents, out TEvent? @event)
        where TEvent : class, IDomainEvent
    {
        if (hasEvents is null)
            throw new ArgumentNullException(nameof(hasEvents));

        @event = hasEvents.DomainEvents?.OfType<TEvent>().FirstOrDefault();
        return @event != null;
    }

    /// <summary>
    /// <para>
    ///     Obtém todos os eventos de um determinado tipo.
    /// </para>
    /// </summary>
    /// <typeparam name="TEvent">Tipo do evento.</typeparam>
    /// <param name="hasEvents">Objeto que contém uma coleção de eventos.</param>
    /// <returns>
    /// <para>
    ///     Um <see cref="IEnumerable{T}"/> dos eventos.
    /// </para></returns>
    public static IEnumerable<TEvent> GetEvents<TEvent>(this IHasEvents hasEvents)
        where TEvent : class, IDomainEvent
    {
        if (hasEvents is null)
            throw new ArgumentNullException(nameof(hasEvents));

        return hasEvents.DomainEvents?.OfType<TEvent>() ?? Array.Empty<TEvent>();
    }

    /// <summary>
    /// <para>
    ///     Obtém todos os eventos de um determinado tipo.
    /// </para>
    /// </summary>
    /// <typeparam name="TEvent">Tipo do evento.</typeparam>
    /// <param name="hasEvents">Objeto que contém uma coleção de eventos.</param>
    /// <param name="events">Um <see cref="IEnumerable{T}"/> dos eventos.</param>
    /// <returns>
    /// <para>
    ///     Verdadeiro se encontrou algum objeto do tipo determinado, falso caso contrário.
    /// </para></returns>
    public static bool TryGetEvents<TEvent>(this IHasEvents hasEvents, out IEnumerable<TEvent> events)
        where TEvent : class, IDomainEvent
    {
        if (hasEvents is null)
            throw new ArgumentNullException(nameof(hasEvents));

        events = hasEvents.DomainEvents?.OfType<TEvent>() ?? Array.Empty<TEvent>();
        return events.Any();
    }
}