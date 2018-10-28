namespace TA.CQRS.Query
{
    using System.Collections.Generic;

    using MediatR;

    using Microsoft.Extensions.Logging;

    using TA.CQRS;

    public class QueryContext<TQuery> : IRequest<ExecutionResponse>, IQueryContext<TQuery> where TQuery : IQuery
    {
        public QueryContext(TQuery query, ILogger logger, IDefaultResponseBuilder defaultResponse)
        {
            this.Query = query;
            this.DefaultResponse = defaultResponse;
            this.Logger = logger;
        }

        public ILogger Logger { get; }

        public TQuery Query { get; }

        public IDefaultResponseBuilder DefaultResponse  { get; }

        public IDictionary<string, object> ContextData { get; }
    }
}
