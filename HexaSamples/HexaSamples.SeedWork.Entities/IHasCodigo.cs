
namespace HexaSamples.SeedWork.Entities;

/// <summary>
/// <para>
///     Interface para modelos de dados com código.
/// </para>
/// <para>
///     O código não é o ID da entidade, mas um identificador único, normalmente mais amigável aos humanos.
/// </para>
/// </summary>
/// <typeparam name="TCode">Tipo de dado do código.</typeparam>
public interface IHasCodigo<TCode>
{
    /// <summary>
    /// Código do modelo de dados.
    /// </summary>
    TCode Codigo { get; }
}