namespace TA.CQRS.Query
{
    using System.Collections.Generic;

    using MediatR;

    using Microsoft.Extensions.Logging;

    using TA.CQRS;

    public class QueryContext<TQuery> : IRequest<ExecutionResponse>, IQueryContext<TQuery> where TQuery : IQuery
    {
        public QueryContext(TQuery query, ILogger logger, IDictionary<string, object> contextData)
        {
            this.Query = query;
            this.Logger = logger;
            this.ContextData = contextData;
        }

        public ILogger Logger { get; }

        public TQuery Query { get; }

        public IDictionary<string, object> ContextData { get; }
    }
}
