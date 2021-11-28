
using HexaSamples.Commons.Results;

namespace HexaSamples.Commons.Application.Data;

/// <summary>
/// Resultado da operação de salvar.
/// </summary>
public interface ISaveResult : IResult
{
    /// <summary>
    /// Quantidade de entidades modificadas.
    /// </summary>
    int Changes { get; }
}
