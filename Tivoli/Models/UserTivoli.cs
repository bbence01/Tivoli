using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Tivoli.Data;
using Tivoli.Models;
using Tivoli.Data;
using Tivoli.Logic;
using System.Text.Json.Serialization;

namespace Tivoli.Models
{
    public class UserTivoli
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
        [ForeignKey(nameof(Models.WorkgroupTivoli))]
        private int? WorkgroupId;
        public bool EmailConfirmed { get; set; }

        public bool IsAdmin { get; set; }


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
        }

        public UserTivoli() { }

        public UserTivoli(int id, string username, string passwordHash, string role, string fullName, string email, bool isActive)
        {
            Id = id;
            Username = username;
            PasswordHash = passwordHash;
            Role = role;
            FullName = fullName;
            Email = email;
            this.isActive = isActive;
        }

        public UserTivoli(int id)
        {
            Id = id;
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
