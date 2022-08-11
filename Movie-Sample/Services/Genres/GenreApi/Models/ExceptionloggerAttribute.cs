using Serilog;
using System.Web.Http.ExceptionHandling;

namespace GenreApi.Models
{
    public class ExceptionloggerAttribute : ExceptionLogger
    {
         
        public override void Log(ExceptionLoggerContext context)
        {
            var configuration = new ConfigurationBuilder()
            .AddJsonFile("Serilog.json")
            .Build();

            Serilog.Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(configuration).CreateLogger();

            try
            {
                var log = context.Exception.ToString();
                Serilog.Log.Error($"An unhandled exception occured: {log}");
            }
            catch (Exception)
            {
                Serilog.Log.CloseAndFlush();
            }
        }
    }
}
