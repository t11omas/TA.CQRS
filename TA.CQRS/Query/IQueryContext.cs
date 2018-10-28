namespace TA.CQRS.Query
{
    using TA.CQRS.Query;

    public interface IQueryContext<out TQuery> : IExecutionContext
        where TQuery : IQuery
    {
        TQuery Query { get; }
    }
}