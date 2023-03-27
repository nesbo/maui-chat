using Microsoft.AspNetCore.SignalR;

namespace Maui.Chat.Server;

public class ChatHub : Hub
{
    public async Task SendToAll(string user, string message)
    {
        await Clients.All.SendAsync("ReceiveMessage", user, message);
    }
}