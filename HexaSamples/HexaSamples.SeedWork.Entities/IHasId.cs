
namespace HexaSamples.SeedWork.Entities;

/// <summary>
/// <para>
///     Interface para modelos de dados com Id, geralmente entidades, ou objetos ao estilo DTO, os quais referenciam
///     o Id de uma entidade.
/// </para>
/// </summary>
public interface IHasId<TId>
{
    /// <summary>
    /// Id do modelo de dados.
    /// </summary>
    TId Id { get; }
}
