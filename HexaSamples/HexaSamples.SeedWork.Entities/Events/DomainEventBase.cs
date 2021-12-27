namespace HexaSamples.SeedWork.Entities.Events;

/// <summary>
/// <para>
///     Implementação abstrata para eventos de domínio.
/// </para>
/// </summary>
public abstract class DomainEventBase : IDomainEvent
{
    /// <summary>
    /// <para>
    ///     Cria um novo evento gerando um novo identificador e utilizando a data e hora atual para determinar
    ///     quando ocorreu o evento.
    /// </para>
    /// </summary>
    protected DomainEventBase()
    {
        Id = Guid.NewGuid();
        Occurred = DateTimeOffset.Now;
    }

    /// <summary>
    /// <para>
    ///     Cria um evento determinando o Id e quando ocorreu.
    ///     Normalmente utilizado para deserialização.
    /// </para>
    /// </summary>
    /// <param name="id">Id do evento.</param>
    /// <param name="occurred">Quando ocorreu.</param>
    protected DomainEventBase(Guid id, DateTimeOffset occurred)
    {
        Id = id;
        Occurred = occurred;
    }

    /// <summary>
    /// Id do evento.
    /// </summary>
    public Guid Id { get; }
    
    /// <summary>
    /// Quando ocorreu o  evento.
    /// </summary>
    public DateTimeOffset Occurred { get; }
}