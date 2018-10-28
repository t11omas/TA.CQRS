namespace TA.CQRS
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IContextDataProvider
    {
        Task AddContextData(Dictionary<string, object> contextData);
    }
}