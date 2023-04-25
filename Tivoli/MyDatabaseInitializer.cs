using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace Tivoli
{
  

    public class MyDatabaseInitializer : CreateDatabaseIfNotExists<MyDatabaseContext>
    {
        public MyDatabaseInitializer(MyDatabaseContext context)
        {
            Seed(context);
        }


        private string hashedPassword;
        private string password ;

        protected override void Seed(MyDatabaseContext context)
        {/*
            // Hash the password (for simplicity, we are using SHA256, but you should use a more secure method)
            using (var sha256 = new System.Security.Cryptography.SHA256Managed())
            {
                var passwordBytes = System.Text.Encoding.UTF8.GetBytes("admin_password");
                var hashedPasswordBytes = sha256.ComputeHash(passwordBytes);
                this.hashedPassword = BitConverter.ToString(hashedPasswordBytes).Replace("-", "").ToLower();
            }
            */


             password = "admin";
             hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);
           

            // Create an admin user
            User adminUser = new User
            (

                "admin",
                 hashedPassword,
                "Admin",
                 "Admin Man",
                 "admin@example.com",
                 true
            );

            password = "user";
            hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

            User basicuser = new User
          (

              "user",
               hashedPassword,
              "User",
               "User Man",
               "user@example.com",
               true
          );


            Workgroup Worker = new Workgroup
                (

                "Basic Workers",
                "Basic Worker"

                );
    

            

            if (context.Users.FirstOrDefault(u => u.username == adminUser.username)==null)
            {
                context.Users.Add(adminUser);
            }
            // Add the admin user to the Users DbSet




            if (context.Users.FirstOrDefault(u => u.username == basicuser.username) == null)
            {
                context.Users.Add(basicuser);
            }


            if (context.Workgroups.FirstOrDefault(w => w.Name == Worker.Name) == null)
            {
                context.Workgroups.Add(Worker);
            }
            

            // Save the changes to the database
            context.SaveChanges();

            base.Seed(context);
        }
    }

}
