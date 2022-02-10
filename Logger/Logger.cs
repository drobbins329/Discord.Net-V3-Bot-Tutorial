using Discord;
using static System.Guid;

namespace DNet_V3_Tutorial.Log
{
    public abstract class Logger : ILogger
    {
        public string _guid;
        public Logger()
        {
            // extra data to show individual logger instances
            _guid = NewGuid().ToString()[^4..];
        }

        public abstract Task Log(LogMessage message);
    }
}