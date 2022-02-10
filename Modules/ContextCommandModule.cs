using Discord;
using Discord.Interactions;
using Discord.WebSocket;
using DNet_V3_Tutorial.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNet_V3_Bot.Modules
{
    // Must use InteractionModuleBase<SocketInteractionContext> for the InteractionService to auto-register the commands
    public class ContextCommandModule : InteractionModuleBase<SocketInteractionContext>
    {
        public InteractionService Commands { get; set; }
        private static Logger _logger;

        public ContextCommandModule(ConsoleLogger logger)
        {
            // Constructor Injection of the ConsoleLogger
            _logger = logger;
        }

        [UserCommand("Mention")]
        public async Task MentionUser(IUser user)
        {
            // Log the user who initiated the command, and the user the command was used on
            await _logger.Log(new LogMessage(LogSeverity.Info, "ContextCommandModule : MentionUser", $"Command user: {Context.User.Username}, User pinged: {user.Username}"));
            // Respond with user ping
            await RespondAsync($"User to ping: <@{user.Id}>");
        }

        [MessageCommand("Author")]
        public async Task MessageAuthor(IMessage message)
        {
            // Log the user who initiated the command, and the channel id/message id the command was used on
            await _logger.Log(new LogMessage(LogSeverity.Info, "ContextCommandModule : MessageAuthor", $"Command user: {Context.User.Username}, ChannelID/MessageID: {message.Channel.Id}/{message.Id}"));
            // Respond with user ping
            await RespondAsync($"Message author: <@{message.Author.Id}>");
        }
    }
}
