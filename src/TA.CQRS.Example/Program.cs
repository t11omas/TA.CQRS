using System;

namespace TA.CQRS.Example
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Threading;
    using System.Threading.Tasks;

    using MediatR;

    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;

    using Serilog;

    using TA.CQRS.Command;
    using TA.CQRS.DependencyInjection;

    using ILogger = Microsoft.Extensions.Logging.ILogger;

    class Program
    {
        static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration().MinimumLevel.Debug()
                .WriteTo.Console()
                .WriteTo.File(@"C:\temp\log.txt")
                .CreateLogger();

            var serviceProvider = new ServiceCollection()
               //    .AddLogging(config => config.AddConsole().SetMinimumLevel(LogLevel.Debug))
                .AddLogging(config => config.AddSerilog(dispose: true))
               // .addser
                .AddTaCqrs()
                .AddCommandHandler<AddCustomerCommandHandler, AddCustomerCommand>()
                .AddScoped<IContextDataProvider, ContextDataProvier>()
                .BuildServiceProvider();

            Stopwatch stopwatch = Stopwatch.StartNew();
            ILoggerFactory loggerFactory = serviceProvider.GetService<ILoggerFactory>();
            ILogger logger = loggerFactory.CreateLogger<Program>();
            ICommandInvoker commandInvoker = serviceProvider.GetService<ICommandInvoker>();

            for (int i = 0; i < 10; i++)
            {
                ExecutionResponse executionResponse = commandInvoker.Invoke(new AddCustomerCommand() { Name = "Customer 1" }).Result;
                logger.LogDebug($"{executionResponse.AdditionalData["CreatedAt"]}");
            }

            stopwatch.Stop();
            logger.LogDebug($"{stopwatch.Elapsed}");
            logger.LogDebug("Completed!, Press any key to exit");
            Console.WriteLine($"{stopwatch.Elapsed}");
            Console.ReadKey();
        }
    }

    internal class ContextDataProvier : IContextDataProvider
    {
        public IEnumerable<KeyValuePair<string, object>> FetchData()
        {
            yield return new KeyValuePair<string, object>("ExecutingUser", "Some Tet User");
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
            customer.CreatedBy = commandContext.ContextData["ExecutingUser"].ToString();
            return this.Ok(customer, new Dictionary<string, object> { { "CreatedAt", DateTime.UtcNow } });
        }
    }
}
