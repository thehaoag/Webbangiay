using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FinalProject.Models;

namespace FinalProject.Controllers
{
    public class OrdersController : Controller
    {
        DBContext db = new DBContext();
        // GET: Orders
        public ActionResult Index()
        {
            if (Session["customer_id"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Orders");
            }
            
        }

        public ActionResult Login()
        {
            return View();
        }
        // Chức năng Login dành cho user
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string username, string password)
        {
            ViewBag.Message = "";
            if (ModelState.IsValid)
            {
                // Check tài khoản user
                if (!db.Customers.Where(m => m.UserName == username).Count().Equals(0))
                {
                    // Check mật khẩu user
                    if (!db.Customers.Where(m => m.UserName == username && m.Password == password).Count().Equals(0))
                    {
                        var customer_login = db.Customers.Where(m => m.UserName == username && m.Password == password).First();
                        Session["customer_admin"] = customer_login.UserName;
                        Session["customer_id"] = customer_login.ID;
                        Session["customer_fullname"] = customer_login.FullName;
                        if (Session["action"] != null)
                        {
                            if (Session["id_product"] != null)
                            {
                                string a = Session["action"].ToString() + "/" + Session["id_product"].ToString();
                                return RedirectToAction(a, Session["control"].ToString());
                            }
                            return RedirectToAction(Session["action"].ToString(), Session["control"].ToString());
                        }
                        else
                        {
                            return RedirectToAction("Index");
                        }
                    }
                    else
                    {
                        ViewBag.Message = "Password is invalid";
                    }
                }
                else
                {
                    Session.Abandon();
                    Session.Clear();
                    Response.Cookies.Clear();

                    ViewBag.Message = "UserName is invalid";
                }
            }
            return View();
        }


        //Logout
        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Default");
        }
        // Láy thông tin đơn đặt hàng của người dùng
        public ActionResult getOrders()
        {
            ViewBag.meta = "Orders";
            int id = int.Parse(Session["customer_id"].ToString());
            var v = from t in db.Orders
                    where t.CusID == id
                    select t;
            return View(v.ToList());
        }
        // Lấy tên của thể loại theo id
        public string NameCategory(int? catid)
        {
            var item = db.Categories.Find(catid);
            if (item != null)
            {
                return item.Name;
            }
            return "uncategory";
        }

    }
}