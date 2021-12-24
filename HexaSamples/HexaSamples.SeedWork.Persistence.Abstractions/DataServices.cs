using HexaSamples.SeedWork.Entities;

namespace HexaSamples.SeedWork.Persistence.Abstractions;

/// <summary>
/// Interface de um serviço de dados.
/// </summary>
public interface IDataService { }

/// <summary>
/// Interface de um serviço de dados para um tipo especificado.
/// </summary>
/// <typeparam name="TEntity">Tipo de dado.</typeparam>
public interface IDataService<TEntity> : IDataService { }

/// <summary>
/// Serviço de dados para adicionar e persistir novas entidades.
/// </summary>
/// <typeparam name="TEntity">Tipo da entidade.</typeparam>
public interface IAdder<TEntity> : IDataService<TEntity>
{
    /// <summary>
    /// Adiciona uma entidade ao repositório para ser persistida.
    /// </summary>
    /// <param name="entity">Instância do modelo de dados.</param>
    /// <returns>Resultado da operação.</returns>
    void Add(TEntity entity);
}

/// <summary>
/// Serviço de dados para buscar uma instância de uma entidade existente.
/// </summary>
/// <typeparam name="TEntity">Tipo da entidade.</typeparam>
public interface IFinder<TEntity> : IDataService<TEntity>
{
    /// <summary>
    /// Procura por uma entidade existente através da chave (Id) dela.
    /// </summary>
    /// <param name="id">Chave, Id, da entidade.</param>
    /// <returns>Instância existente, ou nulo/default caso não exista.</returns>
    TEntity? Find(object id);

    /// <summary>
    /// Procura, de forma assíncrona, por uma entidade existente através da chave (Id) dela.
    /// </summary>
    /// <param name="id">Chave, Id, da entidade.</param>
    /// <param name="token">Token para cancelamento das tasks.</param>
    /// <returns>Instância existente, ou nulo/default caso não exista.</returns>
    ValueTask<TEntity?> FindAsync(object id, CancellationToken token = default);
}

/// <summary>
/// <para>
///     Serviço de dados para buscar uma instância de uma entidade existente a partir de um filtro.
/// </para>
/// <para>
///     Este serviço serve para buscas 'genéricas', podendo ser por Id, Código, Guid, ou outro identificador
///     único.
/// </para>
/// </summary>
/// <typeparam name="TEntity">Tipo da entidade.</typeparam>
/// <typeparam name="TFilter">Tipo do filtro.</typeparam>
public interface IFinder<TEntity, TFilter> : IDataService<TEntity>
{
    /// <summary>
    /// Procura por uma entidade existente através de um filtro, o qual deve ser como uma chave única.
    /// </summary>
    /// <param name="filter">Filtro que identifique a entidade.</param>
    /// <returns>Instância existente, ou nulo/default caso não exista.</returns>
    TEntity? Find(TFilter filter);

    /// <summary>
    /// Procura por uma entidade existente através de um filtro, o qual deve ser como uma chave única.
    /// </summary>
    /// <param name="filter">Filtro que identifique a entidade.</param>
    /// <param name="token">Token para cancelamento das tasks.</param>
    /// <returns>Instância existente, ou nulo/default caso não exista.</returns>
    Task<TEntity?> FindAsync(TFilter filter, CancellationToken token = default);
}

/// <summary>
/// Serviço de dados para buscar uma instância de uma entidade existente através do GUID.
/// </summary>
/// <typeparam name="TEntity">Tipo de dado.</typeparam>
public interface IFinderByGuid<TEntity> : IDataService<TEntity>
    where TEntity : IHasGuid
{
    /// <summary>
    /// Procura por uma entidade existente através do Guid dela.
    /// </summary>
    /// <param name="guid">Chave universal da entidade.</param>
    /// <returns>Instância existente, ou nulo/default caso não exista.</returns>
    TEntity? FindByGuid(Guid guid);

    /// <summary>
    /// Procura, de forma assíncrona, por uma entidade existente através do Guid dela.
    /// </summary>
    /// <param name="guid">Chave universal da entidade.</param>
    /// <param name="token">Token para cancelamento das tasks.</param>
    /// <returns>Instância existente, ou nulo/default caso não exista.</returns>
    Task<TEntity?> FindByGuidAsync(Guid guid, CancellationToken token = default);
}

/// <summary>
/// Serviço de dados para buscar uma instância de uma entidade existente através do Código.
/// </summary>
/// <typeparam name="TEntity">Tipo de dado.</typeparam>
/// <typeparam name="TCode">Tipo do código.</typeparam>
public interface IFinderByCode<TEntity, TCode> : IDataService<TEntity>
    where TEntity : IHasCodigo<TCode>
{
    /// <summary>
    /// Procura por uma entidade existente atravéz do Código dela.
    /// </summary>
    /// <param name="code">Código da entidade.</param>
    /// <returns>Instância existente, ou nulo/default caso não exista.</returns>
    TEntity? FindByCode(TCode code);

    /// <summary>
    /// Procura, de forma assíncrona, por uma entidade existente atravéz do Código dela.
    /// </summary>
    /// <param name="code">Código da entidade.</param>
    /// <param name="token">Token para cancelamento das tasks.</param>
    /// <returns>Instância existente, ou nulo/default caso não exista.</returns>
    Task<TEntity?> FindByCodeAsync(TCode code, CancellationToken token = default);
}

/// <summary>
/// Serviço de dados para mesclar dados a uma entidade existente.
/// </summary>
/// <typeparam name="TEntity">Tipo da entidade.</typeparam>
public interface IUpdater<TEntity> : IDataService<TEntity>
{
    /// <summary>
    /// Operação para mesclar um modelo de dados a entidade existente.
    /// </summary>
    /// <remarks>
    /// <para>
    ///     O modelo de dados deverá ter um id, o qual será utilizado para obter a entidade do banco de dados.
    /// </para>
    /// <para>
    ///     Os campos do modelo de dados deverão ser iguais aos campos da entidade.
    /// </para>
    /// </remarks>
    /// <param name="model">Um modelo de dados com informações para atualizar uma entidade existente.</param>
    /// <typeparam name="TId">Tipo do id.</typeparam>
    /// <returns>O Resultado da operação.</returns>
    bool Merge<TId>(IHasId<TId> model);

    /// <summary>
    /// Operação para mesclar um modelo de dados a entidade existente, de forma assíncrona.
    /// </summary>
    /// <remarks>
    /// <para>
    ///     O modelo de dados deverá ter um id, o qual será utilizado para obter a entidade do banco de dados.
    /// </para>
    /// <para>
    ///     Os campos do modelo de dados deverão ser iguais aos campos da entidade.
    /// </para>
    /// </remarks>
    /// <param name="model">Um modelo de dados com informações para atualizar uma entidade existente.</param>
    /// <param name="token">Token para cancelamento das tasks.</param>
    /// <typeparam name="TId">Tipo do id.</typeparam>
    /// <returns>O Resultado da operação.</returns>
    Task<bool> MergeAsync<TId>(IHasId<TId> model, CancellationToken token = default);
}

/// <summary>
/// Serviço de dados para excluir entidades.
/// </summary>
/// <typeparam name="TEntity">Tipo da entidade.</typeparam>
public interface IRemover<TEntity> : IDataService<TEntity>
{
    /// <summary>
    /// Exclui uma entidade do banco de dados.
    /// </summary>
    /// <param name="entity">Entidade.</param>
    void Remove(TEntity entity);

    /// <summary>
    /// Exclui uma entidade pela a chave dela.
    /// </summary>
    /// <param name="id">Chaves (Id) da entidade a ser excluída.</param>
    /// <returns>A entidade excluída, ou nulo se a entidade não for encontrada.</returns>
    TEntity? Delete(object id);

    /// <summary>
    /// Exclui, de forma assíncrona, uma entidade pela a chave dela.
    /// </summary>
    /// <param name="id">Chaves (Id) da entidade a ser excluída.</param>
    /// <param name="token">Token para cancelamento das tasks.</param>
    /// <returns>A entidade excluída, ou nulo se a entidade não for encontrada.</returns>
    Task<TEntity?> DeleteAsync(object id, CancellationToken token = default);
}

/// <summary>
/// Interface complementar para repositórios que podem gerar o Id da entidade.
/// </summary>
/// <typeparam name="TId">Tipo do Id.</typeparam>
public interface IGenerateId<TId> : IDataService
{
    /// <summary>
    /// Gera o próximo Id para entidade.
    /// </summary>
    /// <returns>Novo Id.</returns>
    TId NextId();
}

/// <summary>
/// Interface complementar para repositório que podem gerar um código para entidade.
/// </summary>
/// <typeparam name="TCode">Tipo de dado do código.</typeparam>
public interface IGenerateCode<TCode> : IDataService
{
    /// <summary>
    /// Gera um novo código para entidade.
    /// </summary>
    /// <returns>Novo código.</returns>
    TCode NextCode();
}
