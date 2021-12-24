using HexaSamples.SeedWork.Entities;

namespace HexaSamples.Domain.SupportEntities;

public class Produto : Entity<Guid, string>
{
    public Produto(Guid id, string codigo,
        string descricao, ICollection<PrecoVenda> precoVenda)
    {
        Id = id;
        Codigo = codigo;
        Descricao = descricao;
        PrecoVenda = precoVenda;
    }

    protected Produto() { }

    public string Descricao { get; }

    public virtual ICollection<PrecoVenda> PrecoVenda { get; }

    internal decimal ObterPrecoVenda(Loja loja)
    {
        var preco = PrecoVenda.FirstOrDefault(p => p.Loja.Id == loja.Id)
                    ?? PrecoVenda.FirstOrDefault();

        return preco?.Valor ?? 0;
    }
}