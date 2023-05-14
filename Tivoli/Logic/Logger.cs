using System.IO;
using System.Security.Cryptography;
using System.Text;
using System;

public class Logger
{
    private static string logFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "LogFile.log");
    private static byte[] salt = Encoding.UTF8.GetBytes("a 16 byte salt");
    private static string passphrase = "your passphrase";

    private static (byte[] Key, byte[] IV) GetAesKeyAndIV()
    {
        using (var deriveBytes = new Rfc2898DeriveBytes(passphrase, salt))
        {
            byte[] key = deriveBytes.GetBytes(32); // Get a 32-byte key
            byte[] iv = deriveBytes.GetBytes(16); // Get a 16-byte IV
            return (key, iv);
        }
    }

    public static void Log(string message)
    {
        string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        string logEntry = $"{timestamp} - {message}";

        string existingLogs = ReadLogs();

        string newLogs = existingLogs + "\n" + logEntry;

        var (key, iv) = GetAesKeyAndIV();

        using (Aes aes = Aes.Create())
        {
            aes.Key = key;
            aes.IV = iv;

            using (ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV))
            {
                using (FileStream fileStream = new FileStream(logFilePath, FileMode.Create, FileAccess.Write))
                {
                    using (CryptoStream cryptoStream = new CryptoStream(fileStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter streamWriter = new StreamWriter(cryptoStream))
                        {
                            streamWriter.Write(newLogs);
                        }
                    }
                }
            }
        }
    }


    public static string ReadLogs()
    {
        var (key, iv) = GetAesKeyAndIV();

        using (Aes aes = Aes.Create())
        {
            aes.Key = key;
            aes.IV = iv;

            using (ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV))
            {
                using (FileStream fileStream = new FileStream(logFilePath, FileMode.Open, FileAccess.Read))
                {
                    using (CryptoStream cryptoStream = new CryptoStream(fileStream, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader streamReader = new StreamReader(cryptoStream))
                        {
                            string logs = streamReader.ReadToEnd();
                            return logs;
                        }
                    }
                }
            }
        }
    }
}
