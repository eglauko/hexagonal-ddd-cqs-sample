using HexaSamples.Application.Cqs.Ordem;
using HexaSamples.SeedWork.Application.Persistence;
using HexaSamples.SeedWork.Persistence.Abstractions;
using HexaSamples.SeedWork.Results;
using HexaSamples.Domain.OrdemAggregate;
using HexaSamples.Domain.SupportEntities;
using MediatR;

// ReSharper disable HeapView.BoxingAllocation

namespace HexaSamples.Application.Handlers.Ordem;

public class CriarOrdemDeVendaHandler : IRequestHandler<CriarOrdemDeVendaCommand, IResult>
{
    private readonly IRepository<OrdemDeVenda> _ordemRepository;
    private readonly IFinder<Pessoa> _produtoFinder;
    private readonly IFinder<Loja> _lojaFinder;
    private readonly IUnitOfWorkContext _context;

    public CriarOrdemDeVendaHandler(
        IRepository<OrdemDeVenda> ordemRepository,
        IFinder<Pessoa> produtoFinder,
        IFinder<Loja> lojaFinder,
        IUnitOfWorkContext context)
    {
        _ordemRepository = ordemRepository;
        _produtoFinder = produtoFinder;
        _lojaFinder = lojaFinder;
        _context = context;
    }

    public async Task<IResult> Handle(CriarOrdemDeVendaCommand command, CancellationToken cancellationToken)
    {
        BaseResult result = new();
        
        var pessoa  = await _produtoFinder.FindAsync(command.PessoaId, cancellationToken);
        if (pessoa is null)
            result.AddError("Produto não encontado");

        var loja = await _lojaFinder.FindAsync(command.LojaId, cancellationToken);
        if (loja is null)
            result.AddError("Loja não encontrada");

        if (!result.Success)
            return result;

        var ordem = new OrdemDeVenda(loja!, pessoa!);
        
        _ordemRepository.Add(ordem);

        return await _context.SaveAsync(cancellationToken);
    }
}