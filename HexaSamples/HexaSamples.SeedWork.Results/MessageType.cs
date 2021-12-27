
namespace HexaSamples.SeedWork.Results;

/// <summary>
/// <para>
///     Enumerador dos tipo de mensagens de um resultado de operação.
/// </para>
/// </summary>
public enum MessageType : int
{
    /// <summary>
    /// Tipo de mensagem de erro, falha, inválidações.
    /// </summary>
    Error = 0,

    /// <summary>
    /// Tipo de mensagem de alerta, aviso.
    /// </summary>
    Warning = 1,

    /// <summary>
    /// Tipo de mensagem informativa.
    /// </summary>
    Info = 2,

    /// <summary>
    /// Tipo de mensagem de sucesso.
    /// </summary>
    Success = 3,
}