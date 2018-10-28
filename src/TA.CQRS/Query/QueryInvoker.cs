namespace TA.CQRS.Query
{
    using System;
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
            QueryContext<TQuery> context = new QueryContext<TQuery>(
                command,
                loggerFactory.CreateLogger(typeof(TQuery)),
                this.serviceProvider.GetService<IDefaultResponseBuilder>());

            return this.mediator.Send(context);
        }
    }
}