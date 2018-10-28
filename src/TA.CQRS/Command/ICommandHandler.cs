namespace TA.CQRS.Command
{
    using MediatR;

    public interface ICommandHandler<TCommand> : IPipelineBehavior<CommandContext<TCommand>, ExecutionResponse>
        where TCommand : ICommand
    { }
}