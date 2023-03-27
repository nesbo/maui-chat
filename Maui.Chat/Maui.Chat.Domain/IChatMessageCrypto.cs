namespace Maui.Chat.Domain;

public interface IChatMessageCrypto
{
    string Encrypt(string message, string key);
    string Decrypt(string message, string key);
}