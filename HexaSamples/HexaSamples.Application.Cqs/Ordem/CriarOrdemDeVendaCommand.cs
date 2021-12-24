using HexaSamples.Application.Cqs.SeedWork;

namespace HexaSamples.Application.Cqs.Ordem;

public class CriarOrdemDeVendaCommand : CommandBase
{
    public Guid PessoaId { get; set; }

    public Guid LojaId { get; set; }
}