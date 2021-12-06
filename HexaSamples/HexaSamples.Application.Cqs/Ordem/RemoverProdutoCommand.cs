using HexaSamples.Application.Cqs.Commons;

namespace HexaSamples.Application.Cqs.Ordem;

public class RemoverProdutoCommand : CommandBase
{
    public Guid OrdemDeVendaId { get; set; }
    
    public Guid ProdutoId { get; set; }
}