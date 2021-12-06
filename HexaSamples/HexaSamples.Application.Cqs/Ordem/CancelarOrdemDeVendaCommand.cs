using HexaSamples.Application.Cqs.Commons;

namespace HexaSamples.Application.Cqs.Ordem;

public class CancelarOrdemDeVendaCommand: CommandBase
{
    public Guid OrdemDeVendaId { get; set; }
}