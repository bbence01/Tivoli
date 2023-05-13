using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Entity;
using Microsoft.EntityFrameworkCore;
using Tivoli.Models;
using Tivoli.Models;
using Tivoli.Data;
using Tivoli.Logic;

namespace Tivoli.Data
{


    public class MyDatabaseInitializer : CreateDatabaseIfNotExists<MyDatabaseContext>
    {
        public MyDatabaseInitializer(MyDatabaseContext context)
        {
            Seed(context);

        }


        private string hashedPassword;
        private string password;

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
            WorkgroupTivoli Worker = new WorkgroupTivoli
                (

                "Basic Workers",
                "Basic Worker"

                );

            WorkgroupTivoli Worker2 = new WorkgroupTivoli
               (

               "Iroda",
               "iroda"

               );
            WorkgroupTivoli hrgroup = new WorkgroupTivoli
              (

              "Hr",
              "Hr"

              );

            password = "admin";
            hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);


            // Create an admin user
            UserTivoli adminUser = new UserTivoli
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

            UserTivoli basicuser = new UserTivoli
          (

              "user",
               hashedPassword,
              "UserTivoli",
               "UserTivoli Man",
               "user@example.com",
               true
          );
            UserTivoli basicuser2 = new UserTivoli
 (

     "user2",
      hashedPassword,
     "UserTivoli",
      "UserTivoli Man",
      "user@example.com",
      true
 );

            UserTivoli hruser = new UserTivoli
 (

     "hruser",
      hashedPassword,
     "hr",
      "UserTivoli Man",
      "user@example.com",
      true,
        3
 );



            UserTivoli basicuserhr = new UserTivoli
(

"userhr",
 hashedPassword,
"hr",
 "UserTivoli Man",
 "user@example.com",
 true
);




            password = "hrpassword";
            hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

            UserTivoli hr = new UserTivoli
      (

          "hr",
           hashedPassword,
           "hr",
           "hr Man",
           "hr@example.com",
           true
      );

      

         


            if (context.Users.FirstOrDefault(u => u.username == adminUser.username) == null)
            {
                context.Users.Add(adminUser);
            }
            // Add the admin user to the Users DbSet

            if (context.Users.FirstOrDefault(u => u.username == hr.username) == null)
            {
                context.Users.Add(hr);
            }


            if (context.Users.FirstOrDefault(u => u.username == basicuser2.username) == null)
            {
                context.Users.Add(basicuser2);
            }

            if (context.Users.FirstOrDefault(u => u.username == basicuser.username) == null)
            {
                context.Users.Add(basicuser);
            }
            if (context.Users.FirstOrDefault(u => u.username == basicuserhr.username) == null)
            {
                context.Users.Add(basicuserhr);
            }
            if (context.Users.FirstOrDefault(u => u.username == hruser.username) == null)
            {
                context.Users.Add(hruser);
            }

            if (context.Workgroups.FirstOrDefault(w => w.Name == Worker.Name) == null)
            {
                context.Workgroups.Add(Worker);

            }

            if (context.Workgroups.FirstOrDefault(w => w.Name == Worker2.Name) == null)
            {
                context.Workgroups.Add(Worker2);

            }
            if (context.Workgroups.FirstOrDefault(w => w.Name == hrgroup.Name) == null)
            {
                context.Workgroups.Add(hrgroup);

            }


            // Save the changes to the database
            context.SaveChanges();

            base.Seed(context);
        }
    }

}
