namespace TA.CQRS
{
    public interface IDefaultResponseBuilder
    {
        TResponse BuildDefault<TResponse>();
    }

    public class DefaultResponseBuilder : IDefaultResponseBuilder
    {
        public TResponse BuildDefault<TResponse>()
        {
            return default(TResponse);
        }
    }
}