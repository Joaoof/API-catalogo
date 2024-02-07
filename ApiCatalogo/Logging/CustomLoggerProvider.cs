using System.Collections.Concurrent;

namespace ApiCatalogo.Logging
{
        public class CustomLoggerProvider : ILoggerProvider
        {
            readonly CustomLoggerProviderConfig loggerConfig;

            readonly ConcurrentDictionary<string, CustomerLogger> loggers = new ConcurrentDictionary<string, CustomerLogger>();

            public CustomLoggerProvider(CustomLoggerProviderConfig config)
            {
                loggerConfig = config;
            } // construtor -> injenção do metodo CustomLoggerProviderConfig
            public ILogger CreateLogger(string categoryName)
            {
                return loggers.GetOrAdd(categoryName, name => new CustomerLogger(name, loggerConfig));
            }

            public void Dispose()
            {
                loggers.Clear();
            }
        }
}
