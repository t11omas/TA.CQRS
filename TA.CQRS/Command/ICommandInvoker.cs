namespace TA.CQRS.Command
{
    using System.Threading.Tasks;

    public interface ICommandInvoker
    {
        Task<ExecutionResponse> Invoke<TCommand>(TCommand command)
             where TCommand : ICommand;

    }
}