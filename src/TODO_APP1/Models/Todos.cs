using System;
using System.Collections.Generic;

namespace TODO_APP1.Models
{
    public partial class Todos
    {
        public Todos()
        {
            Users = new HashSet<Users>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public virtual ICollection<Users> Users { get; set; }
    }
}
