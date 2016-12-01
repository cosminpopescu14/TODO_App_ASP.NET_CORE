using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace TODO_APP1.DTO
{
    public class DTOUser
    {
        public string UserFullName { get; set; }
        public string UserName { get; set; }

        [EmailAddress]
        public string email { get; set; }

        [MinLength(6)]
        public string password { get; set; }
    }       
}
