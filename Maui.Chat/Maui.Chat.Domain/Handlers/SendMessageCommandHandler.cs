using Maui.Chat.Domain.Models;

namespace Maui.Chat.Domain.Handlers;

public class SendMessageCommandHandler
{
    private readonly IClock _clock;
    private readonly IChatMessagePublisher _chatMessagePublisher;
    private readonly IChatMessageCrypto _chatMessageCrypto;

    public SendMessageCommandHandler(IClock clock,
        IChatMessagePublisher chatMessagePublisher,
        IChatMessageCrypto chatMessageCrypto)
    {
        _clock = clock;
        _chatMessagePublisher = chatMessagePublisher;
        _chatMessageCrypto = chatMessageCrypto;
    }

    public async Task SendMessageAsync(SendMessageCommand command, CancellationToken cancellationToken)
    {
        var messageEncrypted = _chatMessageCrypto.Encrypt(command.Message, command.EncryptionKey);
        var message = new ChatMessage(command.Sender, messageEncrypted, _clock.UtcNow, command.SenderId, command.Id);
        await _chatMessagePublisher.PublishAsync(message, cancellationToken);
    }
}