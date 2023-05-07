using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tivoli.Models;
using Tivoli.Data;
using Tivoli.Logic;

namespace Tivoli.Models
{
    public class UserRequest
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int WorkgroupId { get; set; }
        public string Status { get; set; }

        // Navigation properties
        public virtual User User { get; set; }
        public virtual Workgroup Workgroup { get; set; }

        public UserRequest(int id, int userId, int workgroupId, string status)
        {
            Id = id;
            UserId = userId;
            WorkgroupId = workgroupId;
            Status = status;
        }
    }

}
