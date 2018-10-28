namespace TA.CQRS.Query
{
    using MediatR;

    using TA.CQRS.Query;

    public interface IQueryHandler<TQuery> : IPipelineBehavior<QueryContext<TQuery>, ExecutionResponse>
        where TQuery : IQuery
    { }
}