using Discord.Interactions;

namespace DNet_V3_Tutorial
{
    // Must use InteractionModuleBase<SocketInteractionContext> for the InteractionService to auto-register the commands
    public class PingModule : InteractionModuleBase<SocketInteractionContext>
    {
        public InteractionService Commands { get; set; }
		private static Logger _logger;

        public PingModule(Logger logger)
        {
            _logger = logger;
        }


        // Basic slash command. [SlashCommand("name", "description")]
        // Similar to text command creation, and their respective attributes
        [SlashCommand("ping", "Receive a pong!")]
        public async Task Ping()
        {
            logger.Log(LogSeverity.Info, "PingModule : Ping", $"User: {Context.User.Username}, Command: {Context.Interaction.Data.CustomId}", null);
			await RespondAsync("pong");
        }
    }
}
