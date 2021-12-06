using HexaSamples.Application.Cqs.Commons;

namespace HexaSamples.Application.Cqs.Ordem;

public class AdicionarProdutoCommand : CommandBase
{
    public Guid OrdemDeVendaId { get; set; }
    
    public Guid ProdutoId { get; set; }

    public int Quantidade { get; set; }
}