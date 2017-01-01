using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TODO_APP1.Models
{
    public class UsersTodos
    {
        //[ForeignKey("Todos")]
        public int UserId { get; set; }
        public Users User { get; set; }

       // [ForeignKey("Users")]
        public int TodoId { get; set; }
        public Todos Todo { get; set; }
    }
}
