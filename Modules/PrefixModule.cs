using Discord.Commands;
using Discord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNet_V3_Bot.Modules
{
    // this Module name, PrefixModule, will be called by AddModule when loading the bot with the available prefix commands
    public class PrefixModule : ModuleBase<SocketCommandContext>
    {
        [Command("ping")]
        public async Task Pong()
        {
            // Reply to the user's message with the response
            await Context.Message.ReplyAsync("Pong!");
        }
    }
}
