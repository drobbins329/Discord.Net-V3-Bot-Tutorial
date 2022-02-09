using Discord;
using Discord.Interactions;

namespace DNet_V3_Tutorial
{
    public class HelloModalModule : InteractionModuleBase<SocketInteractionContext>
    {
        public InteractionService Commands { get; set; }

        public HelloModalModule()
        {
            // nothing to see here
        }


        // Basic slash command. [SlashCommand("name", "description")]
        // Similar to text command creation, and their respective attributes
        [SlashCommand("modal", "Test modal inputs")]
        public async Task ModalInput()
        {
            await Context.Interaction.RespondWithModalAsync<HelloModal>("modal_input_demo");
        }

        [ModalInteraction("modal_input_demo")]
        public async Task ModalResponce(HelloModal modal)
        {
            // Build the message to send.
            string message = $"{modal.Greeting}";

            // Specify the AllowedMentions so we don't actually ping everyone.
            AllowedMentions mentions = new();
            mentions.AllowedTypes = AllowedMentionTypes.Users;

            // Respond to the modal.
            await RespondAsync(message, allowedMentions: mentions, ephemeral: true);
        }
    }

    // Defines the modal that will be sent.
    public class HelloModal : IModal
    {
        public string Title => "Hello Modal Inputs";
        // Strings with the ModalTextInput attribute will automatically become components.
        [InputLabel("Send a greeting!")]
        [ModalTextInput("greeting_input", TextInputStyle.Paragraph, placeholder: "Be nice...", maxLength: 200)]
        public string Greeting { get; set; }

    }
}