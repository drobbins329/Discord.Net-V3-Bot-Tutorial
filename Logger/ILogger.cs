using Discord;

namespace DNet_V3_Bot.Logger
{
    public interface ILogger
    {
        // Establish required method for all Loggers to implement
        public Task Log(LogMessage message);
    }
}
