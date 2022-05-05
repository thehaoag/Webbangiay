using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FinalProject.Models;

namespace FinalProject.Controllers
{
    public class TempController : Controller
    {
        DBContext db = new DBContext();
        // GET: Temp
        public ActionResult Index()
        {
            return View();
        }
        // Lấy tên các menu để hiển thị lên giao diện
        public ActionResult getMenu()
        {
            var v = from t in db.Menus
                    select t;
            return PartialView(v.ToList());
        }
    }
}