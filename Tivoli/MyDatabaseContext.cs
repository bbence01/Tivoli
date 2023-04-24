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




namespace Tivoli
{
  

    public class MyDatabaseContext : System.Data.Entity.DbContext
    {
        public MyDatabaseContext() : base("MyDatabaseConnectionString")
        {
        }

        public System.Data.Entity.DbSet<User> Users { get; set; }
    }


}
