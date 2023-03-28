using Maui.Chat.Domain;
using Maui.Chat.Domain.Models;
using Microsoft.AspNetCore.SignalR.Client;

namespace Maui.Chat.App;

internal class SignalRChatMessagePublisher : IChatMessagePublisher
{
    private readonly HubConnection _hubConnection;

    public SignalRChatMessagePublisher(HubConnection hubConnection)
    {
        _hubConnection = hubConnection;
    }

    public async Task PublishAsync(ChatMessage message, CancellationToken cancellationToken)
    {
        await _hubConnection
            .SendAsync("SendToAll", message.ToJsonString(), cancellationToken);
    }
}