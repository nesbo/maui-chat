using System.Collections.Concurrent;
using Maui.Chat.Domain;
using Maui.Chat.Domain.Models;

namespace Maui.Chat.App;

public class ChatMessageRepository : IChatMessageRepository
{
    private readonly ConcurrentBag<ChatMessage> _messages = new();
    public IReadOnlyCollection<ChatMessage> Messages => _messages
        .ToArray()
        .OrderByDescending(m=> m.SentUtc)
        .ToArray();

    public Task SaveAsync(ChatMessage message, CancellationToken cancellationToken)
    {
        _messages.Add(message);
        return Task.CompletedTask;
    }
}