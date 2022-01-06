﻿
namespace HexaSamples.SeedWork.Entities.Events;

/// <summary>
/// <para>
///     Domain event.
/// </para>
/// <remarks>
///     See also <see cref="DomainEventBase"/>.
/// </remarks>
/// </summary>
public interface IDomainEvent : IHasId<Guid>
{
    /// <summary>
    /// When the event occurred.
    /// </summary>
    DateTimeOffset Occurred { get; }
}