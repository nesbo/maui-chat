namespace Maui.Chat.Domain.Configuration;

public class ChatConfiguration
{
    public ChatConfiguration(string senderName, string host, string encryptionKey)
    {
        SenderName = senderName;
        Host = host;
        EncryptionKey = encryptionKey;
    }
    
    public string SenderName { get; }
    public string Host { get; }
    public string EncryptionKey { get; }
    public Guid SenderId { get; } = Guid.NewGuid();
}