namespace HexaSamples.Commons.Persistence.Abstractions;

/// <summary>
/// Repositório completo para entidades, podendo criar, adicionar, buscar, modificar e remover uma entidade.
/// Por esse conjunto de operações é chamado de Repositório, um domínio os serviços de acesso a dados.
/// </summary>
/// <typeparam name="T">Tipo de dado da entidade.</typeparam>
public interface IRepository<T> : IAdder<T>, IFinder<T>, IUpdater<T>, IRemover<T> { }