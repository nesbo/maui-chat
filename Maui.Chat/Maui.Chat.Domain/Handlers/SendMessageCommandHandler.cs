using Maui.Chat.Domain.Models;

namespace Maui.Chat.Domain.Handlers;

public class SendMessageCommandHandler
{
    private readonly IClock _clock;
    private readonly IChatMessageCrypto _chatMessageCrypto;

    public SendMessageCommandHandler(IClock clock,
        IChatMessageCrypto chatMessageCrypto)
    {
        _clock = clock;
        _chatMessageCrypto = chatMessageCrypto;
    }

    public async Task SendMessageAsync(SendMessageCommand command, CancellationToken cancellationToken,
        IChatMessagePublisher chatMessagePublisher)
    {
        var messageEncrypted = _chatMessageCrypto.Encrypt(command.Message, command.EncryptionKey);
        var message = new ChatMessage(command.Sender, messageEncrypted, _clock.UtcNow, command.SenderId, command.Id);
        await chatMessagePublisher.PublishAsync(message, cancellationToken);
    }
}