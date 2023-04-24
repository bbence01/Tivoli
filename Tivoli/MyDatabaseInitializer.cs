using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Entity;

namespace Tivoli
{
  

    public class MyDatabaseInitializer : CreateDatabaseIfNotExists<MyDatabaseContext>
    {
        public MyDatabaseInitializer()
        {
        }


       private string hashedPassword;

        protected override void Seed(MyDatabaseContext context)
        {
            // Hash the password (for simplicity, we are using SHA256, but you should use a more secure method)
            using (var sha256 = new System.Security.Cryptography.SHA256Managed())
            {
                var passwordBytes = System.Text.Encoding.UTF8.GetBytes("admin_password");
                var hashedPasswordBytes = sha256.ComputeHash(passwordBytes);
                this.hashedPassword = BitConverter.ToString(hashedPasswordBytes).Replace("-", "").ToLower();
            }
            
            // Create an admin user
            User adminUser = new User
            {
                username = "admin",
                passwordHash = hashedPassword,              
                role = "Admin",
                fullname= "admin",
                email = "admin@example.com",
                IsActive = true
            };

            // Add the admin user to the Users DbSet
            context.Users.Add(adminUser);

            // Save the changes to the database
            context.SaveChanges();

            base.Seed(context);
        }
    }

}
