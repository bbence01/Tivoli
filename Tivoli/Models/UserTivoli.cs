using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Tivoli.Data;
using Tivoli.Models;
using Tivoli.Data;
using Tivoli.Logic;
using System.Text.Json.Serialization;
using System;
using System.Windows;

namespace Tivoli.Models
{
    public class UserTivoli
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id", TypeName = "int")]
        private int Id;
        [Required]

        private string Username;
        [Required]

        private string PasswordHash;
        [Required]

        private string Role;
        [Required]

        private string FullName;
        [Required]

        private string Email;
        [Required]

        private bool isActive;


        [ForeignKey(nameof(Models.WorkgroupTivoli))]
        private int? WorkgroupId;


        private static readonly int MAX_FAILED_ATTEMPTS =3;
        private static readonly double LOCKOUT_MINUTES=100;
        private static readonly int PASSWORD_EXPIRATION_MONTHS=12;

        public bool EmailConfirmed { get; set; }

        public bool IsAdmin { get; set; }

        public DateTime? LockoutEnd { get; set; }
        public int FailedLoginAttempts { get; set; }

        public DateTime PasswordLastSet { get; set; }


        // Navigation property to WorkgroupTivoli
        [JsonIgnore]


        public virtual WorkgroupTivoli Workgroup { get; set; }
        
        [JsonIgnore]
        public virtual List<RequestTivoli> Requests { get; set; }



        public int id { get => Id; set => Id = value; }
        public string username { get => Username; set => Username = value; }
        public string passwordHash { get => PasswordHash; set => PasswordHash = value; }





        public string role { get => Role; set => Role = value; }
        public string fullname { get => FullName; set => FullName = value; }
        public string email { get => Email; set => Email = value; }
        public bool IsActive { get => isActive; set => isActive = value; }
        public int? workgroupId { get => WorkgroupId; set => WorkgroupId = value; }


        public UserTivoli(string username, string passwordHash, string role, string fullName, string email, bool isActive, int? workgroupId = null)
        {
            Username = username;
            PasswordHash = passwordHash;
            Role = role;
            FullName = fullName;
            Email = email;
            this.isActive = isActive;
            WorkgroupId = workgroupId;
            PasswordLastSet = DateTime.Now;
        }

        public UserTivoli() {
            PasswordLastSet = DateTime.Now;
        }

        public UserTivoli(int id, string username, string passwordHash, string role, string fullName, string email, bool isActive)
        {
            Id = id;
            Username = username;
            PasswordHash = passwordHash;
            Role = role;
            FullName = fullName;
            Email = email;
            this.isActive = isActive;
            PasswordLastSet = DateTime.Now;
        }

        public UserTivoli(int id)
        {
            Id = id;
            PasswordLastSet = DateTime.Now;
        }

        public UserTivoli(int id, string username, string passwordHash, string role, string fullName, string email, bool isActive, int? workgroupId)
        {
            Id = id;
            Username = username;
            PasswordHash = passwordHash;
            Role = role;
            FullName = fullName;
            Email = email;
            this.isActive = isActive;
            WorkgroupId = workgroupId;
            PasswordLastSet = DateTime.Now;
        }


        /*
        private List<UserTivoli> users = new List<UserTivoli>
                {
                new UserTivoli { username = "admin", passwordHash = "admin123", role = "Admin" },
                new UserTivoli { username = "user", passwordHash = "user123", role = "Regular" }
                };
       */
        // public List<UserTivoli> Users { get => users; set => users = value; }




        public static UserTivoli Authenticate(string username, string password, MyDatabaseContext context)
        {
            using (context)
            {
                UserTivoli user = context.Users.FirstOrDefault(u => u.username == username);


                if (user != null)
                {
                    if (user.LockoutEnd > DateTime.Now)
                    {
                        return null; // Or throw an exception, or whatever you prefer
                    }

                    if (VerifyPassword(password, user.PasswordHash))
                    {
                        user.FailedLoginAttempts = 0;
                        if (user.PasswordLastSet < DateTime.Now.AddMonths(-PASSWORD_EXPIRATION_MONTHS))
                        {
                            throw new Exception("Password has expired");
                        }

                        return user;
                    }
                    else
                    {
                        user.FailedLoginAttempts++;
                        if (user.FailedLoginAttempts >= MAX_FAILED_ATTEMPTS)
                        {
                            user.LockoutEnd = DateTime.Now.AddMinutes(LOCKOUT_MINUTES);
                        }
                        return null;
                    }
                }
            }
            return null;
        }




        public static string HashPassword(string password)
        {
            string specialChars = "!@#$%^&*()_-+={}[]|:;<>,.?/";

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

            }
            return BCrypt.Net.BCrypt.HashPassword(password);
        }



        public void SetPassword(string password)
        {
            string specialChars = "!@#$%^&*()_-+={}[]|:;<>,.?/";


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

            }

            this.PasswordHash = HashPassword(password);
        }

        private static bool VerifyPassword(string password, string passwordHash)
        {
            return BCrypt.Net.BCrypt.Verify(password, passwordHash);
        }

    }
}
