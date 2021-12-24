
namespace HexaSamples.SeedWork.Entities.Events;

/// <summary>
/// Evento de domínio.
/// Veja também <see cref="DomainEventBase"/>
/// </summary>
public interface IDomainEvent : IHasId<Guid>
{
    /// <summary>
    /// Quando ocorreu o evento.
    /// </summary>
    DateTime Occurred { get; }
}