using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Tivoli.Logic
{
    public class Logger
    {
        private static string logFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "LogFile.log");
        private static byte[] salt = Encoding.UTF8.GetBytes("a 16 byte salt"); // Replace with your salt
        private static string passphrase = "your passphrase"; // Replace with your passphrase

        public static void Log(string message)
        {
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string logEntry = $"{timestamp} - {message}\n";

            Rfc2898DeriveBytes deriveBytes = new Rfc2898DeriveBytes(passphrase, salt);
            byte[] key = deriveBytes.GetBytes(32); // Get a 32-byte key
            byte[] iv = deriveBytes.GetBytes(16); // Get a 16-byte IV

            using (Aes aes = Aes.Create())
            {
                aes.Key = key;
                aes.IV = iv;

                using (ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV))
                {
                    using (FileStream fileStream = new FileStream(logFilePath, FileMode.Append, FileAccess.Write))
                    {
                        using (CryptoStream cryptoStream = new CryptoStream(fileStream, encryptor, CryptoStreamMode.Write))
                        {
                            using (StreamWriter streamWriter = new StreamWriter(cryptoStream))
                            {
                                streamWriter.Write(logEntry);
                            }
                        }
                    }
                }
            }
        }
    }

}
