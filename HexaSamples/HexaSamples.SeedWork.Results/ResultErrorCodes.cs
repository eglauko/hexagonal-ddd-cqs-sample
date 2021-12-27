
namespace HexaSamples.SeedWork.Results;

/// <summary>
/// <para>
///     Código de erros padrão.
/// </para>
/// </summary>
public static class ResultErrorCodes
{
    /// <summary>
    /// Erros onde os parâmetros informados são inválidos.
    /// </summary>
    public static string InvalidParameters = "400.1";

    /// <summary>
    /// Erros de validação.
    /// </summary>
    public static string Validation = "400.2";

    /// <summary>
    /// Erros de alguma entidade/registro não encontrado.
    /// </summary>
    public static string NotFound = "404.1";

    /// <summary>
    /// Erro de aplicativo, exception, da qual não é erro de validação.
    /// </summary>
    public static string ApplicationError = "500.1";
}