using HexaSamples.SeedWork.Results.Exceptions;

namespace HexaSamples.SeedWork.Results;

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
    /// Gera uma <see cref="InvalidOperationException"/> a partir da mensagem.
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    public static InvalidOperationException ToInvalidOperationException(this IMessage message)
    {
        return message.Exception is not null
            ? new InvalidOperationException(message.Text, message.Exception.ToInvalidOperationInnerException())
            : new InvalidOperationException(message.Text);
    }

    /// <summary>
    /// Gera um <see cref="InvalidOperationInnerException"/> a partir do exception da mensagem.
    /// </summary>
    /// <param name="messageException"></param>
    /// <returns></returns>
    public static InvalidOperationInnerException ToInvalidOperationInnerException(this MessageException messageException)
    {
        return messageException.InnerException is not null
            ? new InvalidOperationInnerException(
                messageException.Message,
                messageException.StackTrace,
                messageException.FullNameOfExceptionType,
                messageException.InnerException.ToInvalidOperationInnerException())
            : new InvalidOperationInnerException(
                messageException.Message,
                messageException.StackTrace,
                messageException.FullNameOfExceptionType);
    }
}

