using HexaSamples.SeedWork.Entities.Events;

namespace HexaSamples.SeedWork.Entities;

/// <summary>
/// Extensions methods for <see cref="IHasEvents"/>.
/// </summary>
public static class HasEventsExtensions
{
    /// <summary>
    /// Verifica se existe um evento de um determinado tipo.
    /// </summary>
    /// <typeparam name="TEvent">Tipo do evento.</typeparam>
    /// <param name="hasEvents">Objeto que contém uma coleção de eventos.</param>
    /// <returns>Verdadeiro se existe, falso caso contrário.</returns>
    public static bool HasEvent<TEvent>(this IHasEvents hasEvents)
        where TEvent : class, IDomainEvent
    {
        if (hasEvents is null)
            throw new System.ArgumentNullException(nameof(hasEvents));

        return hasEvents.DomainEvents?.OfType<TEvent>().Any() ?? false;
    }

    /// <summary>
    /// Tenta obter o primeiro evento de um determinado tipo.
    /// </summary>
    /// <typeparam name="TEvent">Tipo do evento.</typeparam>
    /// <param name="hasEvents">Objeto que contém uma coleção de eventos.</param>
    /// <param name="event">Referência de retorno.</param>
    /// <returns>Verdadeiro se encontrou um objeto do tipo determinado, falso caso contrário.</returns>
    public static bool TryGetEvent<TEvent>(this IHasEvents hasEvents, out TEvent? @event)
        where TEvent : class, IDomainEvent
    {
        if (hasEvents is null)
            throw new ArgumentNullException(nameof(hasEvents));

        @event = hasEvents.DomainEvents?.OfType<TEvent>().FirstOrDefault();
        return @event != null;
    }

    /// <summary>
    /// Obtém todos os eventos de um determinado tipo.
    /// </summary>
    /// <typeparam name="TEvent">Tipo do evento.</typeparam>
    /// <param name="hasEvents">Objeto que contém uma coleção de eventos.</param>
    /// <returns>Um <see cref="IEnumerable{T}"/> dos eventos.</returns>
    public static IEnumerable<TEvent> GetEvents<TEvent>(this IHasEvents hasEvents)
        where TEvent : class, IDomainEvent
    {
        if (hasEvents is null)
            throw new ArgumentNullException(nameof(hasEvents));

        return hasEvents.DomainEvents?.OfType<TEvent>() ?? Array.Empty<TEvent>();
    }

    /// <summary>
    /// Obtém todos os eventos de um determinado tipo.
    /// </summary>
    /// <typeparam name="TEvent">Tipo do evento.</typeparam>
    /// <param name="hasEvents">Objeto que contém uma coleção de eventos.</param>
    /// <param name="events">Um <see cref="IEnumerable{T}"/> dos eventos.</param>
    /// <returns>Verdadeiro se encontrou algum objeto do tipo determinado, falso caso contrário.</returns>
    public static bool TryGetEvents<TEvent>(this IHasEvents hasEvents, out IEnumerable<TEvent> events)
        where TEvent : class, IDomainEvent
    {
        if (hasEvents is null)
            throw new System.ArgumentNullException(nameof(hasEvents));

        events = hasEvents.DomainEvents?.OfType<TEvent>() ?? Array.Empty<TEvent>();
        return events.Any();
    }
}