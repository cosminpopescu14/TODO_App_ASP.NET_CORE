using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TODO_APP1.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

using TODO_APP1.DTO;
using TODO_APP1.Utils;
using Microsoft.Owin.Security;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace TODO_APP1.Controllers
{
    public class UsersController : Controller
    {
        private TODO_AppContext todo = new TODO_AppContext();

        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult CreateNewUser()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult LogIn()
        {
            return View();
        }

     

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateNewUser(DTOUser usr)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = new Users
                    {
                        UserFullName = usr.UserFullName,
                        UserName = usr.UserName,
                        Email = usr.email,
                        Password = PassEncryption.ComputeSHA1(usr.password)
                    };

                    todo.Users.Add(user);
                    todo.SaveChanges();
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(DTOLogin usr)
        {
            if (ModelState.IsValid)
            {
                var loginData = todo.Users
                    .Where(u => u.UserName == usr.UserName)
                    .Select(u => new { u.UserName, u.Password});
                try
                {
                    string usrName = loginData.FirstOrDefault().UserName;
                    string pass = loginData.FirstOrDefault().Password;

                    string hashedPassFromUser = PassEncryption.ComputeSHA1(usr.Password);
                    int match = string.Compare(pass, hashedPassFromUser);

                    if (match == 0 && usr.UserName == usrName)
                    {
                        List<Claim> userClaims = new List<Claim>//read about asp.net core identity. 
                        {
                            new Claim ("UserName", Convert.ToString(usrName))
                        };
                        ClaimsPrincipal principal = new ClaimsPrincipal(new ClaimsIdentity(userClaims, "local"));
                        await HttpContext.Authentication.SignInAsync("MyCookieMiddlewareInstance-TODOAPP1122016", principal);//this was set up in startup.cs. i'm using cookie middleware
                        return RedirectToAction("Index", "Todo");
                    }

                    else
                        ModelState.AddModelError("1", "Incorrect login data");
                    
                }
                catch (Exception ex)
                {

                     ModelState.AddModelError("2", "no user found" + ex);
                }
            }

            return RedirectToAction("LogIn");
        }

        public async Task<ActionResult> LogOff()
        {
            await HttpContext.Authentication.SignOutAsync("MyCookieMiddlewareInstance-TODOAPP1122016");
            return Redirect("/Users/LogIn");
        }
    }
}