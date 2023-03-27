using Maui.Chat.Domain.Models;

namespace Maui.Chat.Domain;

public interface IChatMessageRepository
{
    IReadOnlyCollection<ChatMessage> Messages { get; }
    Task SaveAsync(ChatMessage message, CancellationToken cancellationToken);
}