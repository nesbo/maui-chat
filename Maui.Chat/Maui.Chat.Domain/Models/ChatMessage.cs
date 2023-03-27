using System.Text.Json;

namespace Maui.Chat.Domain.Models;

public class ChatMessage
{
    public ChatMessage(string sender, string message, DateTime sentUtc,
        Guid senderId, Guid id)
    {
        Sender = sender;
        Message = message;
        SentUtc = sentUtc;
        SenderId = senderId;
        Id = id;
    }
    
    public Guid Id { get; }

    public string Sender { get; }
    public Guid SenderId { get; }
    public string Message { get; private set; }
    public DateTime SentUtc { get; }
    public DateTime? ReceivedUtc { get; private set; }
    public bool IsRead { get; private set; }
    
    public static ChatMessage FromJsonString(string json) => JsonSerializer.Deserialize<ChatMessage>(json)!;
    public string ToJsonString() => JsonSerializer.Serialize(this);
    
    public void MarkAsReceived(IClock clock) => ReceivedUtc = clock.UtcNow;
    public void MarkAsRead() => IsRead = true;

    public static ChatMessage FromJsonString(string message, string encryptionKey, IChatMessageCrypto crypto)
    {
        var result = FromJsonString(message);
        result.Message = crypto.Decrypt(result.Message, encryptionKey);
        return result;
    }
}