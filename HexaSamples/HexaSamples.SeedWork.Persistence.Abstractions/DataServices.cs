using HexaSamples.SeedWork.Entities;

namespace HexaSamples.SeedWork.Persistence.Abstractions;

/// <summary>
/// <para>
///     Serviço de dados para adicionar e persistir novas entidades.
/// </para>
/// </summary>
/// <remarks>
/// <para>
///     Quando implementado junto com o padrão Unit Of Work a entidade não será persistida diretamente ao usar
///     os métodos deste serviço.
/// </para>
/// </remarks>
/// <typeparam name="TEntity">Tipo da entidade.</typeparam>
public interface IAdder<TEntity>
{
    /// <summary>
    /// <para>
    ///     Adiciona uma entidade ao repositório para ser persistida.
    /// </para>
    /// </summary>
    /// <remarks>
    /// <para>
    ///     Quando implementado junto com o padrão Unit Of Work a entidade não será persistida diretamente neste método,
    ///     ela será armazenada em memória até a finalização da unidade de trabalho.
    /// </para>
    /// </remarks>
    /// <param name="entity">Instância do modelo de dados.</param>
    void Add(TEntity entity);
}

/// <summary>
/// <para>
///     Serviço de dados para buscar uma instância de uma entidade existente.
/// </para>
/// </summary>
/// <typeparam name="TEntity">Tipo da entidade.</typeparam>
public interface IFinder<TEntity>
{
    /// <summary>
    /// <para>
    ///     Procura por uma entidade existente através da chave (Id) dela.
    /// </para>
    /// </summary>
    /// <param name="id">Chave, Id, da entidade.</param>
    /// <returns>
    /// <para>
    ///     Instância existente, ou nulo/default caso não exista.
    /// </para>
    /// </returns>
    TEntity? Find(object id);

    /// <summary>
    /// <para>
    ///     Procura, de forma assíncrona, por uma entidade existente através da chave (Id) dela.
    /// </para>
    /// </summary>
    /// <param name="id">Chave, Id, da entidade.</param>
    /// <param name="token">Token para cancelamento das tasks.</param>
    /// <returns>
    /// <para>
    ///     Instância existente, ou nulo/default caso não exista.
    /// </para>
    /// </returns>
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
/// <typeparam name="TIdentifierFilter">Tipo do filtro identificador.</typeparam>
public interface IFinder<TEntity, in TIdentifierFilter>
{
    /// <summary>
    /// <para>
    ///     Procura por uma entidade existente através de um filtro identificador, o qual deve ser como uma chave única.
    /// </para>
    /// </summary>
    /// <param name="filter">Filtro que identifique a entidade.</param>
    /// <returns>
    /// <para>
    ///     Instância existente, ou nulo/default caso não exista.
    /// </para>
    /// </returns>
    TEntity? Find(TIdentifierFilter filter);

    /// <summary>
    /// <para>
    ///     Procura por uma entidade existente através de um filtro identificador, o qual deve ser como uma chave única.
    /// </para>
    /// </summary>
    /// <param name="filter">Filtro que identifique a entidade.</param>
    /// <param name="token">Token para cancelamento das tasks.</param>
    /// <returns>
    /// <para>
    ///     Instância existente, ou nulo/default caso não exista.
    /// </para>
    /// </returns>
    Task<TEntity?> FindAsync(TIdentifierFilter filter, CancellationToken token = default);
}

/// <summary>
/// <para>
///     Serviço de dados para buscar uma instância de uma entidade existente através do GUID.
/// </para>
/// </summary>
/// <remarks>
/// <para>
///     Este serviço serve a entidades cuja o ID não seja do tipo GUID e a entidade possui uma propriedade
///     adicional para o GUID, definida pela interface <see cref="IHasGuid"/>. 
/// </para>
/// </remarks>
/// <typeparam name="TEntity">Tipo de dado.</typeparam>
public interface IFinderByGuid<TEntity>
    where TEntity : IHasGuid
{
    /// <summary>
    /// <para>
    ///     Procura por uma entidade existente através do Guid dela.
    /// </para>
    /// </summary>
    /// <param name="guid">Chave universal da entidade.</param>
    /// <returns>
    /// <para>
    ///     Instância existente, ou nulo/default caso não exista.
    /// </para>
    /// </returns>
    TEntity? FindByGuid(Guid guid);

    /// <summary>
    /// <para>
    ///     Procura, de forma assíncrona, por uma entidade existente através do Guid dela.
    /// </para>
    /// </summary>
    /// <param name="guid">Chave universal da entidade.</param>
    /// <param name="token">Token para cancelamento das tasks.</param>
    /// <returns>
    /// <para>
    ///     Instância existente, ou nulo/default caso não exista.
    /// </para>
    /// </returns>
    Task<TEntity?> FindByGuidAsync(Guid guid, CancellationToken token = default);
}

/// <summary>
/// <para>
///     Serviço de dados para buscar uma instância de uma entidade existente através do Código.
/// </para>
/// </summary>
/// <remarks>
/// <para>
///     O código não é o ID da entidade, mas um identificador único, normalmente mais amigável aos humanos.
/// </para>
/// <para>
///     A propriedade de código é definida pela interface <see cref="IHasCodigo{TCode}"/>.
/// </para>
/// </remarks>
/// <typeparam name="TEntity">Tipo de dado.</typeparam>
/// <typeparam name="TCode">Tipo do código.</typeparam>
public interface IFinderByCode<TEntity, TCode>
    where TEntity : IHasCodigo<TCode>
{
    /// <summary>
    /// <para>
    ///     Procura por uma entidade existente atravéz do Código dela.
    /// </para>
    /// </summary>
    /// <param name="code">Código da entidade.</param>
    /// <returns>
    /// <para>
    ///     Instância existente, ou nulo/default caso não exista.
    /// </para>
    /// </returns>
    TEntity? FindByCode(TCode code);

    /// <summary>
    /// Procura, de forma assíncrona, por uma entidade existente atravéz do Código dela.
    /// </summary>
    /// <param name="code">Código da entidade.</param>
    /// <param name="token">Token para cancelamento das tasks.</param>
    /// <returns>
    /// <para>
    ///     Instância existente, ou nulo/default caso não exista.
    /// </para>
    /// </returns>
    Task<TEntity?> FindByCodeAsync(TCode code, CancellationToken token = default);
}

/// <summary>
/// <para>
///     Serviço de dados para mesclar dados a uma entidade existente.
/// </para>
/// </summary>
/// <remarks>
/// <para>
///     Quando implementado junto com o padrão Unit Of Work a entidade não será persistida diretamente ao usar
///     os métodos deste serviço.
/// </para>
/// </remarks>
/// <typeparam name="TEntity">Tipo da entidade.</typeparam>
public interface IUpdater<TEntity>
{
    /// <summary>
    /// <para>
    ///     Operação para mesclar um modelo de dados a entidade existente.
    /// </para>
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
    /// <returns>
    /// <para>
    ///     Verdadeiro caso a entidade exista e foi atualizada, falso caso contrário.
    /// </para>
    /// </returns>
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
    /// <returns>
    /// <para>
    ///     Verdadeiro caso a entidade exista e foi atualizada, falso caso contrário.
    /// </para>
    /// </returns>
    Task<bool> MergeAsync<TId>(IHasId<TId> model, CancellationToken token = default);
}

/// <summary>
/// <para>
///     Serviço de dados para excluir entidades.
/// </para>
/// </summary>
/// <remarks>
/// <para>
///     Quando implementado junto com o padrão Unit Of Work a remoção da entidade não será persistida diretamente ao usar
///     os métodos deste serviço.
/// </para>
/// </remarks>
/// <typeparam name="TEntity">Tipo da entidade.</typeparam>
public interface IRemover<TEntity>
{
    /// <summary>
    /// <para>
    ///     Exclui uma entidade do banco de dados.
    /// </para>
    /// </summary>
    /// <remarks>
    /// <para>
    ///     Entende-se que a entidade foi previamente obtida do banco de dados e ela existe.
    /// </para>
    /// </remarks>
    /// <param name="entity">A entidade.</param>
    void Remove(TEntity entity);

    /// <summary>
    /// <para>
    ///     Exclui uma entidade pela a chave dela.
    /// </para>
    /// </summary>
    /// <param name="id">Chaves (Id) da entidade a ser excluída.</param>
    /// <returns>
    /// <para>
    ///     A entidade excluída, ou nulo se a entidade não for encontrada.
    /// </para>
    /// </returns>
    TEntity? Delete(object id);

    /// <summary>
    /// <para>
    ///     Exclui, de forma assíncrona, uma entidade pela a chave dela.
    /// </para>
    /// </summary>
    /// <param name="id">Chaves (Id) da entidade a ser excluída.</param>
    /// <param name="token">Token para cancelamento das tasks.</param>
    /// <returns>
    /// <para>
    ///     A entidade excluída, ou nulo se a entidade não for encontrada.
    /// </para>
    /// </returns>
    Task<TEntity?> DeleteAsync(object id, CancellationToken token = default);
}

/// <summary>
/// <para>
///     Interface complementar para repositórios que podem gerar o Id da entidade.
/// </para>
/// </summary>
/// <typeparam name="TId">Tipo do Id.</typeparam>
public interface IGenerateId<TId>
{
    /// <summary>
    /// <para>
    ///     Gera o próximo Id para entidade.
    /// </para>
    /// </summary>
    /// <returns>
    /// <para>
    ///     O novo Id.
    /// </para>
    /// </returns>
    TId NextId();
}

/// <summary>
/// <para>
///     Interface complementar para repositórios que podem gerar um código para entidade.
/// </para>
/// </summary>
/// <typeparam name="TCode">Tipo de dado do código.</typeparam>
public interface IGenerateCode<TCode>
{
    /// <summary>
    /// <para>
    ///     Gera um novo código para entidade.
    /// </para>
    /// </summary>
    /// <returns>
    /// <para>
    ///     O novo código.
    /// </para>
    /// </returns>
    TCode NextCode();
}
