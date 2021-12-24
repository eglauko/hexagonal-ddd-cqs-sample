namespace HexaSamples.SeedWork.Persistence.Abstractions;

/// <summary>
/// <para>
///     Repositório completo para entidades, podendo criar, adicionar, buscar, modificar e remover uma entidade.
/// </para>
/// <para>
///     Por esse conjunto de operações é chamado de Repositório.
/// </para>
/// <para>
///     Normalmente as implementações de repositório são feitas em conjunto com o padrão Unit Of Work,
///     onde o repositório mantém as entidades em memória durante a unidade de trabalho e as alterações são
///     aplicadas contra a base de dados na finalização da unidade de trabalho.
/// </para>
/// </summary>
/// <typeparam name="T">Tipo de dado da entidade.</typeparam>
public interface IRepository<T> : IAdder<T>, IFinder<T>, IUpdater<T>, IRemover<T> { }