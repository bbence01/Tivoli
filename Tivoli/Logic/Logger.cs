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
    public static class Logger
    {
        private static readonly string logFilePath = "log.txt";
        private static readonly byte[] key = Encoding.UTF8.GetBytes("A long and secure key here");
        private static readonly byte[] iv = Encoding.UTF8.GetBytes("A 16 byte IV here");

        public static void Log(string message)
        {
            using (Aes aes = Aes.Create())
            {
                ICryptoTransform encryptor = aes.CreateEncryptor(key, iv);

                using (FileStream fileStream = new FileStream(logFilePath, FileMode.Append))
                {
                    using (CryptoStream cryptoStream = new CryptoStream(fileStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter writer = new StreamWriter(cryptoStream))
                        {
                            writer.WriteLine($"{DateTime.Now}: {message}");
                        }
                    }
                }
            }
        }

        public static string ReadLogs()
        {
            using (Aes aes = Aes.Create())
            {
                ICryptoTransform decryptor = aes.CreateDecryptor(key, iv);

                using (FileStream fileStream = new FileStream(logFilePath, FileMode.Open))
                {
                    using (CryptoStream cryptoStream = new CryptoStream(fileStream, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader reader = new StreamReader(cryptoStream))
                        {
                            return reader.ReadToEnd();
                        }
                    }
                }
            }
        }
    }
}
