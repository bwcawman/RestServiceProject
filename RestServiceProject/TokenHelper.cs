using System.Text.Json;

namespace RestServiceProject;

public static class TokenHelper
{
    public static string GetToken(string email)
    {
        var token = new Token { Email = email, Expires = DateTime.UtcNow.AddMinutes(1) };
        var jsonString = JsonSerializer.Serialize(token);
        var encryptedJsonString = Crypto.EncryptStringAES(jsonString);
        return encryptedJsonString;
    }

    public static Token DecodeToken(string token)
    {
        var decryptedJsonString = Crypto.DecryptStringAES(token);
        var tokenObject = JsonSerializer.Deserialize<Token>(decryptedJsonString);
        return tokenObject;
    }
}
