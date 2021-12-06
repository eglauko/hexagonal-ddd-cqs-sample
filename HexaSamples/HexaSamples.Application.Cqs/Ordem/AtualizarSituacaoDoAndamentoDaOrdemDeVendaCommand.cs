using HexaSamples.Domain.OrdemAggregate;

namespace HexaSamples.Application.Cqs.Ordem;

public class AtualizarSituacaoDoAndamentoDaOrdemDeVendaCommand
{
    public Guid OrdemDeVendaId { get; set; }

    public OrdemSituacao Situacao { get; set; }
}