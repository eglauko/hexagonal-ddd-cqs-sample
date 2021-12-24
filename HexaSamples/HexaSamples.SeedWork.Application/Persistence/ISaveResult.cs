
using HexaSamples.SeedWork.Results;

namespace HexaSamples.SeedWork.Application.Persistence;

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
