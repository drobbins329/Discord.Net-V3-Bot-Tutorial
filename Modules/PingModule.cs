using Discord;
using Discord.Interactions;
using Discord.WebSocket;
using DNet_V3_Bot.Logger;
using System.Linq;

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
            SocketInteraction i = Context.Interaction;
            await _logger.Log(new LogMessage(LogSeverity.Info, "PingModule : Ping", $"User: {Context.User.Username}, Command: ping", null));
			await RespondAsync("pong");
        }
    }
}
