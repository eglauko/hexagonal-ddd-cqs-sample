
namespace HexaSamples.SeedWork.Results;

/// <summary>
/// <para>
///     Componente de mensagem para os resultados de operações de um serviço.
/// </para>
/// </summary>
public class Message : IMessage
{
    /// <summary>
    /// <para>
    ///     Cria uma nova mensagem de sucesso.
    /// </para>
    /// </summary>
    /// <param name="text">Texto da mensagem.</param>
    /// <param name="property">Propriedade relacionada a mensagem, opcional.</param>
    /// <param name="code">Codigo da mensagem, opcional.</param>
    /// <returns>
    /// <para>
    ///     Nova instância da mensagem.
    /// </para>
    /// </returns>
    public static Message Success(string text, string? property = null, string? code = null)
    {
        return new Message(MessageType.Success, text, property, code, null);
    }

    /// <summary>
    /// <para>
    ///     Cria uma nova mensagem de informação.
    /// </para>
    /// </summary>
    /// <param name="text">Texto da mensagem.</param>
    /// <param name="property">Propriedade relacionada a mensagem, opcional.</param>
    /// <param name="code">Codigo da mensagem, opcional.</param>
    /// <returns>
    /// <para>
    ///     Nova instância da mensagem.
    /// </para></returns>
    public static Message Info(string text, string? property = null, string? code = null)
    {
        return new Message(MessageType.Info, text, property, code, null);
    }

    /// <summary>
    /// <para>
    ///     Cria uma nova mensagem de alerta.
    /// </para>
    /// </summary>
    /// <param name="text">Texto da mensagem.</param>
    /// <param name="property">Propriedade relacionada a mensagem, opcional.</param>
    /// <param name="code">Codigo da mensagem, opcional.</param>
    /// <param name="ex">Exception que ocorreu, opcional.</param>
    /// <returns>
    /// <para>
    ///     Nova instância da mensagem.
    /// </para>
    /// </returns>
    public static Message Warning(string text, string? property = null, string? code = null, Exception? ex = null)
    {
        return new Message(MessageType.Warning, text, property, code, ex);
    }

    /// <summary>
    /// <para>
    ///     Cria uma nova mensagem de erro.
    /// </para>
    /// </summary>
    /// <param name="text">Texto da mensagem.</param>
    /// <param name="property">Propriedade relacionada a mensagem, opcional.</param>
    /// <param name="code">Codigo da mensagem, opcional.</param>
    /// <param name="ex">Exception que ocorreu, opcional.</param>
    /// <returns>
    /// <para>
    ///     Nova instância da mensagem.
    /// </para>
    /// </returns>
    public static Message Error(string text, string? property = null, string? code = null, Exception? ex = null)
    {
        return new Message(MessageType.Error, text, property, code, ex);
    }

    /// <summary>
    /// <para>
    ///     Cria uma mensagem com código de erro de <see cref="ResultErrorCodes.NotFound"/>.
    /// </para>
    /// </summary>
    /// <param name="text">Texto da mensagem.</param>
    /// <returns>
    /// <para>
    ///     Nova instância da mensagem.
    /// </para>
    /// </returns>
    public static Message NotFound(string text)
    {
        return new Message(MessageType.Error, text, null, ResultErrorCodes.NotFound, null);
    }

    /// <summary>
    /// <para>
    ///     Cria uma mensagem com código de erro de <see cref="ResultErrorCodes.InvalidParameters"/>.
    /// </para>
    /// </summary>
    /// <param name="text">Texto da mensagem.</param>
    /// <param name="property">Propriedade relacionada a mensagem, nome do parâmetro inválido.</param>
    /// <returns>
    /// <para>
    ///     Nova instância da mensagem.
    /// </para>
    /// </returns>
    public static Message InvalidParameters(string text, string property)
    {
        return new Message(MessageType.Error, text, property, ResultErrorCodes.InvalidParameters, null);
    }

    /// <summary>
    /// <para>
    ///     Cria uma mensagem com código de erro de <see cref="ResultErrorCodes.Validation"/>.
    /// </para>
    /// </summary>
    /// <param name="text">Texto da mensagem.</param>
    /// <param name="property">Propriedade relacionada a mensagem, nome da propriedade validada.</param>
    /// <param name="ex">Exception de validação.</param>
    /// <returns>
    /// <para>
    ///     Nova instância da mensagem.
    /// </para>
    /// </returns>
    public static Message ValidationError(string text, string? property = null, Exception? ex = null)
    {
        return new Message(MessageType.Error, text, property, ResultErrorCodes.Validation, ex);
    }

    /// <summary>
    /// <para>
    ///     Cria uma mensagem com código de erro de <see cref="ResultErrorCodes.ApplicationError"/>
    /// </para>
    /// </summary>
    /// <param name="ex">Exception ocorrida.</param>
    /// <param name="text">Texto da mensagem, optional, quando não informado será a mensagem da exception.</param>
    /// <returns>
    /// <para>
    ///     Nova instância da mensagem.
    ///</para>
    /// </returns>
    public static Message ApplicationError(Exception ex, string? text = null)
    {
        if (ex is null)
            throw new ArgumentNullException(nameof(ex));

        return new Message(MessageType.Error, text ?? ex.Message, null, ResultErrorCodes.ApplicationError, ex);
    }

    /// <summary>
    /// Código relacionado a mensagem.
    /// </summary>
    public string? Code { get; private set; }

    /// <summary>
    /// Exception relacionada a mensagem.
    /// </summary>
    public MessageException? Exception { get; private set; }

    /// <summary>
    /// Texto da mensagem.
    /// </summary>
    public string Text { get; private set; }

    /// <summary>
    /// Propriedade relacionada a mensagem.
    /// </summary>
    public string? Property { get; private set; }

    /// <summary>
    /// Tipo da mensagem.
    /// </summary>
    public MessageType Type { get; private set; }

    /// <summary>
    /// Construtor com os devidos parâmetros.
    /// </summary>
    /// <param name="type">Tipo de mensagem.</param>
    /// <param name="text">Texto da mensagem.</param>
    public Message(MessageType type, string text)
        : this(type, text, null, null, null) { }

    /// <summary>
    /// Construtor com todos parâmetros.
    /// </summary>
    /// <param name="type">Tipo de mensagem.</param>
    /// <param name="text">Texto da mensagem.</param>
    /// <param name="property">Propriedade relacionada a mensagem.</param>
    public Message(MessageType type, string text, string? property)
        : this(type, text, property, null, null) { }

    /// <summary>
    /// Construtor com todos parâmetros.
    /// </summary>
    /// <param name="type">Tipo de mensagem.</param>
    /// <param name="text">Texto da mensagem.</param>
    /// <param name="property">Propriedade relacionada a mensagem.</param>
    /// <param name="code">Codigo da mensagem.</param>
    public Message(MessageType type, string text, string? property, string? code)
        : this(type, text, property, code, null) { }

    /// <summary>
    /// Construtor com todos parâmetros.
    /// </summary>
    /// <param name="type">Tipo de mensagem.</param>
    /// <param name="text">Texto da mensagem.</param>
    /// <param name="exception">Exception que ocorreu.</param>
    public Message(MessageType type, string text, Exception? exception)
        : this(type, text, null, null, exception) { }

    /// <summary>
    /// Construtor com todos parâmetros.
    /// </summary>
    /// <param name="type">Tipo de mensagem.</param>
    /// <param name="text">Texto da mensagem.</param>
    /// <param name="property">Propriedade relacionada a mensagem.</param>
    /// <param name="exception">Exception que ocorreu.</param>
    public Message(MessageType type, string text, string? property, Exception? exception)
        : this(type, text, property, null, exception) { }

    /// <summary>
    /// Construtor de mensagem de erro a partir de uma exception.
    /// </summary>
    /// <param name="exception">Exception que ocorreu.</param>
    public Message(Exception exception)
        : this(MessageType.Error, exception.Message, null, null, exception) { }

    /// <summary>
    /// Construtor com todos parâmetros.
    /// </summary>
    /// <param name="type">Tipo de mensagem.</param>
    /// <param name="text">Texto da mensagem.</param>
    /// <param name="property">Propriedade relacionada a mensagem.</param>
    /// <param name="code">Codigo da mensagem.</param>
    /// <param name="exception">Exception que ocorreu.</param>
    public Message(MessageType type, string text, string? property, string? code, Exception? exception)
    {
        Type = type;
        Text = text;
        Property = property;
        Code = code;
        if (exception is not null)
            Exception = new MessageException(exception);
    }

    /// <summary>
    /// Retorna mensagem ao ser exibida
    /// </summary>
    public override string ToString() => Text;
}
