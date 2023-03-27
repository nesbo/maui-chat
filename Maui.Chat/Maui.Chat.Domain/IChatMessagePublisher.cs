using Maui.Chat.Domain.Models;

namespace Maui.Chat.Domain;

public interface IChatMessagePublisher
{
    Task PublishAsync(ChatMessage message, CancellationToken cancellationToken = default);
}