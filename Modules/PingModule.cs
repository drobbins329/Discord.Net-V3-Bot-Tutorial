using Discord.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNet_V3_Tutorial
{
    public class PingModule : InteractionModuleBase<SocketInteractionContext>
    {
        public InteractionService Commands { get; set; }

        private CommandHandler _handler;
        public PingModule(CommandHandler handler)
        {
            _handler = handler;
        }

        [SlashCommand("ping", "Receive a pong!")]
        public async Task Ping()
        {
            await RespondAsync("pong");
        }
    }
}
