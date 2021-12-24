namespace HexaSamples.SeedWork.Application.Persistence;

/// <summary>
/// Uma transação com o banco de dados.
/// </summary>
public interface ITransaction
{
    /// <summary>
    /// Commits all changes made to the database in the current transaction.
    /// </summary>
    void Commit();

    /// <summary>
    /// Discards all changes made to the database in the current transaction.
    /// </summary>
    void Rollback();
}