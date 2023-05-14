using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using Tivoli.Models;
using Tivoli.Data;
using Tivoli.Logic;
using Azure.Core;

namespace Tivoli.Data
{


    public class MyDatabaseContext : System.Data.Entity.DbContext
    {
        

        public MyDatabaseContext() : base(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\MyDatabase.mdf;Integrated Security=True;Connect Timeout=300")
        {

        }
        public System.Data.Entity.DbSet<UserTivoli> Users { get; set; }

        public System.Data.Entity.DbSet<WorkgroupTivoli> Workgroups { get; set; }

        //  public virtual ICollection<Azure.Core.RequestTivoli> Requests { get; set; }


        public virtual System.Data.Entity.DbSet<RequestTivoli> Requests { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserTivoli>()
                        .HasOptional(u => u.Workgroup) // If a user can exist without a workgroup, use HasOptional. If not, use HasRequired.
                        .WithMany(w => w.Users)
                        .HasForeignKey(u => u.workgroupId)
                        .WillCascadeOnDelete(false);

            modelBuilder.Entity<RequestTivoli>()
                        .HasRequired(r => r.User) // Assumes a request must have a user.
                        .WithMany(u => u.Requests)
                        .HasForeignKey(r => r.UserId)
                        .WillCascadeOnDelete(false);

            modelBuilder.Entity<RequestTivoli>()
                        .HasRequired(r => r.Workgroup) // Assumes a request must have a workgroup.
                        .WithMany(w => w.Requests)
                        .HasForeignKey(r => r.WorkgroupId)
                        .WillCascadeOnDelete(false);
        }

    }


}
