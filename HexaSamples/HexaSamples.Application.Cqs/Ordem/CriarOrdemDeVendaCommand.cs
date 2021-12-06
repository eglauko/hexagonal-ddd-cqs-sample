using HexaSamples.Application.Cqs.Commons;

namespace HexaSamples.Application.Cqs.Ordem;

public class CriarOrdemDeVendaCommand : CommandBase
{
    public Guid PessoaId { get; set; }

    public Guid LojaId { get; set; }
}