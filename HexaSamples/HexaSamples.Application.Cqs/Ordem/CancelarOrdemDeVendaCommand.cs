using HexaSamples.Application.Cqs.SeedWork;

namespace HexaSamples.Application.Cqs.Ordem;

public class CancelarOrdemDeVendaCommand: CommandBase
{
    public Guid OrdemDeVendaId { get; set; }
}