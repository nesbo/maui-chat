using Maui.Chat.Domain.Models;

namespace Maui.Chat.Domain;

public interface IChatMessageRepository
{
    Task SaveAsync(ChatMessage message, CancellationToken cancellationToken = default);
}