
using System.Text.Json.Serialization;

namespace HexaSamples.SeedWork.Results;

/// <summary>
/// Modelo que contém informações de uma exception, utilizado nas mensagens dos <see cref="IResult"/>.
/// </summary>
public record MessageException
{
    private readonly Exception? originException;

    /// <summary>
    /// Gets a message that describes the current exception.
    /// </summary>
    public string Message { get; }

    /// <summary>
    /// Gets a string representation of the immediate frames on the call stack.
    /// </summary>
    public string? StackTrace { get; }

    /// <summary>
    /// The FullName of the exception type.
    /// </summary>
    public string FullNameOfExceptionType { get; }

    /// <summary>
    /// Gets the System.Exception instance that caused the current exception.
    /// </summary>
    public MessageException? InnerException { get; }

    /// <summary>
    /// Cria nova <see cref="MessageException"/> a partir de uma <see cref="Exception"/>.
    /// </summary>
    /// <param name="ex">Exception</param>
    public MessageException(Exception ex)
    {
        Message = ex.Message;
        FullNameOfExceptionType = ex.GetType().FullName!;
        StackTrace = ex.StackTrace;
        if (ex.InnerException is not null)
            InnerException = new MessageException(ex.InnerException);

        originException = ex;
    }

    /// <summary>
    /// Cria nova instância com os valores das propriedades.
    /// </summary>
    /// <param name="message">Mensagem da Exception.</param>
    /// <param name="stackTrace">Strack trace da exception.</param>
    /// <param name="fullNameOfExceptionType">Nome completo do tipo (classe) da exception.</param>
    /// <param name="innerException">A exception interna, caso exista.</param>
    /// <exception cref="ArgumentNullException"></exception>
    [JsonConstructor]
    public MessageException(
        string message, 
        string? stackTrace, 
        string fullNameOfExceptionType,
        MessageException? innerException)
    {
        Message = message ?? throw new ArgumentNullException(nameof(message));
        StackTrace = stackTrace;
        FullNameOfExceptionType = fullNameOfExceptionType ?? throw new ArgumentNullException(nameof(fullNameOfExceptionType));
        InnerException = innerException;
    }

    /// <summary>
    /// <para>
    ///     Obtém a exception de origem, caso não seja uma deserialização da mensagem.
    /// </para>
    /// <para>
    ///     Se houver uma serialização e deserialização, o valor será sempre nulo.
    ///     Se a instância do objeto deste tipo for criado a partir de uma exception, a exception de origem, ela será
    ///     retornada.
    /// </para>
    /// </summary>
    /// <returns>
    ///     A exception de origem, caso for criado a partir de uma exception, ou nulo se o objeto for de
    ///     criado a partir de uma deserialização.
    /// </returns>
    public Exception? GetOriginExcepion() => originException;
}
