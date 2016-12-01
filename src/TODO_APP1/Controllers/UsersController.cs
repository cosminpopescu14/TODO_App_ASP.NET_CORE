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
using System.IdentityModel.Claims;
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
        public   ActionResult Login(DTOUser usr)
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

                    string hashedPassFromUser = PassEncryption.ComputeSHA1(usr.password);
                    int match = string.Compare(pass, hashedPassFromUser);

                    if (match == 0 && usr.UserName == usrName)
                    {
                        /*var claims = new[,] { new Claim("name", usr.UserName), new Claim(ClaimTypes.Role, "Admin") };
                        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        await HttpContext.Authentication.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));*/
                        //return RedirectToAction("Index", "Todo");
                    }
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }

            return RedirectToAction("Index", "Todo");
        }
    }
}