namespace TA.CQRS.Query
{
    using System.Threading.Tasks;

    using TA.CQRS.Query;

    public interface IQueryInvoker
    {
        Task<ExecutionResponse> Invoke<TQuery>(TQuery query)
             where TQuery : IQuery;

    }
}