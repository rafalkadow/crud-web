using NLog.Extensions.Logging;
using NLog.Web;

namespace Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var logger = NLog.LogManager.GetLogger("");
            logger.Info("Started program.");

            try
            {
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception exception)
            {
                logger.Error(exception, "Stopped program because of exception");
                throw;
            }
            finally
            {
                NLog.LogManager.Shutdown();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
                Host.CreateDefaultBuilder(args)
                    .ConfigureWebHostDefaults(webBuilder =>
                    {
                        webBuilder.UseStartup<Startup>();
                    }).ConfigureLogging(logging =>
                    {
                        logging.ClearProviders();
                        logging.AddNLog();
                    })
                    .UseNLog();
    }
}
