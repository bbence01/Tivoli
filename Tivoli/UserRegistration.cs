using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace Tivoli
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

        [Required(ErrorMessage = "Role is required.")]
        public string Email { get; set; }





        public User RegisterUser(UserRegistration registration)
        {
            using (var dbContext = new MyDatabaseContext())
            {
                // Check if the username is already taken
                if (dbContext.Users.Any(u => u.username == registration.Username))
                {
                    return null;
                }

                // Hash the password
                string passwordHash = HashPassword(registration.Password);

                // Create a new user with the hashed password
                User newUser = new User
                {
                    username = registration.Username,
                    passwordHash = passwordHash,
                    role = registration.Role,
                    fullname = registration.Email,
                    email = registration.Email,                    
                    IsActive = true
                };

                // Add the new user to the database
                dbContext.Users.Add(newUser);
                dbContext.SaveChanges();

                return newUser;
            }
        }

        private ICollection<ValidationResult> ValidateUserRegistration(UserRegistration registration)
        {
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(registration, null, null);
            Validator.TryValidateObject(registration, validationContext, validationResults, true);

            return validationResults;
        }


    }

}
