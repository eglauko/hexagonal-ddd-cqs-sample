
namespace HexaSamples.Commons.Results.Exceptions;

/// <summary>
/// Exception para ser criada a partir de <see cref="MessageException"/>.
/// </summary>
public class InvalidOperationInnerException : Exception
{
    /// <summary>
    /// Cria nova exception.
    /// </summary>
    /// <param name="message"></param>
    /// <param name="originalStackTrace"></param>
    /// <param name="fullNameOfOriginalExceptionType"></param>
    public InvalidOperationInnerException(
        string message,
        string originalStackTrace,
        string fullNameOfOriginalExceptionType) : base(message)
    {
        OriginalStackTrace = originalStackTrace;
        FullNameOfOriginalExceptionType = fullNameOfOriginalExceptionType;
    }

    /// <summary>
    /// Cria nova exception.
    /// </summary>
    /// <param name="message"></param>
    /// <param name="innerException"></param>
    /// <param name="originalStackTrace"></param>
    /// <param name="fullNameOfOriginalExceptionType"></param>
    public InvalidOperationInnerException(
        string message,
        string originalStackTrace,
        string fullNameOfOriginalExceptionType,
        Exception innerException) : base(message, innerException)
    {
        OriginalStackTrace = originalStackTrace;
        FullNameOfOriginalExceptionType = fullNameOfOriginalExceptionType;
    }

    /// <summary>
    /// Gets a string representation of the immediate frames on the call stack.
    /// </summary>
    public virtual string OriginalStackTrace { get; private set; }

    /// <summary>
    /// The FullName of the exception type.
    /// </summary>
    public virtual string FullNameOfOriginalExceptionType { get; private set; }
}
