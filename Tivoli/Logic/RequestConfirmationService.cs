using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tivoli.Logic
{
    public class RequestConfirmationService
    {
        private readonly EmailService _emailSender;
        private readonly Dictionary<string, int> _confirmationCodes;

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
            await _emailSender.SendEmailAsync("you@yourdomain.com", userEmail, "Your Confirmation Code", message, message);
        }

        public bool ConfirmRequest(string userEmail, int confirmationCode)
        {
            if (_confirmationCodes.TryGetValue(userEmail, out var storedCode))
            {
                if (storedCode == confirmationCode)
                {
                    _confirmationCodes.Remove(userEmail);
                    return true;
                }
            }

            return false;
        }
    }

}
