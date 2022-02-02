using Discord;
using static System.Guid;

namespace DNet_V3_Bot.Logger
{
    public abstract class Logger : ILogger
    {
        public string _guid;
        public Logger()
        {
            //_consoleLogger = consoleLogger;
            _guid = NewGuid().ToString()[^4..];
        }

        public abstract Task Log(LogMessage message);
    }
}
