
namespace HexaSamples.SeedWork.Entities.Events;

/// <summary>
/// <para>
///     Evento de domínio.
/// </para>
/// <remarks>
///     Veja também <see cref="DomainEventBase"/>
/// </remarks>
/// </summary>
public interface IDomainEvent : IHasId<Guid>
{
    /// <summary>
    /// Quando ocorreu o evento.
    /// </summary>
    DateTimeOffset Occurred { get; }
}