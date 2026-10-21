using NLog;
using RecipeStudio.Application.Common.Interfaces;

namespace RecipeStudio.Infrastructure.Logging
{
    public class NLogLogger : IAppLogger
    {
        // create a static logger field
        private static Logger _logger = LogManager.GetCurrentClassLogger();

    

        public void LogError(Exception ex, string message)
        {
            _logger.Error(ex, message);
        }

        public void LogWarning(Exception ex, string message)
        {
            _logger.Warn(ex, message);
        }

        public void LogInfo(string message)
        {
            _logger.Info(message);
        }

        public void LogFatal(Exception ex, string message)
        {
            _logger.Fatal(ex, message);           
        }

    }

}
