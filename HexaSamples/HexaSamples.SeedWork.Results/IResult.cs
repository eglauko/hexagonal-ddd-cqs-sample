
namespace HexaSamples.SeedWork.Results;

/// <summary>
/// <para>
///     Interface de componente de resultado de uma operação de um serviço.
/// </para>
/// </summary>
public interface IResult
{
    /// <summary>
    /// Determina se o resultado da operação foi sucesso ou falha.
    /// </summary>
    bool Success { get; }

    /// <summary>
    /// Mensagens do resultado.
    /// </summary>
    IEnumerable<IMessage> Messages { get; }
}

/// <summary>
/// <para>
///     Interface de componente de resultado de uma operação de um serviço com alguma entidade ou modelo de dados.
/// </para>
/// </summary>
/// <typeparam name="TValue">Tipo de dado do valor retornado no resultado da operação.</typeparam>
public interface IResult<TValue> : IResult
{
    /// <summary>
    /// Valor de retorno do resultado.
    /// </summary>
    TValue? Value { get; }
}
