using HexaSamples.SeedWork.Entities;

namespace HexaSamples.Domain.SupportEntities;

public class PrecoVenda : Entity<Guid>
{
    public PrecoVenda(Guid id, Loja loja, decimal valor)
    {
        Id = id;
        Loja = loja;
        Valor = valor;
    }
    
    protected PrecoVenda() { }
    
    public Loja Loja { get; }

    public decimal Valor { get; }
}