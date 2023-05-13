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
using System.Text.Json.Serialization;

namespace Tivoli.Models
{
    public class WorkgroupTivoli
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Id", TypeName = "int")]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }


        public int LeaderId { get; set; } // New field for the Leader
      

        
        [JsonIgnore]
        public virtual List<RequestTivoli> Requests { get; set; }
        
        [JsonIgnore]
        public virtual List<UserTivoli> Users { get; set; }



        public WorkgroupTivoli(int idW)
        {
            Id = idW;
        }

        public WorkgroupTivoli(string name, string description)
        {
            Name = name;
            Description = description;

        }

        public WorkgroupTivoli() { }

        public WorkgroupTivoli(int id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;
        }

        public WorkgroupTivoli(string name, string description, int Leaderid)
        {
            Name = name;
            Description = description;
            LeaderId = Leaderid;
        }


    }

}
