namespace HexaSamples.SeedWork.Application.Persistence;

/// <summary>
/// <para>
///     Representa uma transação com o banco de dados.
/// </para>
/// <para>
///     Normalmente a unidade de trabalho irá gerenciar uma transação para finalização, porém é possível
///     gerenciar a transação a nível de serviços de aplicação. 
/// </para>
/// </summary>
public interface ITransaction
{
    /// <summary>
    /// <para>
    ///     Commits all changes made to the database in the current transaction.
    /// </para>
    /// </summary>
    void Commit();

    /// <summary>
    /// <para>
    ///     Discards all changes made to the database in the current transaction.
    /// </para>
    /// </summary>
    void Rollback();
}