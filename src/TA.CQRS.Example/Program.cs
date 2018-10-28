using System;

namespace TA.CQRS.Example
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Security.Claims;
    using System.Threading;
    using System.Threading.Tasks;

    using MediatR;

    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;

    using TA.CQRS.Command;
    using TA.CQRS.DependencyInjection;

    using ILogger = Microsoft.Extensions.Logging.ILogger;

    internal static class AdditionalDataExtensions
    {
        public static DateTime? GetCreatedAt(this Dictionary<string, object> contextData)
        {
            object data;
            if (contextData.TryGetValue("CreatedAt", out data))
            {
                if (data is DateTime dateTime)
                {
                    return dateTime;
                }
            }

            return null;
        }
    }

    internal static class ContextDataExtensions
    {
        public static void SetExecutingUser(this Dictionary<string, object> contextData, string user)
        {
           contextData.Add("ExecutingUser", user);
        }

        public static string GetExecutingUser(this IDictionary<string, object> contextData)
        {
            object data;
            if (contextData.TryGetValue("ExecutingUser", out data))
            {
                return data.ToString();
            }

            return null;
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
                .AddLogging(config => config.AddConsole().SetMinimumLevel(LogLevel.Debug))
                .AddTaCqrs()
                .AddCommandHandler<AddCustomerCommandHandler, AddCustomerCommand>()
                .AddScoped<IContextDataProvider, ContextDataProvider>()
                .BuildServiceProvider();

            Stopwatch stopwatch = Stopwatch.StartNew();
            ILoggerFactory loggerFactory = serviceProvider.GetService<ILoggerFactory>();
            ILogger logger = loggerFactory.CreateLogger<Program>();
            ICommandInvoker commandInvoker = serviceProvider.GetService<ICommandInvoker>();

            for (int i = 0; i < 10; i++)
            {
                ExecutionResponse executionResponse = commandInvoker.Invoke(new AddCustomerCommand() { Name = "Customer 1" }).Result;
                logger.LogDebug($"{executionResponse.AdditionalData.GetCreatedAt()}");
            }

            stopwatch.Stop();
            logger.LogDebug($"{stopwatch.Elapsed}");
            logger.LogDebug("Completed!, Press any key to exit");
            Console.WriteLine($"{stopwatch.Elapsed}");
            Console.ReadKey();
        }
    }

    internal class ContextDataProvider : IContextDataProvider
    {
        public Task AddContextData(Dictionary<string, object> contextData)
        {
            contextData.SetExecutingUser("Some Tet User");
            return Task.CompletedTask;
        }
    }


    public class AddCustomerCommand : ICommand
    {
        public string Name { get; set; }
    }

    public class Customer
    {
        public string Name { get; set; }

        public string CreatedBy { get; set; }
    }

    public class AddCustomerCommandHandler : CommandHandler<AddCustomerCommand, Customer>
    {
        public override async Task<ExecutionResponse> Handle(CommandContext<AddCustomerCommand> commandContext, CancellationToken cancellationToken, RequestHandlerDelegate<ExecutionResponse> next)
        {
            Customer customer = new Customer();
            customer.Name = commandContext.Command.Name;
            customer.CreatedBy = commandContext.ContextData.GetExecutingUser();
            return this.Ok(customer, new Dictionary<string, object> { { "CreatedAt", DateTime.UtcNow } });
        }
    }
}
