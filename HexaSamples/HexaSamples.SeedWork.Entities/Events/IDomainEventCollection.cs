
namespace HexaSamples.SeedWork.Entities.Events;

/// <summary>
/// <para>
///     A coleção de eventos de domínio é uma interface para componentes de publicação de eventos de domínio.
/// </para>
/// <para>
///     Os eventos de domínio serão publicados, realmente, por algum componente vinculado a coleção de eventos,
///     geralmente ao salvar o contexto da entidade que o criou.
/// </para>
/// </summary>
/// <remarks>
/// <para>
///     Este componente deverá ser usado por entidades que são raizes de agregados.
///     Estas entidades, que são raizes de agregados, deverão implementar IHasEvents.
/// </para>
/// </remarks>
public interface IDomainEventCollection : ICollection<IDomainEvent>
{
    /// <summary>
    /// <para>
    ///     Adiciona um observador a coleção de eventos.
    /// </para>
    /// <para>
    ///     O Observador irá recebeder todas os eventos adicionados na coleção.
    ///     Se já existir eventos adicionados a coleção ao observador ser adicionado a ela,
    ///     eles serão passados ao observador, um a um, no momento da adição do observador.
    ///     Os eventos seguintes serão passados ao observador no momento da adição do evento.
    /// </para>
    /// </summary>
    /// <param name="observerAction">Ação observadora.</param>
    void Observe(Action<IDomainEvent> observerAction);
}