namespace HexaSamples.SeedWork.Results;

public static class ResultsExtensions
{
    /// <summary>
    /// Garante que o resultado é sucesso, caso contrário dispara uma <see cref="InvalidOperationException"/>.
    /// </summary>
    /// <param name="result">Resultado da operação.</param>
    /// <returns>A mesma instância de <paramref name="result"/>.</returns>
    /// <exception cref="InvalidOperationException">Caso o resultado não seja sucesso.</exception>
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
    /// Garante que o resultado é sucesso, caso contrário dispara uma <see cref="InvalidOperationException"/>.
    /// </summary>
    /// <typeparam name="TModel">Tipo do modelo do resultado.</typeparam>
    /// <param name="result">Resultado da operação.</param>
    /// <returns>A mesma instância de <paramref name="result"/>.</returns>
    /// <exception cref="InvalidOperationException">Caso o resultado não seja sucesso.</exception>
    public static IResult<TModel> EnsureSuccess<TModel>(this IResult<TModel> result)
    {
        if (result.Success)
            return result;

        throw new InvalidOperationException(result.Messages.JoinMessages(" - "));
    }

    /// <summary>
    /// <para>
    ///     Cria um novo resultado do tipo <typeparamref name="TModel"/> a partir de um resultado existente.
    /// </para>
    /// <para>
    ///     Este resultado será de falha por não conter o modelo.
    /// </para>
    /// </summary>
    /// <typeparam name="TModel">Tipo do modelo do resultado.</typeparam>
    /// <param name="result">Resultado da operação.</param>
    /// <returns>A mesma instância de <paramref name="result"/>.</returns>
    public static IResult<TModel> Adapt<TModel>(this IResult result)
    {
        return new ValueResult<TModel>(result);
    }

    /// <summary>
    /// <para>
    ///     Cria um novo resultado do tipo <typeparamref name="TModel"/> a partir de um resultado existente.
    /// </para>
    /// </summary>
    /// <typeparam name="TModel">Tipo do modelo do resultado.</typeparam>
    /// <param name="result">Resultado da operação.</param>
    /// <param name="model"></param>
    /// <returns>A mesma instância de <paramref name="result"/>.</returns>
    public static IResult<TModel> Adapt<TModel>(this IResult result, TModel model)
    {
        return new ValueResult<TModel>(model, result);
    }

    /// <summary>
    /// Cria um novo resultado a partir deste, com as mesmas mensagens, adaptando o modelo de dados.
    /// </summary>
    /// <typeparam name="TModel">Tipo de dado do modelo do resultado atual.</typeparam>
    /// <typeparam name="TAdapted">Tipo de dado para adaptar.</typeparam>
    /// <param name="result">Resultado da operação.</param>
    /// <param name="adapter">Adaptador.</param>
    /// <returns>Nova instância de <see cref="TypeResult{TModel}"/>.</returns>
    public static IResult<TAdapted> Adapt<TModel, TAdapted>(this IResult<TModel> result, Func<TModel, TAdapted> adapter)
    {
        if (adapter is null)
            throw new ArgumentNullException(nameof(adapter));

        TAdapted? newModel = result.Value is null ? default : adapter(result.Value);
        var newResult = new ValueResult<TAdapted>(newModel);
        return newResult.Join(result);
    }

    /// <summary>
    /// Adiciona uma mensagem informativa.
    /// </summary>
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
    /// <returns>A mesma instância de <paramref name="result"/> para chamadas encadeadas.</returns>
    public static TResult WithWarning<TResult>(this TResult result, string errorText)
        where TResult : BaseResult
    {
        result.AddWarning(errorText);
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