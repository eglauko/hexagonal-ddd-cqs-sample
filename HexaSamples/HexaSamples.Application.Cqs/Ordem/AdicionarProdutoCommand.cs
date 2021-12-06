namespace HexaSamples.Application.Cqs.Ordem;

public class AdicionarProdutoCommand
{
    public Guid OrdemDeVendaId { get; set; }
    
    public Guid ProdutoId { get; set; }

    public int Quantidade { get; set; }
}