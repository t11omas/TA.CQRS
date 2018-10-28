namespace TA.CQRS.Query
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using MediatR;

    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;

    using TA.CQRS;

    internal class QueryInvoker : IQueryInvoker
    {
        private readonly IMediator mediator;

        private readonly IServiceProvider serviceProvider;

        public QueryInvoker(IMediator mediator, IServiceProvider serviceProvider)
        {
            this.mediator = mediator;
            this.serviceProvider = serviceProvider;
        }

        public Task<ExecutionResponse> Invoke<TQuery>(TQuery command)
            where TQuery : IQuery

        {
            ILoggerFactory loggerFactory = this.serviceProvider.GetService<ILoggerFactory>();
            var dataProviders = this.serviceProvider.GetServices<IContextDataProvider>();

            Dictionary<string, object> contextData = new Dictionary<string, object>();

            foreach (IContextDataProvider dataProvider in dataProviders)
            {
                dataProvider.AddContextData(contextData);
            }
            QueryContext<TQuery> context = new QueryContext<TQuery>(
                command,
                loggerFactory.CreateLogger(typeof(TQuery)),
                contextData);

            return this.mediator.Send(context);
        }
    }
}