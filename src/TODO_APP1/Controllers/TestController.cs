using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TODO_APP1.Models;

using Microsoft.EntityFrameworkCore;
// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860
//Testing porpouse !
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
            /*TestController t = new TestController(_todo);
            return View(_todo.Todos.ToList());*/
            string sqlQuery = @"select *
                                from Users u
                                where u.UserName = 'cosminpop14'";

            var idOfCurentUser = _todo.Users
                .FromSql(sqlQuery)
                .ToList();

            //int id = idOfCurentUser.ToList().FirstOrDefault().Id;

            //return Json(id);
            return View(idOfCurentUser.ToList());
        }
    }
}
