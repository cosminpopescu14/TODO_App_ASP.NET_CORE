using System;
using System.Collections.Generic;

namespace TODO_APP1.Models
{
    public partial class Users
    {
        public int Id { get; set; }
        public string UserFullName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int? TodoId { get; set; }

        public virtual Todos Todo { get; set; }
        public List<UsersTodos> UsersTodos { get; set; }
    }
}
