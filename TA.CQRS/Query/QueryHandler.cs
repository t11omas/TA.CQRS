namespace TA.CQRS.Query
{
    using System.Threading;
    using System.Threading.Tasks;

    using MediatR;

    using TA.CQRS.Query;

    public abstract class QueryHandler<TQuery, TResponse> : ExecutionHandlerBase<TResponse>, IQueryHandler<TQuery>
        where TResponse : class
        where TQuery : IQuery
    {
        public abstract Task<ExecutionResponse> Handle(QueryContext<TQuery> queryContext, CancellationToken cancellationToken, RequestHandlerDelegate<ExecutionResponse> next);

    }
}


