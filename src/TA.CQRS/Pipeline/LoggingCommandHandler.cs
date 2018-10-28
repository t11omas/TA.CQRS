namespace TA.CQRS.Pipeline
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using MediatR;

    using Microsoft.Extensions.Logging;

    internal class LoggingCommandHandler<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            if (request is IExecutionContext executionContext)
            {
                using (var timedOperation = new TimedOperation($"{typeof(TRequest).Name}", executionContext.Logger))
                {
                    executionContext.Logger.LogDebug($"Handling request for {typeof(TRequest).Name}");

                    TResponse response = default(TResponse);
                    try
                    {
                        response = await next().ConfigureAwait(false);
                        executionContext.Logger.LogDebug($"Handled request for {typeof(TRequest).Name}");
                    }
                    catch (Exception ex)
                    {
                        executionContext.Logger.LogError(ex, $"Handled request for {typeof(TRequest).Name}");
                        throw;
                    }
                    finally
                    {
                        timedOperation.SetComplete();
                    }

                    return response;
                }
            }

            return await next().ConfigureAwait(false);
        }
    }
}