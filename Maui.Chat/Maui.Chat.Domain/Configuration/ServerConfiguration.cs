namespace Maui.Chat.Domain.Configuration;

public static class ServerConfiguration
{
    public const string HubName = "chathub";
    public static string GetUrl(string host) => $"http://{host}/{HubName}";
}