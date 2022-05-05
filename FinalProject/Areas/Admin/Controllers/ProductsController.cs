using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FinalProject.Models;
using PagedList;

namespace FinalProject.Areas.Admin.Controllers
{
    public class ProductsController : Controller
    {
        private DBContext db = new DBContext();

        // GET: Admin/Products
        public ActionResult Index(int page = 1, int pageSize = 10)
        {
            if (Session["user_admin"].Equals(""))
            {
                return RedirectToAction("Login", "Auth");
            }
            else
            {
                // Gọi hàm getAllProduct để lấy dữ liệu tất cả sản phẩm
                var model = getAllProduct(page, pageSize);
                return View(model);
            }
            
        }

        // GET: Admin/Products/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["user_admin"].Equals(""))
            {
                return RedirectToAction("Login", "Auth");
            }
            else 
            {
                // Xem thông tin chi tiết của sản phẩm
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                // Lấy dữ liệu từ database
                Product product = db.Products.Find(id);
                if (product == null)
                {
                    return HttpNotFound();
                }
                return View(product);
            }
            
        }

        // GET: Admin/Products/Create
        public ActionResult Create()
        {
            if (Session["user_admin"].Equals(""))
            {
                return RedirectToAction("Login", "Auth");
            }
            else
            {
                return View();
            }
        }

        // POST: Admin/Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "ID,CatID,Name,Detail,Image,Price,Color,Size")] Product product, HttpPostedFileBase img)
        {
            // Chức năng tạo sản phẩm mới
            var path = "";
            var filename = "";
            if (ModelState.IsValid)
            {
                if (img!=null)
                {
                    // Lấy tên file ảnh
                    filename = img.FileName;
                    // Lấy đường dẫn để lưu file ảnh
                    path = Path.Combine(Server.MapPath("~/Content/images"), filename);
                    // Lưu file ảnh vào đường dẫn
                    img.SaveAs(path);
                    product.Image = filename;
                }
                else
                {
                    //Lưu tên sản phẩm khi không có ảnh
                    product.Image = "none.png";
                }
                // Lưu thông tin sản phẩm vào database
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(product);
        }

        // GET: Admin/Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["user_admin"].Equals(""))
            {
                return RedirectToAction("Login", "Auth");
            }
            else
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Product product = db.Products.Find(id);
                if (product == null)
                {
                    return HttpNotFound();
                }
                return View(product);
            }
        }

        // POST: Admin/Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,CatID,Name,Detail,Image,Price,Color,Size")] Product product, HttpPostedFileBase img)
        {
            var path = "";
            var filename = "";
            //Lấy thông tin dữ liệu sản phẩm lúc đầu
            Product temp = getById(product.ID);
            if (ModelState.IsValid)
            {
                if (img != null)
                {
                    // Lưu thông tin ảnh mới
                    filename = img.FileName;
                    path = Path.Combine(Server.MapPath("~/Content/images"), filename);
                    img.SaveAs(path);
                    temp.Image = filename;
                }
                // Lưu thông tin sản phẩm khi thay đổi
                temp.CatID = product.CatID;
                temp.Name = product.Name;
                temp.Detail = product.Detail;
                temp.Price = product.Price;
                temp.Color = product.Color;
                temp.Size = product.Size;
                db.Entry(temp).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // GET: Admin/Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["user_admin"].Equals(""))
            {
                return RedirectToAction("Login", "Auth");
            }
            else
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Product product = db.Products.Find(id);
                if (product == null)
                {
                    return HttpNotFound();
                }
                return View(product);
            }
        }

        // POST: Admin/Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public string NameCategory(int? catid)
        {
            var item = db.Categories.Find(catid);
            if (item!=null)
            {
                return item.Name;
            }
            return "uncategory";
        }

        public Product getById(long id)
        {
            return db.Products.Where(x => x.ID == id).FirstOrDefault();
        }

        public IEnumerable<Product> getAllProduct(int page, int pageSize)
        {
            return db.Products.OrderBy(x => x.ID).ToPagedList(page, pageSize);
        }
    }
}
