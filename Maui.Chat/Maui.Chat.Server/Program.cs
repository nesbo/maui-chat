// See https://aka.ms/new-console-template for more information

using Maui.Chat.Domain.Configuration;
using Maui.Chat.Server;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSignalR();

var host = builder.Build();

host.MapHub<ChatHub>($"/{ServerConfiguration.HubName}");

await host.RunAsync();