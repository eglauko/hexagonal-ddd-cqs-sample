namespace HexaSamples.Application.Cqs.Ordem;

public class RemoverProdutoCommand
{
    public Guid OrdemDeVendaId { get; set; }
    
    public Guid ProdutoId { get; set; }
}