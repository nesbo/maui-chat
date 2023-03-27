namespace Maui.Chat.Domain;

public class Clock : IClock
{
    public DateTime UtcNow => DateTime.UtcNow;
}