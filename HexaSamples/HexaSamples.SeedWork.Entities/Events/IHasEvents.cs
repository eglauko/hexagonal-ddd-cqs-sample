
namespace HexaSamples.SeedWork.Entities.Events;

/// <summary>
/// <para>
///     Interface para raiz de agregador que dispara eventos de domínio.
/// </para>
/// <para>
///     Os eventos serão armazenados em um <see cref="IDomainEventCollection"/>, 
///     o qual irá disponibilizar os eventos para a unidade de trabalho, ou componentes de despacho de eventos,
///     dispará-los durante a finalização (salvamento das alterações).
/// </para>
/// </summary>
public interface IHasEvents
{
    /// <summary>
    ///     A coleção de eventos, a qual conterá os eventos gerados pela entidade durante a unidade de trabalho.
    /// </summary>
    /// <remarks>
    /// <para>
    ///     Os eventos de domínio serão escutados pelo contexto a partir do anexo da entidade ao contexto.
    /// </para>
    /// <para>
    ///     Porém, é necessário reforçar que os eventos só serão despachados durante a finalização da
    ///     unidade de trabalho, que é o salvamento do contexto.
    /// </para>
    /// <para>
    ///     Se uma coleção for atribuída a esta propriedade após a entidade ser anexada ao contexto,
    ///     os eventos adicionados a última instância da coleção não serão escutados pelo contexto.
    /// </para>
    /// <para>
    ///     O contexto, por sua vez, irá atribuir uma coleção a esta propriedade quando a entidade for anexada
    ///     ao contexto e ela, a propriedade, for nula.
    /// </para>
    /// </remarks>
    IDomainEventCollection? DomainEvents { get; set; }
}
