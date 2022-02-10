using Discord;
using Discord.Interactions;
using DNet_V3_Tutorial.Log;

namespace DNet_V3_Tutorial
{
    // Must use InteractionModuleBase<SocketInteractionContext> for the InteractionService to auto-register the commands
    public class PingModule : InteractionModuleBase<SocketInteractionContext>
    {
        public InteractionService Commands { get; set; }
        private static Logger _logger;

        public PingModule(ConsoleLogger logger)
        {
            _logger = logger;
        }


        // Basic slash command. [SlashCommand("name", "description")]
        // Similar to text command creation, and their respective attributes
        [SlashCommand("ping", "Receive a pong!")]
        public async Task Ping()
        {
            // New LogMessage created to pass desired info to the console using the existing Discord.Net LogMessage parameters
            await _logger.Log(new LogMessage(LogSeverity.Info, "PingModule : Ping", $"User: {Context.User.Username}, Command: ping", null));
            // Respond to the user
            await RespondAsync("pong");
        }
    }
}
