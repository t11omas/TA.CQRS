namespace TA.CQRS.Command
{
    using System.Threading;
    using System.Threading.Tasks;

    using MediatR;
    
    public abstract class CommandHandler<TCommand, TResponse> : ExecutionHandlerBase<TResponse>, ICommandHandler<TCommand>
        where TResponse : class
        where TCommand : ICommand
    {
        public abstract Task<ExecutionResponse> Handle(CommandContext<TCommand> commandContext, CancellationToken cancellationToken, RequestHandlerDelegate<ExecutionResponse> next);

    }

    public abstract class CommandHandler<TCommand> : ExecutionHandlerBase<EmptyResponse>, ICommandHandler<TCommand>
        where TCommand : ICommand
    {
        public abstract Task<ExecutionResponse> Handle(CommandContext<TCommand> commandContext, CancellationToken cancellationToken, RequestHandlerDelegate<ExecutionResponse> next);

    }
}


