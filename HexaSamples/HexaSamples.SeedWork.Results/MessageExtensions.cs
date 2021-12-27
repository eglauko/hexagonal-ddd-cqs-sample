using HexaSamples.SeedWork.Results.Exceptions;

namespace HexaSamples.SeedWork.Results;

/// <summary>
/// Extension methods for results and messages.
/// </summary>
public static class MessageExtensions
{
    /// <summary>
    /// Contatena o texto das mensagens em uma única String, quebrando por linha.
    /// </summary>
    /// <param name="messages">Conjunto de mensagens para serem contatenadas.</param>
    /// <param name="separator">Separador, padrão nova linha.</param>
    /// <returns>Uma String com os textos das mensagens contatenados e quebrando por linha.</returns>
    public static string JoinMessages(this IEnumerable<IMessage> messages, string separator = "\n")
    {
        return string.Join(separator, messages);
    }

    /// <summary>
    /// <para>
    ///     Gera uma exception a partir da mensagen, mas em caso da mensagem já possuir uma exception e seja
    ///     possível obter a instância original da exception, é retornada essa exception de origem.
    /// </para>
    /// </summary>
    /// <param name="message">Mensagem para extrair ou gerar exception.</param>
    /// <returns>
    /// <para>
    ///     Uma instância de uma exception para a mensagem.
    /// </para>
    /// </returns>
    public static Exception ToException(this IMessage message)
    {
        return message.Exception?.GetOriginExcepion() ?? message.ToInvalidOperationException();
    }

    /// <summary>
    /// <para>
    ///     Gera uma <see cref="InvalidOperationException"/> a partir da mensagem.
    /// </para>
    /// </summary>
    /// <param name="message">Mensagem para gerar exception.</param>
    /// <returns>
    /// <para>
    ///     Uma instância de <see cref="InvalidOperationException"/> para a mensagem.
    /// </para>
    /// </returns>
    public static InvalidOperationException ToInvalidOperationException(this IMessage message)
    {
        var originalException = message.Exception?.GetOriginExcepion();
        return originalException is not null
            ? originalException is InvalidOperationException ioex
                ? ioex
                : new InvalidOperationException(message.Text, originalException)
            : message.Exception is not null
                ? new InvalidOperationException(message.Text, message.Exception.ToInvalidOperationInnerException())
                : new InvalidOperationException(message.Text);
    }

    /// <summary>
    /// Gera um <see cref="InvalidOperationInnerException"/> a partir do exception da mensagem.
    /// </summary>
    /// <param name="messageException">Mensagem para gerar exception.</param>
    /// <returns>
    /// <para>
    ///     Uma instância de <see cref="InvalidOperationException"/> para a mensagem.
    /// </para>
    /// </returns>
    public static InvalidOperationInnerException ToInvalidOperationInnerException(this MessageException messageException)
    {
        return messageException.InnerException is not null
            ? new InvalidOperationInnerException(
                messageException.Message,
                messageException.StackTrace!,
                messageException.FullNameOfExceptionType,
                messageException.InnerException.ToInvalidOperationInnerException())
            : new InvalidOperationInnerException(
                messageException.Message,
                messageException.StackTrace!,
                messageException.FullNameOfExceptionType);
    }
}

