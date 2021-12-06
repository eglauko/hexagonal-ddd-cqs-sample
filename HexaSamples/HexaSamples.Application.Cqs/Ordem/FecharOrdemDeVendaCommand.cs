using HexaSamples.Application.Cqs.Commons;

namespace HexaSamples.Application.Cqs.Ordem;

public class FecharOrdemDeVendaCommand : CommandBase
{
    public Guid OrdemDeVendaId { get; set; }
}