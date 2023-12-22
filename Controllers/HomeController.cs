using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Carts.Models;

namespace Carts.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            List<ProductsEF> result = new List<ProductsEF>();

            using( TESTEntities db = new TESTEntities())
            {
                result = (from product in db.ProductsEFs select product).ToList();
            }

            return View(result);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Index2()
        {
            return Content("<html><body><h1>這是一段測試文字1</h1></body></html>");
        }
    }
}