
namespace HexaSamples.SeedWork.Results;

/// <summary>
/// Interface da mensagem dos resultados de operações de um serviço.
/// </summary>
public interface IMessage
{
    /// <summary>
    /// Tipo da mensagem.
    /// </summary>
    MessageType Type { get; }

    /// <summary>
    /// Propriedade que deu origem a mensagem,
    /// ou propriedade que se destina a mensagem.
    /// Quando não vinculada a alguma propriedade, informar uma string em branco.
    /// </summary>
    string? Property { get; }

    /// <summary>
    /// Texto da mensagem a ser exibida.
    /// </summary>
    string Text { get; }

    /// <summary>
    /// Código relacionado a mensagem.
    /// </summary>
    string? Code { get; }

    /// <summary>
    /// Exception relacionada a mensagem.
    /// </summary>
    MessageException? Exception { get; }
}
