using HexaSamples.SeedWork.Results;
using MediatR;

namespace HexaSamples.Application.Cqs.Commons;

public abstract class CommandBase : IRequest<IResult>
{
    /// <summary>
    /// The command Id.
    /// </summary>
    public Guid Id { get; set; }
}

public abstract class CommandBase<TResultValue> : IRequest<IResult<TResultValue>>
{
    /// <summary>
    /// The command Id.
    /// </summary>
    public Guid Id { get; set; }
}