using HexaSamples.Application.Cqs.Commons;

namespace HexaSamples.Application.Cqs.Ordem;

public class CorrigirLojaEtClienteCommand: CommandBase
{
    public Guid OrdemDeVendaId { get; set; }
    
    public Guid PessoaId { get; set; }

    public Guid LojaId { get; set; }
}