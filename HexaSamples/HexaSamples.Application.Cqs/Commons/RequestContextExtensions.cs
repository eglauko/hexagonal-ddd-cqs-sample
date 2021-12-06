using HexaSamples.Commons.Results;
using Paramore.Brighter;

namespace HexaSamples.Application.Cqs.Commons;

public static class RequestContextExtensions
{
    public static void AddResult(this IRequestContext context, IResult result)
    {
        context.GetResultBag().Add(result);
    }

    private static ICollection<IResult> GetResultBag(this IRequestContext context)
    {
        if(context.Bag.ContainsKey(nameof(IResult)))
            return (ICollection<IResult>) context.Bag[nameof(IResult)];

        var collection = new LinkedList<IResult>();
        context.Bag.Add(nameof(IResult), collection);

        return collection;
    }
}