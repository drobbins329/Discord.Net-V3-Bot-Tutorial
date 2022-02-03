using Discord.Interactions;
using Discord.WebSocket;
using DNet_V3_Bot.Logger;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace DNet_V3_Tutorial
{
    public class program
    {
        private DiscordSocketClient _client;
        
        public static Task Main(string[] args) => new program().MainAsync();

        public async Task MainAsync()
        {
            using IHost host = Host.CreateDefaultBuilder()
                .ConfigureServices((_, services) =>
            services
            //.AddTransient<IConsoleLogger, Logger>()
            .AddSingleton(x => new DiscordSocketClient(new DiscordSocketConfig
            {
                GatewayIntents = Discord.GatewayIntents.AllUnprivileged,
                AlwaysDownloadUsers = true,
            }))
            .AddSingleton(x => new InteractionService(x.GetRequiredService<DiscordSocketClient>()))
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
            
            _client.Log += _ => provider.GetRequiredService<ConsoleLogger>().Log(_);
            commands.Log += _ => provider.GetRequiredService<ConsoleLogger>().Log(_);


            var token = "NzI2MjQxODgwMTY5MjUwOTE5.XvabdQ.GOUUFV6cO6R6RPGNLKbcMs_zJxw";
            await _client.LoginAsync(Discord.TokenType.Bot, token);
            await _client.StartAsync();

            await Task.Delay(-1);
        }

    }
}