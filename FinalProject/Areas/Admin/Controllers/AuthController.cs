using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using FinalProject.Models;

namespace FinalProject.Areas.Admin.Controllers
{
    public class AuthController : Controller
    {
        DBContext db = new DBContext();
        // GET: Admin/Auth
        [HttpGet]
        [ActionName("Login")]
        public ActionResult Login(User auth)
        {
            return View();
        }
        // POST: Admin/Auth
        [HttpPost]
        [ActionName("Login")]
        public ActionResult Login_Post(User auth)
        {
            ViewBag.Message = "";
            if (ModelState.IsValid)
            {
                // Check tài khoản Admin
                if (!db.Users.Where(m => m.UserName == auth.UserName).Count().Equals(0))
                {
                    // Check mật khẩu Admin
                    if (!db.Users.Where(m => m.UserName == auth.UserName && m.Password == auth.Password).Count().Equals(0))
                    {
                        var user_login = db.Users.Where(m => m.UserName == auth.UserName && m.Password == auth.Password).First();
                        Session["user_admin"] = user_login.UserName;
                        Session["user_id"] = user_login.ID;
                        Session["user_fullname"] = user_login.FullName;
                        return RedirectToAction("Index", "Dashboard");
                    }
                    else
                    {
                        // Thông báo sai mật khẩu
                        ViewBag.Message = "Password is invalid";
                    }
                }
                else
                {
                    Session.Abandon();
                    Session.Clear();
                    Response.Cookies.Clear();
                    // Thông báo sai tài khoản
                    ViewBag.Message = "UserName is invalid";
                }
            }
            return View();
        }
        // Đăng xuất tài khoản Admin
        public ActionResult LogOut()
        {
            Session.Abandon(); 
            return RedirectToAction("Login", "Auth");
        }
    }
}