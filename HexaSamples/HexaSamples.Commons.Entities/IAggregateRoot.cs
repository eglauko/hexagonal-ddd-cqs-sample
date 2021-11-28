﻿using HexaSamples.Commons.Entities.Events;

namespace HexaSamples.Commons.Entities;

/// <summary>
/// Entidade que raiz de agregado.
/// </summary>
public interface IAggregateRoot : IEntity, IHasEvents { }

/// <summary>
/// Entidade que raiz de agregado.
/// </summary>
/// <typeparam name="TId">Tipo de dado do Id da entidade.</typeparam>
public interface IAggregateRoot<TId> : IAggregateRoot, IEntity<TId> { }