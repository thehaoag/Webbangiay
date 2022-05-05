using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FinalProject.Models;

namespace FinalProject.Controllers
{
    public class CategoryController : Controller
    {
        DBContext db = new DBContext();
        // GET: Category và phân trang
        public ActionResult Index(int page = 1, int pageSize = 12)
        {
            Session["action"] = "Index";
            Session["control"] = "Category";
            var dao = new DefaultController();
            var model = dao.getAllProduct(page, pageSize);
            return View(model);
        }
        // Lấy thông tin tất cả các thể loại
        public ActionResult getCategory()
        {
            ViewBag.meta = "Category";
            var v = from t in db.Categories
                    select t;
            return PartialView(v.ToList());
        }
        // Lấy thông tin sản phẩm theo ID
        public ActionResult Product(int id)
        {
            Session["action"] = "Product";
            Session["control"] = "Category";
            Session["id_product"] = id;
            var v = from t in db.Products
                    where t.ID == id
                    select t;
            return View(v.FirstOrDefault());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        // Thêm sản phảm mà người dùng mua vào đơn đặt hàng của người dùng đó.
        public ActionResult Buy_Product([Bind(Include = "CusID,CatID,Name,Price")] Order order)
        {
            if (Session["customer_id"] != null )
            {
                // Lưu thông tin đơn sản phẩm mà người dùng đặt
                order.Created_date = DateTime.Now;
                order.Status = 0;
                db.Orders.Add(order);
                db.SaveChanges();
                return RedirectToAction("Index", "Orders");
            }
            else
            {
                return RedirectToAction("Login", "Orders");
            }

        }
    }
}