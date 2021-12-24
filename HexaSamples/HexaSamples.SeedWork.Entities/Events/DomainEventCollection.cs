
using System.Collections;

namespace HexaSamples.SeedWork.Entities.Events;

/// <summary>
/// Implementação de <see cref="IDomainEventCollection"/>.
/// </summary>
public class DomainEventCollection : IDomainEventCollection
{
    private List<IDomainEvent>? _innerList;
    private List<Action<IDomainEvent>>? _observers;

    /// <summary>
    /// Lista interna que armazena os eventos de domínio.
    /// </summary>
    protected List<IDomainEvent> InnerList => _innerList ??= new List<IDomainEvent>();

    /// <summary>
    /// Contagem de eventos adicionados na coleção.
    /// </summary>
    public int Count => _innerList?.Count ?? 0;

    /// <summary>
    /// Sempre falso, a coleção sempre poderá receber eventos.
    /// </summary>
    public bool IsReadOnly => false;

    /// <summary>
    /// Adiciona um novo evento a coleção.
    /// </summary>
    /// <param name="item">Evento a ser adicionado a coleção.</param>
    public virtual void Add(IDomainEvent item)
    {
        InnerList.Add(item);
        Fire(item);
    }

    /// <summary>
    /// Limpa os eventos da coleção.
    /// </summary>
    public void Clear() => _innerList?.Clear();

    /// <summary>
    /// Se a coleção tem algum evento adicionado.
    /// </summary>
    /// <param name="item">Evento para ser verificado.</param>
    /// <returns>Verdadeiro se o evento foi adicionado à coleção, falso caso contrario.</returns>
    public bool Contains(IDomainEvent item) => _innerList?.Contains(item) ?? false;

    /// <summary>
    /// Ver CopyTo de ICollection.
    /// </summary>
    /// <param name="array">Array para onde será copiado os eventos.</param>
    /// <param name="arrayIndex">Índice inicial dos eventos que deverão ser copiados.</param>
    public void CopyTo(IDomainEvent[] array, int arrayIndex) => InnerList.CopyTo(array, arrayIndex);

    /// <summary>
    /// Retorna um IEnumerator para os eventos da coleção.
    /// </summary>
    /// <returns>IEnumerator para os eventos da coleção.</returns>
    public IEnumerator<IDomainEvent> GetEnumerator() => InnerList.GetEnumerator();

    /// <summary>
    /// Remove um evento que foi adicionado a coleção.
    /// </summary>
    /// <param name="item">Evento a ser removido.</param>
    /// <returns>Verdadeiro se o evento foi removido, falso caso contrário.</returns>
    public bool Remove(IDomainEvent item) => _innerList?.Remove(item) ?? false;

    /// <summary>
    /// Retorna um IEnumerator para os eventos da coleção.
    /// </summary>
    /// <returns>IEnumerator para os eventos da coleção.</returns>
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

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
    public void Observe(Action<IDomainEvent> observerAction)
    {
        if (observerAction is null)
            throw new ArgumentNullException(nameof(observerAction));

        _observers ??= new List<Action<IDomainEvent>>();
        _observers.Add(observerAction);

        _innerList?.ForEach(observerAction);
    }

    private void Fire(IDomainEvent evt)
    {
        if (_observers is not null)
            _observers.ForEach(a => a(evt));
    }
}
