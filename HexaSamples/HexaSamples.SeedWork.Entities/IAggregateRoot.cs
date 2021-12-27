using HexaSamples.SeedWork.Entities.Events;

namespace HexaSamples.SeedWork.Entities;

/// <summary>
/// <para>
///     Entidade que raiz de agregado.
/// </para>
/// <para>
///     A raiz de agregado é a entidade principal de um agregado.
///     Geralmente só existirá repositórios para as entidades raiz de agregado, e esta entidade criará e armazenará
///     os eventos ocorridos em todos objetos internos do agregado.
/// </para>
/// </summary>
public interface IAggregateRoot : IEntity, IHasEvents { }

/// <summary>
/// <para>
///     Entidade que raiz de agregado com definição do tipo do Id.
/// </para>
/// <para>
///     Tem as mesmas caracteristicas que <see cref="IAggregateRoot"/>.
/// </para>
/// </summary>
/// <typeparam name="TId">Tipo de dado do Id da entidade.</typeparam>
public interface IAggregateRoot<TId> : IAggregateRoot, IEntity<TId> { }