namespace TA.CQRS.Command
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using MediatR;

    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;

    using TA.CQRS;
    using TA.CQRS.Command;

    internal class CommandInvoker : ICommandInvoker
    {
        private readonly IMediator mediator;

        private readonly IServiceProvider serviceProvider;

        public CommandInvoker(IMediator mediator, IServiceProvider serviceProvider)
        {
            this.mediator = mediator;
            this.serviceProvider = serviceProvider;
        }

        public Task<ExecutionResponse> Invoke<TCommand>(TCommand command)
            where TCommand : ICommand

        {
            ILoggerFactory loggerFactory = this.serviceProvider.GetService<ILoggerFactory>();
            var dataProviders = this.serviceProvider.GetServices<IContextDataProvider>();

            Dictionary<string, object> contextData = new Dictionary<string, object>();

            foreach (IContextDataProvider dataProvider in dataProviders)
            {
                foreach (var data in dataProvider.FetchData())
                {
                   contextData.Add(data.Key, data.Value); 
                }
            }

            CommandContext<TCommand> context = new CommandContext<TCommand>(
                command,
                loggerFactory.CreateLogger(typeof(TCommand)),
                this.serviceProvider.GetService<IDefaultResponseBuilder>(),
                contextData);

            return this.mediator.Send(context);
        }
    }
}