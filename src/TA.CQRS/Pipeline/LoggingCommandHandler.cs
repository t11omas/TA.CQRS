namespace TA.CQRS.Pipeline
{
    using System;
    using System.Diagnostics;
    using System.Threading;
    using System.Threading.Tasks;

    using MediatR;

    using Microsoft.Extensions.Logging;

    using TA.CQRS.Command;
    using TA.CQRS.Query;

    internal class LoggingCommandHandler<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            if (request is IExecutionContext executionContext)
            {
                string name = typeof(TRequest).Name;
                if (request is IQueryContext<IQuery> queryContext)
                {
                    name = queryContext.Query.GetType().Name;
                }
                else

                if (request is ICommandContext<ICommand> commandContext)
                {
                    name = commandContext.Command.GetType().Name;
                }

                Stopwatch stopwatch = Stopwatch.StartNew();

                
                    executionContext.Logger.LogDebug($"Handling request for {name}");

                    TResponse response = default(TResponse);
                    try
                    {
                        response = await next().ConfigureAwait(false);
                        
                    }
                    catch (Exception ex)
                    {
                        executionContext.Logger.LogError(ex, $"Handled request for {name}");
                        throw;
                    }
                    finally
                    {
                        stopwatch.Stop();
                        executionContext.Logger.LogDebug($"Handled request for {name} completed in {stopwatch.Elapsed}");
                        executionContext.ContextData["X-Elapsed-Milliseconds"] = stopwatch.Elapsed.TotalMilliseconds;
                    }

                    return response;
                
            }

            return await next().ConfigureAwait(false);
        }
    }
}