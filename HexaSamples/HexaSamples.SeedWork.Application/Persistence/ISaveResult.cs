
using HexaSamples.SeedWork.Results;

namespace HexaSamples.SeedWork.Application.Persistence;

/// <summary>
/// <para>
///     Resultado da operação de salvar.
/// </para>
/// </summary>
public interface ISaveResult : IResult
{
    /// <summary>
    /// <para>
    ///     Quantidade de entidades modificadas.
    /// </para>
    /// </summary>
    int Changes { get; }
}
