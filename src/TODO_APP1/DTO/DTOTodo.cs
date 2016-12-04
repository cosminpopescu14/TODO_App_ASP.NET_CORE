using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TODO_APP1.DTO
{
    public class DTOTodo
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}
