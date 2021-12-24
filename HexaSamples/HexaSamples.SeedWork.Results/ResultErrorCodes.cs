
namespace HexaSamples.SeedWork.Results;

/// <summary>
/// Código de erros padrão.
/// </summary>
public static class ResultErrorCodes
{
    /// <summary>
    /// Erros onde os parâmetros informados são inválidos.
    /// </summary>
    public const string InvalidParameters = "400.1";

    /// <summary>
    /// Erros de validação.
    /// </summary>
    public const string Validation = "400.2";

    /// <summary>
    /// Erros de alguma entidade/registro não encontrado.
    /// </summary>
    public const string NotFound = "404.1";

    /// <summary>
    /// Erro de aplicativo, exception, da qual não é erro de validação.
    /// </summary>
    public const string ApplicationError = "500.1";
}