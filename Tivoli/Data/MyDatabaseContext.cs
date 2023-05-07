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

namespace Tivoli.Data
{


    public class MyDatabaseContext : System.Data.Entity.DbContext
    {
        public MyDatabaseContext() : base(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Egyetem\4.felev\INFOBiz\feleves\Tivoli\Tivoli\Data\MyDatabase.mdf;Integrated Security=True;Connect Timeout=30")
        {

        }

        public System.Data.Entity.DbSet<User> Users { get; set; }

        public System.Data.Entity.DbSet<Workgroup> Workgroups { get; set; }

        public virtual System.Data.Entity.DbSet<UserRequest> UserRequests { get; set; }


    }


}
