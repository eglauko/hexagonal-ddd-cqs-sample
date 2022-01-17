namespace HexaSamples.SeedWork.Results;

/// <summary>
/// Extension methods for results and messages.
/// </summary>
public static class ResultsExtensions
{
    /// <summary>
    /// It ensures that the result is success, otherwise it fires a <see cref="InvalidOperationException"/>.
    /// </summary>
    /// <param name="result">The result.</param>
    /// <returns>The same instance of <paramref name="result"/>.</returns>
    /// <exception cref="InvalidOperationException">
    ///     Case the result is not success.
    /// </exception>
    public static IResult EnsureSuccess(this IResult result)
    {
        if (result.Success)
            return result;

        var exceptions = result.Messages.Where(m => m.Type == MessageType.Error)
            .Select(m => m.ToInvalidOperationException())
            .ToList();

        Exception exception = exceptions.Count == 1
            ? exceptions.First()
            : new AggregateException("Multiple exceptions have occurred, check the internal exceptions to see the details.", exceptions);

        throw exception;
    }

    /// <summary>
    /// It ensures that the result is success, otherwise it fires a <see cref="InvalidOperationException"/>.
    /// </summary>
    /// <typeparam name="TValue">The result value type.</typeparam>
    /// <param name="result">The result.</param>
    /// <returns>The same instance of <paramref name="result"/>.</returns>
    /// <exception cref="InvalidOperationException">
    ///     Case the result is not success.
    /// </exception>
    public static IResult<TValue> EnsureSuccess<TValue>(this IResult<TValue> result)
    {
        if (result.Success)
            return result;

        throw new InvalidOperationException(result.Messages.JoinMessages(" - "));
    }

    /// <summary>
    /// <para>
    ///     Creates a new result of type <typeparamref name="TValue"/> from an existing result.
    /// </para>
    /// <para>
    ///     This result will fail because it does not contain the model.
    /// </para>
    /// </summary>
    /// <typeparam name="TValue">The result value type.</typeparam>
    /// <param name="result">The result.</param>
    /// <returns>
    ///     A new instance of <see cref="IResult{TValue}"/>.
    /// </returns>
    public static IResult<TValue> Adapt<TValue>(this IResult result)
    {
        return new ValueResult<TValue>(result);
    }

    /// <summary>
    /// <para>
    ///     Creates a new result of type <typeparamref name="TValue"/> from an existing result.
    /// </para>
    /// </summary>
    /// <typeparam name="TModel">The result value type.</typeparam>
    /// <param name="result">The result.</param>
    /// <param name="value">The operation result value.</param>
    /// <returns>
    ///     A new instance of <see cref="IResult{TValue}"/>.
    /// </returns>
    public static IResult<TModel> Adapt<TModel>(this IResult result, TModel value)
    {
        return new ValueResult<TModel>(value, result);
    }

    /// <summary>
    /// Creates a new result from this one, with the same messages, adapting the data model.
    /// </summary>
    /// <typeparam name="TValue">The type of operation result value.</typeparam>
    /// <typeparam name="TAdaptedValue">The adapted type.</typeparam>
    /// <param name="result">The result.</param>
    /// <param name="adapter">The adapter.</param>
    /// <returns>
    ///     A new instance of <see cref="IResult{TValue}"/>.
    /// </returns>
    public static IResult<TAdaptedValue> Adapt<TValue, TAdaptedValue>(this IResult<TValue> result, Func<TValue, TAdaptedValue> adapter)
    {
        if (adapter is null)
            throw new ArgumentNullException(nameof(adapter));

        TAdaptedValue? newModel = result.Value is null ? default : adapter(result.Value);
        var newResult = new ValueResult<TAdaptedValue>(newModel);
        return newResult.Join(result);
    }

    /// <summary>
    /// Adiciona uma mensagem informativa.
    /// </summary>
    /// <param name="result">Resultado da operação.</param>
    /// <param name="text">Texto da mensagem.</param>
    /// <param name="property">Propriedade da mensagem.</param>
    /// <param name="code">Código da mensagem.</param>
    public static void AddInfo(this BaseResult result, string text, string? property = null, string? code = null)
    {
        result.AddMessage(MessageType.Info, text, property, code, null);
    }

    /// <summary>
    /// Adiciona uma mensagem de alerta.
    /// </summary>
    /// <param name="result">Resultado da operação.</param>
    /// <param name="text">Texto da mensagem.</param>
    /// <param name="property">Propriedade da mensagem.</param>
    /// <param name="code">Código da mensagem.</param>
    public static void AddWarning(this BaseResult result, string text, string? property = null, string? code = null)
    {
        result.AddMessage(MessageType.Warning, text, property, code, null);
    }

    /// <summary>
    /// Adiciona uma mensagem de erro e muda o resultado para falha.
    /// </summary>
    /// <param name="result">Resultado da operação.</param>
    /// <param name="text">Texto da mensagem.</param>
    /// <param name="property">Propriedade da mensagem.</param>
    /// <param name="code">Código da mensagem.</param>
    /// <param name="ex">Exception que gerou a mensagem.</param>
    public static void AddError(this BaseResult result, string text, 
        string? property = null, string? code = null, Exception? ex = null)
    {
        result.AddMessage(MessageType.Error, text, property, code, ex);
    }

    /// <summary>
    /// Adiciona uma mensagem de erro e muda o resultado para falha.
    /// </summary>
    /// <param name="result">Resultado da operação.</param>
    /// <param name="ex">Exception que gerou a mensagem.</param>
    /// <param name="property">Propriedade da mensagem.</param>
    /// <param name="code">Código da mensagem.</param>
    public static void AddError(this BaseResult result, Exception ex, string? property = null, string? code = null)
    {
        result.AddMessage(MessageType.Error, ex.Message, property, code, ex);
    }

    /// <summary>
    /// Adiciona uma mensagem e retorna o mesmo objeto de resultado da operação.
    /// </summary>
    /// <typeparam name="TResult">Tipo do resultado.</typeparam>
    /// <param name="result">Resultado da operação.</param>
    /// <param name="message">A mensagem.</param>
    /// <returns>A mesma instância de <paramref name="result"/> para chamadas encadeadas.</returns>
    public static TResult WithMessage<TResult>(this TResult result, IMessage message)
        where TResult : BaseResult
    {
        result.AddMessage(message);
        return result;
    }

    /// <summary>
    /// Adiciona uma mensagem e retorna o mesmo objeto de resultado da operação.
    /// </summary>
    /// <typeparam name="TResult">Tipo do resultado.</typeparam>
    /// <param name="result">Resultado da operação.</param>
    /// <param name="type"></param>
    /// <param name="text"></param>
    /// <param name="property"></param>
    /// <param name="code"></param>
    /// <param name="ex"></param>
    /// <returns>A mesma instância de <paramref name="result"/> para chamadas encadeadas.</returns>
    public static TResult WithMessage<TResult>(this TResult result,
        MessageType type, string text, string? property = null, string? code = null, Exception? ex = null)
        where TResult : BaseResult
    {
        result.AddMessage(type, text, property, code, ex);
        return result;
    }

    /// <summary>
    /// Adiciona uma mensagem e retorna o mesmo objeto de resultado da operação.
    /// </summary>
    /// <typeparam name="TResult">Tipo do resultado.</typeparam>
    /// <param name="result">Resultado da operação.</param>
    /// <param name="errorText">Mensagem de erro.</param>
    /// <param name="propertyName">Nome da propriedade</param>
    /// <param name="code">Código do erro.</param>
    /// <param name="ex">Exception ocorrida.</param>
    /// <returns>A mesma instância de <paramref name="result"/> para chamadas encadeadas.</returns>
    public static TResult WithError<TResult>(this TResult result, string errorText, 
        string? propertyName = null, string? code = null, Exception? ex = null)
        where TResult : BaseResult
    {
        result.AddError(errorText, propertyName, code, ex);
        return result;
    }

    /// <summary>
    /// Adiciona uma mensagem e retorna o mesmo objeto de resultado da operação.
    /// </summary>
    /// <typeparam name="TResult">Tipo do resultado.</typeparam>
    /// <param name="result">Resultado da operação.</param>
    /// <param name="ex">Exception ocorrida.</param>
    /// <param name="propertyName">Nome da propriedade</param>
    /// <param name="code">Código do erro.</param>
    /// <returns>A mesma instância de <paramref name="result"/> para chamadas encadeadas.</returns>
    public static TResult WithError<TResult>(this TResult result, Exception ex, 
        string? propertyName = null, string? code = null)
        where TResult : BaseResult
    {
        result.AddError(ex, propertyName, code);
        return result;
    }

    /// <summary>
    /// Adiciona uma mensagem e retorna o mesmo objeto de resultado da operação.
    /// </summary>
    /// <typeparam name="TResult">Tipo do resultado.</typeparam>
    /// <param name="result">Resultado da operação.</param>
    /// <param name="text">Mensagem de sucesso.</param>
    /// <param name="propertyName">Nome da propriedade</param>
    /// <returns>A mesma instância de <paramref name="result"/> para chamadas encadeadas.</returns>
    public static TResult WithSuccess<TResult>(this TResult result, string text, 
        string? propertyName = null, string? code = null)
        where TResult : BaseResult
    {
        result.AddSuccess(text, propertyName, code);
        return result;
    }

    /// <summary>
    /// Adiciona uma mensagem e retorna o mesmo objeto de resultado da operação.
    /// </summary>
    /// <typeparam name="TResult">Tipo do resultado.</typeparam>
    /// <param name="result">Resultado da operação.</param>
    /// <param name="infoText">Mensagem de informação.</param>
    /// <param name="propertyName">Nome da propriedade</param>
    /// <param name="code">Código da mensagem.</param>
    /// <returns>A mesma instância de <paramref name="result"/> para chamadas encadeadas.</returns>
    public static TResult WithInfo<TResult>(this TResult result,
        string infoText, string? propertyName = null, string? code = null)
        where TResult : BaseResult
    {
        result.AddInfo(infoText, propertyName, code);
        return result;
    }

    /// <summary>
    /// Adiciona uma mensagem e retorna o mesmo objeto de resultado da operação.
    /// </summary>
    /// <typeparam name="TResult">Tipo do resultado.</typeparam>
    /// <param name="result">Resultado da operação.</param>
    /// <param name="errorText">Mensagem de erro.</param>
    /// <param name="propertyName">Nome da propriedade</param>
    /// <returns>A mesma instância de <paramref name="result"/> para chamadas encadeadas.</returns>
    public static TResult WithWarning<TResult>(this TResult result, string errorText, 
        string? propertyName = null, string? code = null)
        where TResult : BaseResult
    {
        result.AddWarning(errorText, propertyName, code);
        return result;
    }

    /// <summary>
    /// Cria um novo resultado com código de erro de <see cref="ResultErrorCodes.NotFound"/>.
    /// </summary>
    /// <typeparam name="TResult">Tipo do resultado.</typeparam>
    /// <param name="result">Resultado da operação.</param>
    /// <param name="text">Texto da mensagem.</param>
    /// <returns>A mesma instância de <paramref name="result"/> para chamadas encadeadas.</returns>
    public static TResult WithNotFound<TResult>(this TResult result, string text)
        where TResult : BaseResult
    {
        result.AddMessage(Message.NotFound(text));
        return result;
    }

    /// <summary>
    /// Cria um novo resultado com código de erro de <see cref="ResultErrorCodes.InvalidParameters"/>.
    /// </summary>
    /// <typeparam name="TResult">Tipo do resultado.</typeparam>
    /// <param name="result">Resultado da operação.</param>
    /// <param name="text">Texto da mensagem.</param>
    /// <param name="property">Propriedade relacionada a mensagem, nome do parâmetro inválido.</param>
    /// <returns>A mesma instância de <paramref name="result"/> para chamadas encadeadas.</returns>
    public static TResult WithInvalidParameters<TResult>(this TResult result, string text, string property)
        where TResult : BaseResult
    {
        result.AddMessage(Message.InvalidParameters(text, property));
        return result;
    }

    /// <summary>
    /// Cria um novo resultado com código de erro de <see cref="ResultErrorCodes.Validation"/>.
    /// </summary>
    /// <typeparam name="TResult">Tipo do resultado.</typeparam>
    /// <param name="result">Resultado da operação.</param>
    /// <param name="text">Texto da mensagem.</param>
    /// <param name="property">Propriedade relacionada a mensagem, nome da propriedade validada.</param>
    /// <param name="ex">Exception de validação.</param>
    /// <returns>A mesma instância de <paramref name="result"/> para chamadas encadeadas.</returns>
    public static TResult WithValidationError<TResult>(this TResult result, 
        string text, string? property = null, Exception? ex = null)
        where TResult : BaseResult
    {
        result.AddMessage(Message.ValidationError(text, property, ex));
        return result;
    }

    /// <summary>
    /// Cria um novo resultado com código de erro de <see cref="ResultErrorCodes.ApplicationError"/>
    /// </summary>
    /// <typeparam name="TResult">Tipo do resultado.</typeparam>
    /// <param name="result">Resultado da operação.</param>
    /// <param name="ex">Exception ocorrida.</param>
    /// <param name="text">Texto da mensagem, optional, quando não informado será a mensagem da exception.</param>
    /// <returns>A mesma instância de <paramref name="result"/> para chamadas encadeadas.</returns>
    public static TResult WithApplicationError<TResult>(this TResult result, Exception ex, string? text = null)
        where TResult : BaseResult
    {
        if (ex is null)
            throw new ArgumentNullException(nameof(ex));

        result.AddMessage(Message.ApplicationError(ex, text));
        return result;
    }
}