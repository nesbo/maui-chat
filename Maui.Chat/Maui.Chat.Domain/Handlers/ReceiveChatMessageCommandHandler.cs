using Maui.Chat.Domain.Models;

namespace Maui.Chat.Domain.Handlers;

public class ReceiveChatMessageCommandHandler
{
    private readonly IChatMessageCrypto _chatMessageCrypto;
    private readonly IChatMessageRepository _messageRepository;
    private readonly IClock _clock;

    public ReceiveChatMessageCommandHandler(
        IChatMessageCrypto chatMessageCrypto,
        IChatMessageRepository messageRepository,
        IClock clock)
    {
        _chatMessageCrypto = chatMessageCrypto;
        _messageRepository = messageRepository;
        _clock = clock;
    }

    public async Task ReceiveChatMessageAsync(ReceiveChatMessageCommand command, CancellationToken cancellationToken)
    {
        var message = ChatMessage.FromJsonString(command.Message, command.EncryptionKey, _chatMessageCrypto);
        message.MarkAsReceived(_clock);
        await _messageRepository.SaveAsync(message, cancellationToken);
    }
}