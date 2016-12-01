using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace TODO_APP1.Controllers
{
    public class TodoController : Controller
    {
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }
    }
}