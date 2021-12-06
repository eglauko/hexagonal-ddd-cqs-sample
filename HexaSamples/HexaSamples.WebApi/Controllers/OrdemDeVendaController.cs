using HexaSamples.Application.Cqs.Ordem;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using IResult = HexaSamples.Commons.Results.IResult;

namespace HexaSamples.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class OrdemDeVendaController : Controller
{
    private readonly IMediator _mediator;
    private readonly ILogger _logger;

    public OrdemDeVendaController(IMediator mediator, ILogger<OrdemDeVendaController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpPost]
    public async Task<IResult> Add(CriarOrdemDeVendaCommand model, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _mediator.Send(model, cancellationToken);
            
            _logger.LogDebug("Criar ordem de venda, resultado: {0}", result);
            
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao criar ordem de venda");
            throw;
        }
    }
}