
namespace HexaSamples.SeedWork.Entities.Events;

/// <summary>
/// Interface do componente de publicação de eventos de domínio.
/// Os eventos de domínio serão publicados, realmente, ao salvar o contexto da entidade que o criou.
/// Este componente deverá ser usado por entidades que são raizes de agregados.
/// Estas entidades, que são raizes de agregados, deverão implementar IHasEvents.
/// </summary>
public interface IDomainEventCollection : ICollection<IDomainEvent>
{
    /// <summary>
    /// <para>
    /// Adiciona um observador a coleção de eventos.
    /// </para>
    /// <para>
    /// O Observador irá recebeder todas os eventos adicionados na coleção.
    /// Se já existir eventos adicionados a coleção ao observador ser adicionado a ela,
    /// eles serão passados ao observador, um a um, no momento da adição do observador.
    /// Os eventos seguintes serão passados ao observador no momento da adição do evento.
    /// </para>
    /// </summary>
    /// <param name="observerAction">Ação observadora</param>
    void Observe(Action<IDomainEvent> observerAction);
}