using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Tivoli.Models;
using Tivoli.Models;
using Tivoli.Data;
using Tivoli.Logic;
using System.Windows;

namespace Tivoli.Logic
{
    public class UserRegistration
    {
        [Required(ErrorMessage = "Username is required.")]
        [StringLength(50, ErrorMessage = "Username must be between 3 and 50 characters.", MinimumLength = 3)]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(100, ErrorMessage = "Password must be between 8 and 100 characters.", MinimumLength = 8)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Role is required.")]
        public string Role { get; set; }

        [Required(ErrorMessage = "Full Name is required.")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Role is required.")]
        public string Email { get; set; }







        public static UserTivoli RegisterUser(UserRegistration registration)
        {
            using (var dbContext = new MyDatabaseContext())
            {
                // Check if the username is already taken
                if (dbContext.Users.Any(u => u.username == registration.Username))
                {
                    Logger.Log($" {registration.Username} username is already taken");

                    return null;
                }

                string specialChars = "!@#$%^&*()_-+={}[]|:;<>,.?/";

                string password = registration.Password;
                if (

                           password.Length < 8 ||
                           !password.Any(char.IsUpper) ||
                           !password.Any(char.IsLower) ||
                           !password.Any(char.IsDigit) ||
                           (!password.Any(char.IsSymbol) && !password.Any(c => specialChars.Contains(c)))


                           )
                {
                    Logger.Log($" Password does not meet complexity requirements");

                    MessageBox.Show("Password does not meet complexity requirements.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

                    return null;

                }
               

                // Hash the password
                string passwordHash = UserTivoli.HashPassword(registration.Password);

                // Create a new user with the hashed password
                UserTivoli newUser = new UserTivoli
                (
                     registration.Username,
                     passwordHash,
                     registration.Role,
                     registration.FullName,
                     registration.Email,
                     true
                );

                // Add the new user to the database

                dbContext.Users.Add(newUser);
                dbContext.SaveChanges();
                Logger.Log($" {registration.Username} username created");

                return newUser;
            }
        }

        public static ICollection<ValidationResult> ValidateUserRegistration(UserRegistration registration)
        {
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(registration, null, null);
            Validator.TryValidateObject(registration, validationContext, validationResults, true);
            Logger.Log($" {registration.Username} Valiadation");

            return validationResults;
        }


    }

}
