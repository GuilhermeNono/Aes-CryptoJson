using System.Security.Cryptography;
using System.Text;
using AesEncryptApp.Common.Holder;

namespace AesEncryptApp.Common;

public static class CriptoHelper
{
    public static Aes CreateAes()
    {
        var criptoConfiguration = ConfigurationHolder.Instantiate<CriptoConfiguration>("Crypto");

        var aes = Aes.Create();
        aes.Mode = CipherMode.ECB;
        aes.Padding = PaddingMode.PKCS7;
        aes.IV = Encoding.UTF8.GetBytes(criptoConfiguration.Iv);
        aes.Key = Encoding.UTF8.GetBytes(criptoConfiguration.Key);

        return aes;
    }

    public static string AesEncrypt(string data)
    {
        using var aes = CreateAes();
        using var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
        var dataInBytes = Encoding.UTF8.GetBytes(data);
        var encrypted = encryptor.TransformFinalBlock(dataInBytes, 0, dataInBytes.Length);
        return Convert.ToBase64String(encrypted);
    }
    
    public static string AesDecrypt(string data)
    {
        using Aes aes = CreateAes();
        using var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

        var bytes = Convert.FromBase64String(data);
        var decrypted = decryptor.TransformFinalBlock(bytes, 0, bytes.Length);
        return Encoding.UTF8.GetString(decrypted);
    }
}