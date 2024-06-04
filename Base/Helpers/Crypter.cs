using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace Base.Helpers;

public class Crypter
{
    public static string Separator = ":__";
    public static string Encrypt(string text)
    {
        var salt = RandomNumberGenerator.GetBytes(16);
        var hash = GetHashWithSalt(text, salt);
        return $"{Convert.ToBase64String(salt)}{Separator}{Convert.ToBase64String(hash)}";

    }

    public static bool IsMatching(string text, string hash)
    {
        var hashArr = hash.Split(Separator);
        if (hashArr.Length < 2) return false;
        var salt = hashArr[0];
        var encryptedPass = hashArr[1];
        var textHash = GetHashWithSalt(text, Convert.FromBase64String(salt));
        return encryptedPass.Equals(Convert.ToBase64String(textHash));
    }

    private static byte[] GetHashWithSalt(string text, byte[] salt)
    {
        return KeyDerivation.Pbkdf2(text, salt, KeyDerivationPrf.HMACSHA512, 500, 16);
    }
}