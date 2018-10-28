namespace TA.CQRS.Command
{
    using System.Collections.Generic;

    using MediatR;
    using Microsoft.Extensions.Logging;

    using TA.CQRS;

    public class CommandContext<TCommand> : IRequest<ExecutionResponse>, ICommandContext<TCommand> where TCommand : ICommand
    {
        public CommandContext(TCommand command, ILogger logger, IDictionary<string, object> contextData)
        {
            this.Command = command;
            this.ContextData = contextData;
            this.Logger = logger;
        }

        public TCommand Command { get; }

        public ILogger Logger { get; }

        public IDictionary<string, object> ContextData { get; }
    }
}
