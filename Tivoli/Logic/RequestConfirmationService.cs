using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Tivoli.Logic
{
    public class RequestConfirmationService
    {
        private readonly EmailService _emailSender;
        private readonly Dictionary<string, int> _confirmationCodes;
        private readonly Dictionary<string, int> _attemptCounts = new Dictionary<string, int>();
        private const int MaxAttempts = 3;
        public RequestConfirmationService(EmailService emailSender)
        {
            _emailSender = emailSender;
            _confirmationCodes = new Dictionary<string, int>();
        }

        public async Task SendConfirmationEmailAsync(string userEmail, string requestDetails)
        {
            var confirmationCode = new Random().Next(100000, 999999);
            _confirmationCodes[userEmail] = confirmationCode;

            var message = $"Request Details: {requestDetails}\nConfirmation Code: {confirmationCode}";
            await _emailSender.SendEmailAsync("tivoliteszt002@gmail.com", userEmail, "Your Confirmation Code", message, message);
            Logger.Log($" {userEmail} Email sent");

        }

        public bool ConfirmRequest(string userEmail, int confirmationCode)
        {

            if (_confirmationCodes.TryGetValue(userEmail, out var storedCode))
            {
                if (storedCode == confirmationCode)
                {
                    _confirmationCodes.Remove(userEmail);
                    _attemptCounts.Remove(userEmail); // Remove the user from the attempt counts dictionary
                    Logger.Log($" {userEmail} corrent code");

                    return true;
                }
                else
                {
                    _attemptCounts[userEmail]++; // Increment the attempt count

                    // If the attempt count is over the maximum, generate a new code and reset the count
                    if (_attemptCounts[userEmail] > MaxAttempts)
                    {
                        _attemptCounts[userEmail] = 0; // Reset the attempt count
                        SendConfirmationEmailAsync(userEmail, "Details").Wait(); // Generate and send a new code
                        Logger.Log($" Max attempts code reset");

                    }
                }
            }

            return false;
        }
    }

}
