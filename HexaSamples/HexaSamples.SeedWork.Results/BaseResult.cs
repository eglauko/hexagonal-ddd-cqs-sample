
namespace HexaSamples.SeedWork.Results;

/// <summary>
/// Implementação padrão de IResultadoOperacao.
/// </summary>
/// <see cref="IResult"/>
public class BaseResult : IResult
{
    /// <summary>
    /// Instância de <see cref="Immutable.ImmutableSuccess"/>. Singleton.
    /// </summary>
    public static readonly IResult ImmutableSuccess = new Immutable.ImmutableSuccess();

    /// <summary>
    /// Instância de <see cref="Immutable.ImmutableFailure"/>. Singleton.
    /// </summary>
    public static readonly IResult ImmutableFailure = new Immutable.ImmutableFailure();

    /// <summary>
    /// Lista privada para armazenar as mensagens.
    /// </summary>
    private readonly List<IMessage> _messages = new();

    /// <summary>
    /// Mensagens do resultado.
    /// </summary>
    public IEnumerable<IMessage> Messages { get { return _messages.AsReadOnly(); } }

    /// <summary>
    /// Determina se o resultado da operação foi sucesso ou falha.
    /// </summary>
    public bool Success { get; private set; }

    /// <summary>
    /// Construtor padrão, com resultado em sucesso, até ser adicionada uma mensagem de erro.
    /// </summary>
    public BaseResult()
    {
        Success = true;
    }

    /// <summary>
    /// Construtor de falha, com uma mensagem de erro.
    /// </summary>
    /// <param name="errorMessage">Texto da mensagem de erro.</param>
    public BaseResult(string errorMessage)
    {
        this.AddError(errorMessage);
    }

    /// <summary>
    /// Construtor de falha, através de uma exception ocorrida.
    /// </summary>
    /// <param name="ex">Exception de erro.</param>
    public BaseResult(Exception ex)
    {
        this.AddError(ex);
    }

    /// <summary>
    /// Cria um <see cref="BaseResult"/> a partir de outro resultado (<see cref="IResult"/>).
    /// </summary>
    /// <param name="other"></param>
    public BaseResult(IResult other)
    {
        Success = other.Success;
        _messages.AddRange(other.Messages);
    }

    /// <summary>
    /// Interno, para <see cref="TypeResult{TModel}"/>.
    /// </summary>
    /// <param name="other"></param>
    /// <param name="success"></param>
    internal protected BaseResult(IResult other, bool success)
    {
        Success = success;
        _messages.AddRange(other.Messages);
    }

    /// <summary>
    /// Cria um novo Resultado Operação de Sucesso.
    /// </summary>
    /// <returns>Nova instância.</returns>
    public static BaseResult CreateSuccess()
    {
        return new BaseResult();
    }

    /// <summary>
    /// Cria um novo Resultado Operação de Falha.
    /// </summary>
    /// <param name="error">Texto da mensagem de erro.</param>
    /// <returns>Nova instância.</returns>
    public static BaseResult CreateFailure(string error)
    {
        return new BaseResult(error);
    }

    /// <summary>
    /// Cria um novo Resultado Operação de Falha.
    /// </summary>
    /// <param name="ex"><see cref="Exception"/> de erro.</param>
    /// <returns>Nova instância.</returns>
    public static BaseResult CreateFailure(Exception ex)
    {
        return new BaseResult(ex);
    }

    /// <summary>
    /// Cria um novo Resultado Operação de Falha.
    /// </summary>
    /// <param name="error">Texto da mensagem de erro.</param>
    /// <param name="property">Propriedade da mensagem de erro.</param>
    /// <returns>Nova instância.</returns>
    public static BaseResult CreateFailure(string error, string property, string? code = null)
    {
        var resultado = new BaseResult();
        resultado.AddError(error, property, code);
        return resultado;
    }

    /// <summary>
    /// Cria um novo resultado com código de erro de <see cref="ResultErrorCodes.NotFound"/>.
    /// </summary>
    /// <param name="text">Texto da mensagem.</param>
    /// <returns>Nova instância de <see cref="BaseResult"/>.</returns>
    public static BaseResult NotFound(string text)
    {
        var result = new BaseResult();
        result.AddMessage(Message.NotFound(text));
        return result;
    }

    /// <summary>
    /// Cria um novo resultado com código de erro de <see cref="ResultErrorCodes.InvalidParameters"/>.
    /// </summary>
    /// <param name="text">Texto da mensagem.</param>
    /// <param name="property">Propriedade relacionada a mensagem, nome do parâmetro inválido.</param>
    /// <returns>Nova instância de <see cref="BaseResult"/>.</returns>
    public static BaseResult InvalidParameters(string text, string property)
    {
        var result = new BaseResult();
        result.AddMessage(Message.InvalidParameters(text, property));
        return result;
    }

    /// <summary>
    /// Cria um novo resultado com código de erro de <see cref="ResultErrorCodes.Validation"/>.
    /// </summary>
    /// <param name="text">Texto da mensagem.</param>
    /// <param name="property">Propriedade relacionada a mensagem, nome da propriedade validada.</param>
    /// <param name="ex">Exception de validação.</param>
    /// <returns>Nova instância de <see cref="BaseResult"/>.</returns>
    public static BaseResult ValidationError(string text, string? property = null, Exception? ex = null)
    {
        var result = new BaseResult();
        result.AddMessage(Message.ValidationError(text, property, ex));
        return result;
    }

    /// <summary>
    /// Cria um novo resultado com código de erro de <see cref="ResultErrorCodes.ApplicationError"/>
    /// </summary>
    /// <param name="ex">Exception ocorrida.</param>
    /// <param name="text">Texto da mensagem, optional, quando não informado será a mensagem da exception.</param>
    /// <returns>Nova instância de <see cref="BaseResult"/>.</returns>
    public static BaseResult ApplicationError(Exception ex, string? text = null)
    {
        if (ex is null)
            throw new ArgumentNullException(nameof(ex));

        var result = new BaseResult();
        result.AddMessage(Message.ApplicationError(ex, text));
        return result;
    }

    /// <summary>
    /// Junta um outro resultado a este resultado.
    /// </summary>
    /// <param name="other">Outro resultado de serviço.</param>
    public virtual BaseResult Join(IResult other)
    {
        Success = Success && other.Success;
        _messages.AddRange(other.Messages);
        return this;
    }

    /// <summary>
    /// Adiciona uma mensagem, e muda o resultado para falha se o tipo dela for de erro.
    /// </summary>
    /// <param name="message">Mensagem a ser adicionada.</param>
    public void AddMessage(IMessage message)
    {
        Success = Success && message.Type != MessageType.Error;
        _messages.Add(message);
    }

    /// <summary>
    /// Adiciona uma mensagem pelas propriedades dela, e muda o resultado para falha se o tipo dela for de erro.
    /// </summary>
    /// <param name="type">Tipo de mensagem.</param>
    /// <param name="text">Texto da mensagem.</param>
    /// <param name="property">Propriedade da mensagem.</param>
    /// <param name="code">Código da mensagem.</param>
    /// <param name="ex">Exception que gerou a mensagem.</param>
    public void AddMessage(MessageType type, string text, string? property, string? code, Exception? ex)
    {
        AddMessage(new Message(type, text, property, code, ex));
    }

    /// <summary>
    /// Adiciona uma mensagem de sucesso.
    /// </summary>
    /// <param name="text">Texto da mensagem.</param>
    /// <param name="property">Propriedade da mensagem.</param>
    /// <param name="code">Código da mensagem.</param>
    public void AddSuccess(string text, string? property = null, string? code = null)
    {
        AddMessage(MessageType.Success, text, property, code, null);
    }

    private static class Immutable
    {
        /// <summary>
        /// <para>
        ///     Implementação immutável abstrata de <see cref="IResult"/>.
        /// </para>
        /// <para>
        ///     Veja <see cref="ImmutableSuccess"/> e <see cref="ImmutableFailure"/>.
        /// </para>
        /// </summary>
        public abstract class ImmutableResult : IResult
        {
            /// <summary>
            /// Sempre a mesma instância de um array de zero posições.
            /// </summary>
            public IEnumerable<IMessage> Messages => Array.Empty<IMessage>();

            /// <summary>
            /// Abstrato.
            /// </summary>
            public abstract bool Success { get; }
        }

        /// <summary>
        /// <para>
        ///     Implementação immutável de <see cref="IResult"/> onde sempre será sucesso.
        /// </para>
        /// <para>
        ///     Usável através do <see cref="BaseResult.ImmutableSuccess"/>.
        /// </para>
        /// </summary>
        public class ImmutableSuccess : ImmutableResult
        {
            /// <summary>
            /// Sempre true.
            /// </summary>
            public override bool Success => true;
        }

        /// <summary>
        /// <para>
        ///     Implementação immutável de <see cref="IResult"/> onde sempre será falha.
        /// </para>
        /// <para>
        ///     Usável através do <see cref="BaseResult.ImmutableFailure"/>.
        /// </para>
        /// </summary>
        public class ImmutableFailure : ImmutableResult
        {
            /// <summary>
            /// Sempre true.
            /// </summary>
            public override bool Success => false;
        }
    }
}
