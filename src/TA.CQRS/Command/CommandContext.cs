namespace TA.CQRS.Command
{
    using System.Collections.Generic;

    using MediatR;
    using Microsoft.Extensions.Logging;

    using TA.CQRS;

    public class CommandContext<TCommand> : IRequest<ExecutionResponse>, ICommandContext<TCommand> where TCommand : ICommand
    {
        public CommandContext(TCommand command, ILogger logger, IDefaultResponseBuilder defaultResponse, IDictionary<string, object> contextData)
        {
            this.Command = command;
            this.DefaultResponse = defaultResponse;
            this.ContextData = contextData;
            this.Logger = logger;
        }

        public TCommand Command { get; }

        public ILogger Logger { get; }

        public IDefaultResponseBuilder DefaultResponse  { get; }

        public IDictionary<string, object> ContextData { get; }
    }
}
