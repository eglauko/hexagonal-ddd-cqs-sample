using HexaSamples.Commons.Entities;
using HexaSamples.Commons.Results;
using HexaSamples.Domain.SupportEntities;

#pragma warning disable CS8618

namespace HexaSamples.Domain.OrdemAggregate;

public class OrdemDeVenda : AggregateRoot<Guid>
{
    private ICollection<ItemVenda>? _itens;

    public OrdemDeVenda(Loja loja, Pessoa cliente)
    {
        Loja = loja ?? throw new ArgumentNullException(nameof(loja));
        Cliente = cliente ?? throw new ArgumentNullException(nameof(cliente));
        DataCriacao = DateTimeOffset.Now;
        Situacao = OrdemSituacao.EmEdicao;
    }
        
    /// <summary>
    /// Construtor for deserializers.
    /// </summary>
    protected OrdemDeVenda() { }

    public virtual Loja Loja { get; private set; }

    public virtual Pessoa Cliente { get; private set; }

    public DateTimeOffset DataCriacao { get; }

    public OrdemSituacao Situacao { get; private set; }

    public virtual IEnumerable<ItemVenda> Itens => _itens ??= new List<ItemVenda>();

    private ICollection<ItemVenda> InternalItens
    {
        get
        {
            if (_itens is null) 
                _ = Itens;
            return _itens!;
        }
    }

    public IResult CorrigirLojaEtCliente(Loja loja, Pessoa cliente)
    {
        var guard = GuardSituacaoParaAlterar();
        if (!guard.Success)
            return guard;

        var alterouLoja = Loja.Id != loja.Id;
            
        Loja = loja;
        Cliente = cliente;

            
        var result = alterouLoja 
            ? AtualizarPrecosProdutos()
            : BaseResult.ImmutableSuccess;
            
        return result.Success 
            ? result
            : BaseResult.CreateFailure("Não é possível atualizar o ordem de venda para está loja").Join(result);
    }

    private IResult AtualizarPrecosProdutos()
    {
        foreach (var item in InternalItens)
        {
            item.AtualizarPrecoVenda(Loja);
        }

        return InternalItens.Any(item => item.ValorUnitario == 0)
            ? BaseResult.ValidationError("Há produto(s) sem preço de venda para está Loja")
            : BaseResult.ImmutableSuccess;
    }

    public IResult AdicionarProduto(Produto produto, int quantidade = 1)
    {
        var guard = GuardSituacaoParaAlterar();
        if (!guard.Success)
            return guard;
            
        if (quantidade <= 0)
            return BaseResult.InvalidParameters("Quantidade deve ser maior que zero (0)", nameof(quantidade));

        var item = InternalItens.FirstOrDefault(i => i.Produto.Id == produto.Id);
        if (item is not null)
        {
            item.AdicionarQuantidade(quantidade);
        }
        else
        {
            item = new ItemVenda(Loja, produto, quantidade);
            if (item.ValorUnitario == 0)
                return BaseResult.ValidationError("Produto sem preço de venda!");

            InternalItens.Add(item);
        }

        return BaseResult.ImmutableFailure;
    }

    public IResult RemoverQuantidadeDoProduto(Guid produtoId, int quantidade = 1)
    {
        var guard = GuardSituacaoParaAlterar();
        if (!guard.Success)
            return guard;
            
        if (quantidade <= 0)
            return BaseResult.InvalidParameters("Quantidade deve ser maior que zero (0)", nameof(quantidade));
            
        var item = InternalItens.FirstOrDefault(i => i.Produto.Id == produtoId);
        if (item is null)
            return BaseResult.InvalidParameters("Produto não encontrado na ordem de venda", nameof(produtoId));
            
        item.RemoverQuantidade(quantidade);

        if (item.Quantidade <= 0)
            InternalItens.Remove(item);
            
        return BaseResult.ImmutableSuccess;
    }

    public IResult RemoverProduto(Guid produtoId)
    {
        var guard = GuardSituacaoParaAlterar();
        if (!guard.Success)
            return guard;
            
        var item = InternalItens.FirstOrDefault(i => i.Produto.Id == produtoId);
        if (item is null)
            return BaseResult.InvalidParameters("Produto não encontrado na ordem de venda", nameof(produtoId));

        InternalItens.Remove(item);
            
        return  BaseResult.ImmutableSuccess;
    }

    public IResult Fechar()
    {
        if (Situacao is not OrdemSituacao.EmEdicao)
            return BaseResult.ValidationError(
                $"Não é possível fechar a ordem de venda, a situação '{Situacao}' é inválida.");
            
        Situacao = OrdemSituacao.Fechada;
        return BaseResult.ImmutableSuccess;
    }

    /// <summary>
    /// <para>
    ///     Uma ordem de venda poderá ser cancelada enquanto não for despachada ao cliente.
    /// </para>
    /// <para>
    ///     Se o cliente devolver a compra, o ordem de venda não será alterada, mas será criado uma devolução
    ///     para o cliente.
    /// </para>
    /// <para>
    ///     Se o cliente não receber a encomenta, e a transportadora retornar o produto, a ordem será movida
    ///     para retornado, e um incidente será aberto. Depois disto a ordem pode ser cancelada ou re-enviada.
    /// </para>
    /// </summary>
    /// <returns>
    ///     Resultado da operação.
    /// </returns>
    public IResult Cancelar()
    {
        if (Situacao is OrdemSituacao.Despachada 
            or OrdemSituacao.Entregue
            or OrdemSituacao.Finalizada
            or OrdemSituacao.Cancelada)
            return BaseResult.ValidationError(
                $"Não é possível cancelar a ordem de venda, a situação '{Situacao}' é inválida.");
            
        Situacao = OrdemSituacao.Cancelada;
        return BaseResult.ImmutableSuccess;
    }

        
    public IResult AtualizarSituacaoDoAndamento(OrdemSituacao situacao)
    {
        if (Situacao is OrdemSituacao.EmEdicao 
            or OrdemSituacao.Finalizada
            or OrdemSituacao.Cancelada)
            return BaseResult.ValidationError(
                $"Não é possível atualizar o andamento da ordem de venda, a situação atual '{Situacao}' é inválida.");
            
        if (situacao is OrdemSituacao.EmEdicao
            or OrdemSituacao.Cancelada
            or OrdemSituacao.Fechada)
            return BaseResult.InvalidParameters(
                "A situação informada não é permitida", nameof(situacao));

        Situacao = situacao;
        return BaseResult.ImmutableSuccess;
    }

    private IResult GuardSituacaoParaAlterar()
    {
        return Situacao is not OrdemSituacao.EmEdicao
            ? BaseResult.ValidationError(
                $"Não é possível alterar a ordem de venda na situação atual ({Situacao}).")
            : BaseResult.ImmutableSuccess;
    }
}