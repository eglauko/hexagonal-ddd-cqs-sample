namespace HexaSamples.SeedWork.Application.Persistence;

/// <summary>
/// <para>
///     Interface de um contexto de uma unidade de trabalho (Unit of Work).
/// </para>
/// <para>
///     Este componene deve utilizar-se de outros componentes para gerenciar as entidades carregadas em memória
///     durante a unidade de trabalho.
/// </para>
/// <para>
///     O contexto da unidade de trabalho deve relacionar-se, de alguma forma, com os repositórios,
///     os quais são os responsáveis pela leitura e escrita das entidades.
/// </para>
/// <para>
///     No final de uma unidade de trabalho, o contexto aplicará no banco de dados (ou storage) as alterações
///     realizadas nas entidades durante a unidade de trabalho (inclusão, alteração, exclusão).
/// </para>
/// <para>
///     Para bancos de dados que suportam transações, é possível gerenciá-las pelo contexto da unidade de trabalho.
/// </para>
/// </summary>
public interface IUnitOfWorkContext
{
    /// <summary>
    /// <para>
    ///     Salva as alterações nas entidades realizadas pelos serviços durante a unidade de trabalho.
    /// </para>
    /// </summary>
    /// <returns>
    /// <para>
    ///     Resultado das alterações.
    /// </para>
    /// </returns>
    ISaveResult Save();

    /// <summary>
    /// <para>
    ///     Salva, de forma assíncrona,
    ///     as alterações nas entidades realizadas pelos serviços durante a unidade de trabalho.
    /// </para>
    /// </summary>
    /// <param name="token">Token para cancelamento das tasks.</param>
    /// <returns>
    /// <para>
    ///     Resultado das alterações.
    /// </para>
    /// </returns>
    Task<ISaveResult> SaveAsync(CancellationToken token = default);

    /// <summary>
    /// <para>
    ///     Inicia uma transação e retorna um componente para manipulá-la.
    /// </para>
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
    /// <returns>
    /// <para>
    ///     Objeto para manipular a transação.
    /// </para>
    /// </returns>
    /// <exception cref="NotSupportedException">
    /// <para>
    ///     Caso a tecnologia de persistência não superte transactions.
    /// </para>
    /// </exception>
    ITransaction BeginTransaction();

    /// <summary>
    /// <para>
    ///     Inicia uma transação, de forma assíncrona, e retorna um componente para manipulá-la.
    /// </para>
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
    /// <returns>
    /// <para>
    ///     Objeto para manipular a transação.
    /// </para>
    /// </returns>
    /// <exception cref="NotSupportedException">
    /// <para>
    ///     Caso a tecnologia de persistência não superte transactions.
    /// </para>
    /// </exception>
    Task<ITransaction> BeginTransactionAsync(CancellationToken token = default);

    /// <summary>
    /// <para>
    ///     Limpa o contexto, removendo as entidades do tracking, ou componente semelhante que mantém as entidades
    ///     em memória.
    /// </para>
    /// </summary>
    /// <param name="force">
    /// <para>
    ///     Se deverão ser removidas todas entidades, ou apenas as não modificadas.
    /// </para>
    /// <para>
    ///     Verdadeiro remove todas entidades, falso somente as não modificadas.
    /// </para>
    /// </param>
    void CleanUp(bool force = true);
}
