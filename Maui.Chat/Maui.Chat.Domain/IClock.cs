namespace Maui.Chat.Domain;

public interface IClock
{
    DateTime UtcNow { get; }
}