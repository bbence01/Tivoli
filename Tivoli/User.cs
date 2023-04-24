using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Tivoli
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id", TypeName = "int")]
        private int Id;
        private string Username;
        private string PasswordHash;
        private string Role;
        private string FullName;
        private string Email;
        private bool isActive;

        public User(string username, string passwordHash, string role, string fullName, string email, bool isActive)
        {
            Username = username;
            PasswordHash = passwordHash;
            Role = role;
            FullName = fullName;
            Email = email;
            this.isActive = isActive;
        }

        public int id { get =>Id; set => Id=value; }
        public string username { get => Username; set => Username = value; }
        public string passwordHash { get => PasswordHash; set => PasswordHash = value; }
        public string role { get => Role; set => Role = value; }
        public string fullname { get => FullName; set => FullName = value; }
        public string email { get => Email; set => Email = value; }       
        public bool IsActive { get => isActive; set => isActive = value; }


        public User() { }   


        /*
        private List<User> users = new List<User>
                {
                new User { username = "admin", passwordHash = "admin123", role = "Admin" },
                new User { username = "user", passwordHash = "user123", role = "Regular" }
                };
       */
        // public List<User> Users { get => users; set => users = value; }


       

        public static User Authenticate(string username, string password , MyDatabaseContext context)
        {
            using (context)
            {
                User user = context.Users.FirstOrDefault(u => u.username == username);
                if (user != null && VerifyPassword(password, user.PasswordHash))
                {
                    return user;
                }
            }

            return null;
        }



        public static string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }


        private static bool VerifyPassword(string password, string passwordHash)
        {
            return BCrypt.Net.BCrypt.Verify(password, passwordHash);
        }

    }
}
