namespace TA.CQRS.Query
{
    using System.Threading.Tasks;

    public interface IQueryInvoker
    {
        Task<ExecutionResponse> Invoke<TQuery>(TQuery query)
             where TQuery : IQuery;

    }
}