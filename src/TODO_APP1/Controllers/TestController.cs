using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TODO_APP1.Models;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace TODO_APP1.Controllers
{
    public class TestController : Controller
    {
        // GET: /<controller>/
        private TODO_AppContext _todo;

        public TestController(TODO_AppContext todo)
        {
            _todo = todo;
        }

        public IActionResult Index()
        {
            return View(_todo.Todos.ToList());
        }
    }
}
