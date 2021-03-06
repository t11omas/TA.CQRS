﻿namespace TA.CQRS
{
    using System.Collections.Generic;

    using Microsoft.Extensions.Logging;

    public interface IExecutionContext
    {
        ILogger Logger { get; }

        IDictionary<string, object> ContextData { get; }
    }
}