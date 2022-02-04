using Discord.Interactions;
using Discord.WebSocket;
using DNet_V3_Bot.Logger;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration.Yaml;
using YamlDotNet;
using System;
using System.Collections;
using DNet_V3_Bot;

namespace DNet_V3_Tutorial
{
    public class program
    {
        private DiscordSocketClient _client;        
        
        public static Task Main(string[] args) => new program().MainAsync();

        public async Task MainAsync()
        {
            var config = new ConfigurationBuilder()
            .AddEnvironmentVariables(prefix: "&")
            .SetBasePath(AppContext.BaseDirectory)
            .AddYamlFile("config.yml")
            .Build();
            
            using IHost host = Host.CreateDefaultBuilder()
                .ConfigureServices((_, services) =>
            services
            //.AddTransient<IConsoleLogger, Logger>()
            .AddSingleton(config)
            .AddSingleton(x => new DiscordSocketClient(new DiscordSocketConfig
            {
                GatewayIntents = Discord.GatewayIntents.AllUnprivileged,
                AlwaysDownloadUsers = true,
            }))
            .AddSingleton(x => new InteractionService(x.GetRequiredService<DiscordSocketClient>()))
            .AddSingleton<CommandHandler>()
            .AddTransient<ConsoleLogger>())
            .Build();

            // _client = new DiscordSocketClient(new DiscordSocketConfig()
            // {
            //     GatewayIntents = Discord.GatewayIntents.AllUnprivileged,
            //     AlwaysDownloadUsers = true,
            // }) ;

            await RunAsync(host);
        }

        public async Task RunAsync(IHost host)
        {
            using IServiceScope serviceScope = host.Services.CreateScope();
            IServiceProvider provider = serviceScope.ServiceProvider;

            var commands = provider.GetRequiredService<InteractionService>();
            _client = provider.GetRequiredService<DiscordSocketClient>();
            var config = provider.GetRequiredService<IConfigurationRoot>();

            await provider.GetRequiredService<CommandHandler>().InitializeAsync();

            _client.Log += _ => provider.GetRequiredService<ConsoleLogger>().Log(_);
            commands.Log += _ => provider.GetRequiredService<ConsoleLogger>().Log(_);

            _client.Ready += async () =>
            {
                if (IsDebug())
                    // Id of the test guild can be provided from the Configuration object
                    await commands.RegisterCommandsToGuildAsync(UInt64.Parse(config["testGuild"]), true);
                else
                    await commands.RegisterCommandsGloballyAsync(true);
            };


            await _client.LoginAsync(Discord.TokenType.Bot, config["tokens:discord"]);
            await _client.StartAsync();

            await Task.Delay(-1);
        }

        static bool IsDebug()
        {
#if DEBUG
            return true;
#else
            return false;
#endif
        }
    }
}