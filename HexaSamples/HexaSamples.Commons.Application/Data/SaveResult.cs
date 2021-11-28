﻿
using HexaSamples.Commons.Results;

namespace HexaSamples.Commons.Application.Data;

/// <summary>
/// Implementação de <see cref="ISaveResult"/>.
/// </summary>
public class SaveResult : BaseResult, ISaveResult
{
    /// <summary>
    /// Quantidade de entidades modificadas.
    /// </summary>
    public int Changes { get; private set; }

    /// <summary>
    /// Construtor para sucesso.
    /// </summary>
    /// <param name="changes">Quantidade de entidades modificadas.</param>
    public SaveResult(int changes) : base()
    {
        Changes = changes;
    }

    /// <summary>
    /// Construtor para falhas.
    /// </summary>
    /// <param name="ex">Exception da falha.</param>
    public SaveResult(Exception ex) : base(ex) { }
}