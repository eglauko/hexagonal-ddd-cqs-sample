using HexaSamples.Application.Cqs.SeedWork;

namespace HexaSamples.Application.Cqs.Ordem;

public class RemoverQuantidadeDoProdutoCommand : CommandBase
{
    public Guid OrdemDeVendaId { get; set; }
    
    public Guid ProdutoId { get; set; }

    public int Quantidade { get; set; }
}