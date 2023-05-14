using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Tivoli.Logic
{
    public class RequestConfirmationService
    {
        private readonly EmailService _emailSender;
        private const int MaxAttempts = 3;
        
        private static string FilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "confirmationCodes.bin");

        private static  string AttemptCountsFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "attemptCounts.bin");

        private readonly byte[] key;
        private readonly byte[] iv;

        public RequestConfirmationService(EmailService emailSender)
        {
            _emailSender = emailSender;

            // TODO: Replace with your actual key and IV
            key = new byte[16];
            iv = new byte[16];

            using (var aes = Aes.Create())
            {
                aes.GenerateKey();
                aes.GenerateIV();
                key = aes.Key;
                iv = aes.IV;
            }
        }

        public async Task SendConfirmationEmailAsync(string userEmail, string requestDetails)
        {
            var confirmationCode = new Random().Next(100000, 999999);
            var confirmationCodes = ReadFromFile<Dictionary<string, int>>(FilePath);
            confirmationCodes[userEmail] = confirmationCode;
            WriteToFile(confirmationCodes, FilePath);

            var message = $"Request Details: {requestDetails}\nConfirmation Code: {confirmationCode}";
            await _emailSender.SendEmailAsync("tivoliteszt002@gmail.com", userEmail, "Your Confirmation Code", message, message);
            Logger.Log($"{userEmail} Email sent");
        }

        public bool ConfirmRequest(string userEmail, int confirmationCode)
        {
            var confirmationCodes = ReadFromFile<Dictionary<string, int>>(FilePath);
            var attemptCounts = ReadFromFile<Dictionary<string, int>>(AttemptCountsFilePath);

            if (confirmationCodes.TryGetValue(userEmail, out var storedCode))
            {
                if (storedCode == confirmationCode)
                {
                    confirmationCodes.Remove(userEmail);
                    attemptCounts.Remove(userEmail);
                    WriteToFile(confirmationCodes, FilePath);
                    WriteToFile(attemptCounts, AttemptCountsFilePath);
                    Logger.Log($"{userEmail} correct code");
                    return true;
                }
                else
                {
                    if (!attemptCounts.ContainsKey(userEmail))
                    {
                        attemptCounts[userEmail] = 0;
                    }

                    attemptCounts[userEmail]++;

                    if (attemptCounts[userEmail] > MaxAttempts)
                    {
                        attemptCounts[userEmail] = 0;
                        SendConfirmationEmailAsync(userEmail, "Details").Wait();
                        Logger.Log("Max attempts code reset");
                    }

                    WriteToFile(attemptCounts, AttemptCountsFilePath);
                }
            }

            return false;
        }

        private T ReadFromFile<T>(string filePath) where T : new()
        {
            if (!File.Exists(filePath))
            {
                return new T();
            }

            var encryptedBytes = File.ReadAllBytes(filePath);
            var decryptedBytes = Decrypt(encryptedBytes);
            var json = Encoding.UTF8.GetString(decryptedBytes);
            return JsonSerializer.Deserialize<T>(json);
        }

        private void WriteToFile<T>(T data, string filePath)
        {
            var json = JsonSerializer.Serialize(data);
            var bytes = Encoding.UTF8.GetBytes(json);
            var encryptedBytes = Encrypt(bytes);
            File.WriteAllBytes(filePath, encryptedBytes);
        }

        private byte[] Encrypt(byte[] data)
        {
            using var aes = Aes.Create();
            using var encryptor = aes.CreateEncryptor(key, iv);
            return PerformCryptography(data, encryptor);
        }

        private byte[] Decrypt(byte[] data)
        {
            using var aes = Aes.Create();
            using var decryptor = aes.CreateDecryptor(key, iv);
            return PerformCryptography(data, decryptor);
        }

        private byte[] PerformCryptography(byte[] data, ICryptoTransform cryptoTransform)
        {
            using var ms = new MemoryStream();
            using var cs = new CryptoStream(ms, cryptoTransform, CryptoStreamMode.Write);
            cs.Write(data, 0, data.Length);
            cs.FlushFinalBlock();
            return ms.ToArray();
        }
    }
}

