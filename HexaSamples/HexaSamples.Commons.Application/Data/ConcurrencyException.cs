
namespace HexaSamples.Commons.Application.Data;

/// <summary>
/// Exception para quando ocorrer falhas de concorrência no banco de dados.
/// </summary>
public class ConcurrencyException : Exception
{
    /// <summary>
    /// Construtor padrão.
    /// </summary>
    /// <param name="message">Mensagem.</param>
    /// <param name="innerException">Exception original.</param>
    public ConcurrencyException(string message, Exception innerException) : base(message, innerException) { }
}