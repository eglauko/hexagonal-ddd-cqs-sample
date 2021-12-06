using HexaSamples.Application.Cqs.Commons;
using HexaSamples.Domain.OrdemAggregate;

namespace HexaSamples.Application.Cqs.Ordem;

public class AtualizarSituacaoDoAndamentoDaOrdemDeVendaCommand: CommandBase
{
    public Guid OrdemDeVendaId { get; set; }

    public OrdemSituacao Situacao { get; set; }
}