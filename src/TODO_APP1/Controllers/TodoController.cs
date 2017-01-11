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
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Logging;
using TODO_APP1.Utils;

namespace TODO_APP1.Controllers
{
    public class TodoController : Controller
    {
        private TODO_AppContext todoContext = new TODO_AppContext();
        private readonly ILogger _logger;

        public TodoController(ILogger<TodoController> logger)
        {
            this._logger = logger;
        }

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
                    string sqlQuery = @"select t.id, t.Title, t.Description, 
                                        convert(date, t.StartDate, 3) as StartDate,
                                        convert(date, t.EndDate, 3) as EndDate
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
        public async Task<ActionResult> AddTodo(DTOTodo _todo)
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
                    await todoContext.AddAsync(todo); //insert in database
                    todoContext.SaveChanges();

                    AddUserTodo();                 
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
            else
            {
                var message = string.Join(" | ", ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage));

                _logger.LogError(LoggingEvents.MODEL_ERROR, "Model error", message);
            }
            return RedirectToAction("Index");
        }

        public void AddUserTodo()
        {
            try
            {
                string sqlQueryForUserId = @"select *
                                             from Users u
                                             where u.UserName = " +
                                             "'" + User.Claims.FirstOrDefault().Value + "'";

                var idOfCurentUser = todoContext.Users
                    .FromSql(sqlQueryForUserId)
                    .ToList();
                int getId = idOfCurentUser.ToList().FirstOrDefault().Id;

                string sqlQueryForLastTodoId = @"select top 1 *
                                                from TODOS t
                                                order by t.id desc";

                var idOfTodo = todoContext.Todos
                    .FromSql(sqlQueryForLastTodoId)
                    .ToList();
                int getTodoId = idOfTodo.FirstOrDefault().Id;

                //todoContext.Database.ExecuteSqlCommand("uspAddInUserTodos @UserId, @TodoId", parameters: new SqlParameter[] {"@UserId", getId });
                var x = todoContext.UsersTodo
                    .FromSql("EXECUTE uspAddInUserTodos {0}, {1}", getId, getTodoId)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }           
        }

        //[Authorize]
        [HttpGet]
        public ActionResult DeleteTodo()
        {
            return Json("I should remove a todo from database");
        }

        //[Authorize]
        //[HttpGet]
        /*public ActionResult GetTodos()
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
        }*/
    }
}