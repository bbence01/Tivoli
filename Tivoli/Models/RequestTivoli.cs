using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tivoli.Models;
using Tivoli.Data;
using Tivoli.Logic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Tivoli.Models
{
    public class RequestTivoli
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id", TypeName = "int")]
        public int Id { get; set; }

        [ForeignKey("User")]
        [Required]

        public int UserId { get; set; }


        [ForeignKey("Workgroup")]
        [Required]

        public int WorkgroupId { get; set; }
        public string Status { get; set; }

        // Navigation properties

        [Required]

        public string ? RequestType { get; set; }

        [JsonIgnore]
        public virtual UserTivoli User { get; set; }


        [JsonIgnore]
        public virtual WorkgroupTivoli Workgroup { get; set; }

        public RequestTivoli(int userId, int workgroupId, string status)
        {

            UserId = userId;
            WorkgroupId = workgroupId;
            Status = status;
        }

        public RequestTivoli()
        {

            
        }
    }

}
