namespace Maui.Chat.Domain.Handlers;

public class SendMessageCommand
{
    public SendMessageCommand(string sender, Guid senderId, string message, string encryptionKey)
    {
        Sender = sender;
        SenderId = senderId;
        Message = message;
        EncryptionKey = encryptionKey;
    }

    public Guid Id { get; } = Guid.NewGuid();
    public string Sender { get; }
    public Guid SenderId { get; }
    public string Message { get; }
    public string EncryptionKey { get; }
}