
namespace HexaSamples.SeedWork.Results;

/// <summary>
/// Implementação do resultado de operações que retorna um valor do tipo <typeparamref name="TValue"/>.
/// </summary>
/// <typeparam name="TValue">Tipo de dado do valor retornado no resultado da operação.</typeparam>
public class ValueResult<TValue> : BaseResult, IResult<TValue>
{
    /// <summary>
    /// Modelo de dados do resultado
    /// </summary>
    public TValue? Value { get; private set; } = default;

    /// <summary>
    /// Construtor privado para métodos static.
    /// </summary>
    private ValueResult() { }

    /// <summary>
    /// Construtor padrão, com resultado em sucesso, até ser adicionada uma mensagem de erro.
    /// </summary>
    /// <param name="model">Modelo de dados do resultado.</param>
    public ValueResult(TValue? model)
    {
        Value = model;
    }

    /// <summary>
    /// <para>
    ///     Cria um <see cref="ValueResult{TValue}"/> a partir de outro resultado (<see cref="IResult"/>).
    /// </para>
    /// <para>
    ///     Este será um resultado de falha, pois não é informado o <typeparamref name="TValue"/>.
    /// </para>
    /// </summary>
    /// <param name="other">Outro resultado.</param>
    public ValueResult(IResult other) : base(other, false) { }

    /// <summary>
    /// <para>
    ///     Cria um <see cref="ValueResult{TValue}"/> a partir de outro resultado (<see cref="IResult"/>).
    /// </para>
    /// <para>
    ///     O sucesso dependerá do outro resultado.
    /// </para>
    /// </summary>
    /// <param name="model">Modelo de dados do resultado.</param>
    /// <param name="other">Outro resultado.</param>
    public ValueResult(TValue model, IResult other) : base(other)
    {
        Value = model;
    }

    /// <summary>
    /// Construtor de falha, com uma mensagem de erro.
    /// </summary>
    /// <param name="model">Modelo de dados do resultado.</param>
    /// <param name="error">Texto da mensagem de erro.</param>
    public ValueResult(TValue model, string error)
    {
        Value = model;
        this.AddError(error);
    }

    /// <summary>
    /// Construtor de falha, com uma mensagem de erro.
    /// </summary>
    /// <param name="error">Texto da mensagem de erro.</param>
    public ValueResult(string error)
    {
        this.AddError(error);
    }

    /// <summary>
    /// Construtor de falha, a partir de um <see cref="Exception"/>.
    /// </summary>
    /// <param name="ex"><see cref="Exception"/> de erro.</param>
    public ValueResult(Exception ex)
    {
        this.AddError(ex);
    }

    /// <summary>
    /// Cria um novo ResultadoOperacao de Sucesso.
    /// </summary>
    /// <param name="model">Modelo do retornado pelo resultado.</param>
    /// <returns>Nova instância.</returns>
    public static ValueResult<TValue> CreateSuccess(TValue model)
    {
        var resultado = new ValueResult<TValue>
        {
            Value = model
        };
        return resultado;
    }

    /// <summary>
    /// Cria um novo ResultadoOperacao de Falha.
    /// </summary>
    /// <param name="model">Modelo do retornado pelo resultado.</param>
    /// <param name="error">Texto da mensagem de erro.</param>
    /// <returns>Nova instância.</returns>
    public static ValueResult<TValue> CreateFailure(TValue model, string error)
    {
        var resultado = new ValueResult<TValue>
        {
            Value = model
        };
        resultado.AddError(error);
        return resultado;
    }

    /// <summary>
    /// Cria um novo ResultadoOperacao de Falha.
    /// </summary>
    /// <param name="model">Modelo do retornado pelo resultado.</param>
    /// <param name="error">Texto da mensagem de erro.</param>
    /// <param name="property">Propriedade da mensagem de erro.</param>
    /// <returns>Nova instância.</returns>
    public static ValueResult<TValue> CreateFailure(TValue model, string error, string property)
    {
        var resultado = new ValueResult<TValue>
        {
            Value = model
        };
        resultado.AddError(error, property);
        return resultado;
    }

    /// <summary>
    /// Cria um novo ResultadoOperacao de Falha.
    /// </summary>
    /// <param name="error">Texto da mensagem de erro.</param>
    /// <returns>Nova instância.</returns>
    public new static ValueResult<TValue> CreateFailure(string error)
    {
        var resultado = new ValueResult<TValue>
        {
            Value = default
        };
        resultado.AddError(error);
        return resultado;
    }

    /// <summary>
    /// Cria um novo ResultadoOperacao de Falha.
    /// </summary>
    /// <param name="ex"><see cref="Exception"/> de erro.</param>
    /// <returns>Nova instância.</returns>
    public new static ValueResult<TValue> CreateFailure(Exception ex)
    {
        var resultado = new ValueResult<TValue>
        {
            Value = default
        };
        resultado.AddError(ex);
        return resultado;
    }

    /// <summary>
    /// Junta um outro resultado a este resultado.
    /// </summary>
    /// <param name="other">Outro resultado de serviço.</param>
    public new ValueResult<TValue> Join(IResult other)
    {
        base.Join(other);
        return this;
    }

    /// <summary>
    /// Cria um novo resultado a partir deste, com as mesmas mensagens, adaptando o modelo de dados.
    /// </summary>
    /// <typeparam name="TAdapted">Tipo de dado para adaptar.</typeparam>
    /// <param name="adapter">Adaptador.</param>
    /// <returns>Nova instância de <see cref="ValueResult{TValue}"/>.</returns>
    public ValueResult<TAdapted> AdaptTo<TAdapted>(Func<TValue, TAdapted> adapter)
    {
        if (adapter is null)
            throw new ArgumentNullException(nameof(adapter));

        TAdapted? newValue = Value == null ? default : adapter(Value);
        var newResult = new ValueResult<TAdapted>(newValue);
        return newResult.Join(this);
    }

    /// <summary>
    /// Cria um novo Base Result com as mesmas mensagens deste resultado, mas sem o modelo.
    /// </summary>
    /// <returns>Nova instância de <see cref="BaseResult"/>.</returns>
    public BaseResult ToBase() => CreateSuccess().Join(this);
}

