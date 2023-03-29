using System.Security.Cryptography;
using System.Text;

namespace Maui.Chat.Domain;

public class ChatMessageCryptoAes : IChatMessageCrypto
{
    private const string Salt = "d357b157098746cd";
    private readonly Aes _aes = Aes.Create();
    
    public string Encrypt(string message, string key)
    {
        var encryptor = _aes.CreateEncryptor(
            Encoding.UTF8.GetBytes(key),
            Encoding.UTF8.GetBytes(Salt));
        
        using var memoryStream = new MemoryStream();
        using var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
        using var streamWriter = new StreamWriter(cryptoStream);
        streamWriter.Write(message);
        
        return Convert.ToBase64String(memoryStream.ToArray());
    }

    public string Decrypt(string message, string key)
    {
        var decryptor = _aes.CreateDecryptor(
            Encoding.UTF8.GetBytes(key),
            Encoding.UTF8.GetBytes(Salt));

        using var memoryStream = new MemoryStream(Convert.FromBase64String(message));
        using var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
        using var streamReader = new StreamReader(cryptoStream);
        return streamReader.ReadToEnd();
    }
}