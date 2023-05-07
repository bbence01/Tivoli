using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tivoli.Models;
using Tivoli.Data;
using Tivoli.Logic;

namespace Tivoli.Models
{
    public class Workgroup
    {
        public Workgroup(int idW)
        {
            Id = idW;
        }

        public Workgroup(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public Workgroup() { }

        public Workgroup(int id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Id", TypeName = "int")]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<UserRequest> UserRequests { get; set; }

    }

}
