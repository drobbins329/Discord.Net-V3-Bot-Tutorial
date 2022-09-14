using Discord;
using Discord.Interactions;
using DNet_V3_Tutorial.Log;

namespace DNet_V3_Tutorial
{
    // Must use InteractionModuleBase<SocketInteractionContext> for the InteractionService to auto-register the commands
    public class InteractionDemoModule : InteractionModuleBase<SocketInteractionContext>
    {
        public InteractionService Commands { get; set; }
        private static Logger _logger;

        public InteractionDemoModule(ConsoleLogger logger)
        {
            _logger = logger;
        }

        // Simple slash command to bring up a message with a button to press
        [SlashCommand("button", "Button demo command")]
        public async Task ButtonInput()
        {
            var components = new ComponentBuilder();
            var button = new ButtonBuilder()
            {
                Label = "Button",
                CustomId = "button1",
                Style = ButtonStyle.Primary
            };

            // Messages take component lists. Either buttons or select menus. The button can not be directly added to the message. It must be added to the ComponentBuilder.
            // The ComponentBuilder is then given to the message components property.
            components.WithButton(button);

            await RespondAsync("This message has a button!", components: components.Build());
        }

        // This is the handler for the button created above. It is triggered by nmatching the customID of the button.
        [ComponentInteraction("button1")]
        public async Task ButtonHandler()
        {
            // try setting a breakpoint here to see what kind of data is supplied in a ComponentInteraction.
            var c = Context;
            await RespondAsync($"You pressed a button!");
        }

        // Simple slash command to bring up a message with a select menu
        [SlashCommand("menu", "Select Menu demo command")]
        public async Task MenuInput()
        {
            var components = new ComponentBuilder();
            var select = new SelectMenuBuilder()
            {  
		        CustomId = "menu",
		        Placeholder = "Select xomething..." 
	        };
            select.AddOption("First menu", "1");
            select.AddOption("Second menu", "2");

            components.WithSelectMenu(select);

            await RespondAsync("This message has a menu!", components: components.Build());
	    }

        [ComponentInteraction("menu")]
        public async Task MenuHandler(string[] selections)
        {
	        if(selections.First() == "1")
            {
                await RespondAsync("You selected first menu"); 
	        } 
            if(selections.First() == "2")
            {
                await RespondAsync("You selected second menu!"); 
	        }
	    }
    }
}
