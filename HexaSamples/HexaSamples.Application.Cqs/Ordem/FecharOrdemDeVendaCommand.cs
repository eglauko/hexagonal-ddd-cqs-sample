using HexaSamples.Application.Cqs.SeedWork;

namespace HexaSamples.Application.Cqs.Ordem;

public class FecharOrdemDeVendaCommand : CommandBase
{
    public Guid OrdemDeVendaId { get; set; }
}