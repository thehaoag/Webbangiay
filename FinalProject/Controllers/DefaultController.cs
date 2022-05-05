using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FinalProject.Models;
using PagedList;

namespace FinalProject.Controllers
{
    public class DefaultController : Controller
    {
        DBContext db = new DBContext();
        // GET: Default
        public ActionResult Index()
        {
            return View();
        }
        // Lấy thông tin của tất cả thể loại
        public ActionResult getCategory()
        {
            ViewBag.meta = "Category";
            var v = from t in db.Categories
                    select t;
            return PartialView(v.ToList());
        }
        // Lấy thông tin tất cả sản phẩm và phân trang
        public IEnumerable<Product> getAllProduct(int page, int pageSize)
        {
            return db.Products.OrderBy(x => x.ID).ToPagedList(page, pageSize);
        }
        // Lấy 10 sản phẩm mới nhất
        public ActionResult getNewestProduct()
        {
            var v = (from t in db.Products
                     select t).OrderByDescending(t => t.ID).Take(10);

            return PartialView(v.ToList());
        }
    }
}