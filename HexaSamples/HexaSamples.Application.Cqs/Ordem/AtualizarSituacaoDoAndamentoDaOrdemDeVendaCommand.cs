using HexaSamples.Application.Cqs.SeedWork;
using HexaSamples.Domain.OrdemAggregate;

namespace HexaSamples.Application.Cqs.Ordem;

public class AtualizarSituacaoDoAndamentoDaOrdemDeVendaCommand: CommandBase
{
    public Guid OrdemDeVendaId { get; set; }

    public OrdemSituacao Situacao { get; set; }
}