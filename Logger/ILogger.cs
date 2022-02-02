using Discord;

namespace DNet_V3_Bot.Logger
{
    public interface ILogger
    {
        // string OperationId { get; }
        public Task Log(LogMessage message);
    }
}
