namespace TA.CQRS.DependencyInjection
{
    using MediatR;

    using Microsoft.Extensions.DependencyInjection;

    using TA.CQRS;
    using TA.CQRS.Command;
    using TA.CQRS.Pipeline;
    using TA.CQRS.Query;

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddTaCqrs(this IServiceCollection services)
        {
            services.AddMediatR();

            // This override the default Mediar behaviour which catches argument exceptions and tries to create the pipeline via reflections.  This 
            // doesn't work for us, and just results in exceptions being swallowed, which takes too much effort to track down
            services.AddScoped<ServiceFactory>(p => p.GetService);
            
            services.AddScoped<ICommandInvoker, CommandInvoker>();
            services.AddScoped<IQueryInvoker, QueryInvoker>();

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingCommandHandler<,>));
            

            return services;
        }

        public static IServiceCollection AddCommandHandler<THandler, TCommand>(this IServiceCollection services)
            where THandler : class, IPipelineBehavior<CommandContext<TCommand>, ExecutionResponse>
            where TCommand : ICommand
        {
            services.AddScoped<IPipelineBehavior<CommandContext<TCommand>, ExecutionResponse>, THandler>();
            return services;
        }

        public static IServiceCollection AddQueryHandler<THandler, TQuery>(this IServiceCollection services)
            where THandler : class, IPipelineBehavior<QueryContext<TQuery>, ExecutionResponse>
            where TQuery : IQuery
        {
            services.AddScoped<IPipelineBehavior<QueryContext<TQuery>, ExecutionResponse>, THandler>();
            return services;
        }
    }
}
