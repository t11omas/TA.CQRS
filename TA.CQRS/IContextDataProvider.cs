namespace TA.CQRS
{
    using System.Collections.Generic;

    public interface IContextDataProvider
    {
        IEnumerable<KeyValuePair<string, object>> FetchData();
    }
}