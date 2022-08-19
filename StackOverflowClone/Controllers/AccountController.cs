using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StackOverflowClone.ViewModels;
using StackOverflowClone.ServiceLayer;

namespace StackOverflowClone.Controllers
{
    public class AccountController : Controller
    {

        IUsersService us;

        public AccountController(IUsersService userService)
        {
            this.us = userService;
        }
        // GET: Account
        public ActionResult Register()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Register(RegisterViewModel rvm)
        {
            if (ModelState.IsValid)
            {
                int uid = this.us.InsertUser(rvm);
                Session["CurrrentUserID"] = uid;
                Session["CurrentUserName"] = rvm.Name;
                Session["CurrentUserEmail"] = rvm.Email;
                Session["CurrentUserPassword"] = rvm.Password;
                Session["CurrentUserIsAdmin"] = false;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("x", "Invalid data");
                return View();
            }

        }

        public ActionResult Login()
        {
            LoginViewModel lvm= new LoginViewModel();
            return View(lvm);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Login(LoginViewModel lvm)
        {
            if (ModelState.IsValid)
            {
                UserViewModel uvm = this.us.GetUsersByEmailAndPassword(lvm.Email, lvm.Password);

                if (uvm != null)
                {
                    Session["CurrrentUserID"] = uvm.UserID;
                    Session["CurrentUserName"] = uvm.Name;
                    Session["CurrentUserEmail"] = uvm.Email;
                    Session["CurrentUserPassword"] = uvm.Password;
                    Session["CurrentUserIsAdmin"] = uvm.IsAdmin;

                    if (uvm.IsAdmin)
                    {
                        return RedirectToRoute(new { area = "admin", Controller = "AdminHome", Action = "Index" });
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }

                }
                else
                {
                    ModelState.AddModelError("x", "Invalid Email/Password");
                    return View(lvm);

                }

            }
            else
            {
                ModelState.AddModelError("x", "Invalid Data");
                return View(lvm);
            }


        }
    }
}