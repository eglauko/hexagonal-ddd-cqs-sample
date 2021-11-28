
namespace HexaSamples.Commons.Entities;

/// <summary>
/// Interface para modelos de dados com Id.
/// </summary>
public interface IHasId<TId>
{
    /// <summary>
    /// Id do modelo de dados.
    /// </summary>
    TId Id { get; }
}
