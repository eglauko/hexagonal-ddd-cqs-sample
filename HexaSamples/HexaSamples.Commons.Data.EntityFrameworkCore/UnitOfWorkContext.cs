
using HexaSamples.Commons.Application.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.ComponentModel.DataAnnotations;

namespace HexaSamples.Commons.Data.EntityFrameworkCore;

public class UnitOfWorkContext<TDbContext> : IUnitOfWorkContext, ITransaction
    where TDbContext : DbContext
{
    private IDbContextTransaction? dbContextTransaction;

    /// <summary>
    /// Construtor com a sessão para configurar o contexto de persistência e fornecer os serviços.
    /// </summary>
    /// <param name="db">Instância de <see cref="DbContext"/> para realizar as operações.</param>
    public UnitOfWorkContext(TDbContext db)
    {
        Db = db ?? throw new ArgumentNullException(nameof(db));
    }

    /// <summary>
    /// Contexto de EF.
    /// </summary>
    public TDbContext Db { get; }

    /// <summary>
    /// Inicia uma transação e retorna um componente para manipulá-la.
    /// </summary>
    /// <remarks>
    /// <para>
    ///     Este comando não é, normalmente, necessário para a unidade de trabalho.
    /// </para>
    /// <para>
    ///     O Design Pattern Unit of Work é responsável por coletar as alterações nas entidades
    ///     durante o unidade de trabalho, iniciar uma transação e enviar os comandos ao banco de dados e finalizar
    ///     a transação.
    /// </para>
    /// <para>
    ///     Há casos que uma única transação não resolve a unidade de trabalho e é necessário realizar a
    ///     operação em partes. É justificável o uso manual de transações quando é necessário enviar os dados 
    ///     ao banco várias vezes, durante uma mesma transação.
    /// </para>
    /// <para>
    ///     Outras operações, como lock de registras (não recomendado), podem ser feitas através de transações.
    /// </para>
    /// </remarks>
    /// <returns>Objeto para manipular a transação.</returns>
    /// <exception cref="NotSupportedException">
    /// Caso a tecnologia de persistência não superte transactions.
    /// </exception>
    public ITransaction BeginTransaction()
    {
        if (dbContextTransaction is null)
            dbContextTransaction = Db.Database.BeginTransaction();
        return this;
    }

    /// <summary>
    /// Inicia uma transação, de forma assíncrona, e retorna um componente para manipulá-la.
    /// </summary>
    /// <remarks>
    /// <para>
    ///     Este comando não é, normalmente, necessário para a unidade de trabalho.
    /// </para>
    /// <para>
    ///     O Design Pattern Unit of Work é responsável por coletar as alterações nas entidades
    ///     durante o unidade de trabalho, iniciar uma transação e enviar os comandos ao banco de dados e finalizar
    ///     a transação.
    /// </para>
    /// <para>
    ///     Há casos que uma única transação não resolve a unidade de trabalho e é necessário realizar a
    ///     operação em partes. É justificável o uso manual de transações quando é necessário enviar os dados 
    ///     ao banco várias vezes, durante uma mesma transação.
    /// </para>
    /// <para>
    ///     Outras operações, como lock de registras (não recomendado), podem ser feitas através de transações.
    /// </para>
    /// </remarks>
    /// <param name="token">Token para cancelamento das tasks.</param>
    /// <returns>Objeto para manipular a transação.</returns>
    /// <exception cref="NotSupportedException">
    /// Caso a tecnologia de persistência não superte transactions.
    /// </exception>
    public async Task<ITransaction> BeginTransactionAsync(CancellationToken token = default)
    {
        if (dbContextTransaction is null)
            dbContextTransaction = await Db.Database.BeginTransactionAsync(token);
        return this;
    }

    /// <summary>
    /// Limpa o contexto, removendo as entidades do tracking.
    /// </summary>
    /// <param name="force">
    /// <para>
    ///     Se deverão ser removidas todas entidades, ou apenas as não modificadas.
    /// </para>
    /// <para>
    ///     Verdadeiro remove todas entidades, falso somente as não modificadas.
    /// </para>
    /// </param>
    public void CleanUp(bool force = true)
    {
        var entries = Db.ChangeTracker.Entries().Where(e => e.Entity != null);
        if (!force)
            entries = entries.Where(e => e.State == EntityState.Unchanged);
        foreach (var entry in entries)
            entry.State = EntityState.Detached;
    }

    /// <summary>
    /// Commits all changes made to the database in the current transaction.
    /// </summary>
    public void Commit()
    {
        if (dbContextTransaction is null)
            throw new InvalidOperationException("The transaction is not created");

        dbContextTransaction.Commit();

        dbContextTransaction = null;
    }

    /// <summary>
    /// Discards all changes made to the database in the current transaction.
    /// </summary>
    public void Rollback()
    {
        if (dbContextTransaction is null)
            throw new InvalidOperationException("The transaction is not created");

        dbContextTransaction.Rollback();

        dbContextTransaction = null;
    }

    /// <summary>
    /// Salva as alterações nas entidades realizadas pelos serviços.
    /// </summary>
    /// <returns>Resultado das alterações</returns>
    public ISaveResult Save()
    {
        try
        {
            var changes = Db.SaveChanges();
            var result = new SaveResult(changes);

            return result;
        }
        catch (DbUpdateConcurrencyException ex)
        {
            var cex = new ConcurrencyException(ex.Message, ex);

            throw cex;
        }
        catch (DbUpdateException ex)
        {
            return new SaveResult(ex);
        }
    }

    /// <summary>
    /// Salva, de forma assíncrona, as alterações nas entidades realizadas pelos serviços.
    /// </summary>
    /// <param name="token">Token para cancelamento das tasks.</param>
    /// <returns>Resultado das alterações.</returns>
    public async Task<ISaveResult> SaveAsync(CancellationToken token = default)
    {
        try
        {
            var changes = await Db.SaveChangesAsync(token);
            var result = new SaveResult(changes);

            return result;
        }
        catch (DbUpdateConcurrencyException ex)
        {
            var cex = new ConcurrencyException(ex.Message, ex);

            throw cex;
        }
        catch (DbUpdateException ex)
        {
            return new SaveResult(ex);
        }
    }
}

