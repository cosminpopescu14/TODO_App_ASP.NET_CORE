using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using TODO_APP1.DTO;
using TODO_APP1.Models;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore;

namespace TODO_APP1.Controllers
{
    public class TodoController : Controller
    {
        private TODO_AppContext todoContext = new TODO_AppContext();

        [Authorize]
        [HttpGet]
        public IActionResult Index()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    /*var todos = todoContext.Todos.Select(todo => new Todos
                    {
                        Title = todo.Title,
                        Description = todo.Description,
                        StartDate = todo.StartDate,
                        EndDate = todo.EndDate
                    });*/
                    string sqlQuery = @"select t.id, t.Title, t.Description, t.StartDate, t.EndDate
                                        from TODOS t
                                        join UsersTodos ut on t.id = ut.TodoId
                                        join Users u on u.id = ut.UserId
                                        where u.UserName =  " + "'" + User.Claims.FirstOrDefault().Value + "'";

                    var todos = todoContext.Todos
                        .FromSql(sqlQuery)
                        .ToList();

                    return View(todos.ToList());
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
                ModelState.AddModelError("1", "error ocurred :(");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult AddTodo(DTOTodo _todo)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var todo = new Todos//create object todo
                    {
                        Title = _todo.Title,
                        Description = _todo.Description,
                        StartDate = _todo.Start,
                        EndDate = _todo.End
                    };

                    Users user = new Users();
                    todoContext.Add(todo);//insert in database
                    todoContext.SaveChanges();                   
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
            return RedirectToAction("Index");
        }

        [Authorize]
        [HttpGet]
        public ActionResult GetTodos()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var todos = from todo in todoContext.Todos//select a todo from db using linq
                                select new
                                {
                                    todo.Title,
                                    todo.Description,
                                    todo.StartDate,
                                    todo.EndDate
                                };
                    return Json(todos);//return the todo in json format
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
                ModelState.AddModelError("1", "error ocurred :(");

            return RedirectToAction("Index");
        }
    }
}