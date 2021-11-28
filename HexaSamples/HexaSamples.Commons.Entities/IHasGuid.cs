
namespace HexaSamples.Commons.Entities;

/// <summary>
/// Interface para entidades que possuem um campo do tipo Guid.
/// </summary>
public interface IHasGuid
{
    /// <summary>
    /// O Guid da entidade.
    /// </summary>
    Guid Guid { get; }
}