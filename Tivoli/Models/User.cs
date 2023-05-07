﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Tivoli.Data;
using Tivoli.Models;
using Tivoli.Data;
using Tivoli.Logic;

namespace Tivoli.Models
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
        private int? WorkgroupId;




        // Navigation property to Workgroup
        public virtual Workgroup Workgroup { get; set; }
        public virtual ICollection<UserRequest> UserRequests { get; set; }


        public User(string username, string passwordHash, string role, string fullName, string email, bool isActive, int? workgroupId = null)
        {
            Username = username;
            PasswordHash = passwordHash;
            Role = role;
            FullName = fullName;
            Email = email;
            this.isActive = isActive;
            WorkgroupId = workgroupId;
        }

        public int id { get => Id; set => Id = value; }
        public string username { get => Username; set => Username = value; }
        public string passwordHash { get => PasswordHash; set => PasswordHash = value; }
        public string role { get => Role; set => Role = value; }
        public string fullname { get => FullName; set => FullName = value; }
        public string email { get => Email; set => Email = value; }
        public bool IsActive { get => isActive; set => isActive = value; }
        public int? workgroupId { get => WorkgroupId; set => WorkgroupId = value; }

        public User() { }

        public User(int id, string username, string passwordHash, string role, string fullName, string email, bool isActive)
        {
            Id = id;
            Username = username;
            PasswordHash = passwordHash;
            Role = role;
            FullName = fullName;
            Email = email;
            this.isActive = isActive;
        }

        public User(int id)
        {
            Id = id;
        }

        public User(int id, string username, string passwordHash, string role, string fullName, string email, bool isActive, int? workgroupId)
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
        private List<User> users = new List<User>
                {
                new User { username = "admin", passwordHash = "admin123", role = "Admin" },
                new User { username = "user", passwordHash = "user123", role = "Regular" }
                };
       */
        // public List<User> Users { get => users; set => users = value; }




        public static User Authenticate(string username, string password, MyDatabaseContext context)
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