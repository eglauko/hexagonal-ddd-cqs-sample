
namespace HexaSamples.Commons.Entities;

/// <summary>
/// Interface para modelos de dados com código.
/// </summary>
/// <typeparam name="TCode">Tipo de dado do código.</typeparam>
public interface IHasCodigo<TCode>
{
    /// <summary>
    /// Código do modelo de dados.
    /// </summary>
    TCode Codigo { get; }
}