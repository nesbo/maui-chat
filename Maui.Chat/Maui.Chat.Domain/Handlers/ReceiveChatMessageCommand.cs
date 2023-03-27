namespace Maui.Chat.Domain.Handlers;

public class ReceiveChatMessageCommand
{
    public ReceiveChatMessageCommand(string message, string encryptionKey)
    {
        Message = message;
        EncryptionKey = encryptionKey;
    }
    
    public string Message { get; }
    public string EncryptionKey { get; }
}