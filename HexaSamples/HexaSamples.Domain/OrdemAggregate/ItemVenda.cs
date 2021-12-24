using HexaSamples.SeedWork.Entities;
using HexaSamples.Domain.SupportEntities;

namespace HexaSamples.Domain.OrdemAggregate;

/// <summary>
/// <para>
///     Um item da ordem de venda, relacionada a um único produto.
/// </para>
/// </summary>
/// <remarks>
/// <para>
///     Cada ordem só deverá ter um item para um produto.
/// </para>
/// </remarks>
public class ItemVenda : Entity<long>
{
    internal ItemVenda(Loja loja, Produto produto, int quantidade)
    {
        Produto = produto;
        Quantidade = quantidade;
        ValorUnitario = produto.ObterPrecoVenda(loja);
    }
        
    /// <summary>
    /// Construtor para deserialização.
    /// </summary>
    protected ItemVenda() { }

    public virtual Produto Produto { get; }

    public int Quantidade { get; private set; }

    public decimal ValorUnitario { get; private set; }

    internal void AtualizarPrecoVenda(Loja loja)
        => ValorUnitario = Produto.ObterPrecoVenda(loja);
    
    internal void AdicionarQuantidade(int quantidade)
        => Quantidade += quantidade;

    internal void RemoverQuantidade(int quantidade)
        => Quantidade -= quantidade;
}