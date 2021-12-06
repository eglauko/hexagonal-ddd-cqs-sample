namespace HexaSamples.Application.Cqs.Ordem;

public class CorrigirLojaEtClienteCommand
{
    public Guid OrdemDeVendaId { get; set; }
    
    public Guid PessoaId { get; set; }

    public Guid LojaId { get; set; }
}