using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FinalProject.Models;

namespace FinalProject.Areas.Admin.Controllers
{
    public class DashboardController : Controller
    {
        DBContext db = new DBContext();
        // GET: Admin/Dashboard
        public ActionResult Index()
        {
            // Check đã login hay chưa
            if (Session["user_admin"].Equals(""))
            {
                return RedirectToAction("Login", "Auth");
            }
            else
            {
                var user = Session["user_admin"].ToString();
                // Lấy thông tin của User
                var v = from t in db.Users
                        where t.UserName == user
                        select t;
                return View(v.FirstOrDefault());
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update([Bind(Include = "ID,FullName,UserName,Password,Email,Gender,Phone")] User users)
        {
            if (ModelState.IsValid)
            {
                // Cập nhật thông tin account Admin
                db.Entry(users).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(users);
        }
    }
}