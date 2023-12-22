using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Carts.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            List<Models.ProductsEF> result = new List<Models.ProductsEF>();

            ViewBag.ResultMessage = TempData["ResultMessage"];

            using (Models.TESTEntities db = new Models.TESTEntities())
            {
                result = (from product in db.ProductsEFs select product).ToList();
            }

            return View(result);
        }

        // 建立商品頁面
        public ActionResult Create()
        {
            return View();
        }

        // 建立商品頁面 POST回傳處理
        [HttpPost]
        public ActionResult Create(Models.ProductsEF data)
        {
            if(this.ModelState.IsValid)
            {
                using (Models.TESTEntities db = new Models.TESTEntities())
                {
                    db.ProductsEFs.Add(data);
                    db.SaveChanges();

                    TempData["ResultMessage"] = String.Format("商品[{0}]成功建立", data.Name);
                    return RedirectToAction("Index");
                }
            }

            ViewBag.ResultMessage = "資料有誤，請檢查";

            return View(data);
        }

        // 編輯商品頁面
        public ActionResult Edit(int id)
        {
            using (Models.TESTEntities db = new Models.TESTEntities())
            {
                var result = (from product in db.ProductsEFs where product.Id == id select product).FirstOrDefault();

                if (result != default(Models.ProductsEF)) return View(result);
                else
                {
                    TempData["ResultMessage"] = "資料有誤，請重新操作";
                    return RedirectToAction("Index");
                }
            }
        }

        // 編輯商品頁面 POST回傳處理
        [HttpPost]
        public ActionResult Edit(Models.ProductsEF data)
        {
            if (this.ModelState.IsValid)
            {
                using (Models.TESTEntities db = new Models.TESTEntities())
                {
                    var result = (from product in db.ProductsEFs where product.Id == data.Id select product).FirstOrDefault();

                    result.Name = data.Name;
                    result.Description = data.Description;
                    result.CategoryId = data.CategoryId;
                    result.Price = data.Price;
                    result.PublishDate = data.PublishDate;
                    result.Status = data.Status;
                    result.DefaultImageId = data.DefaultImageId;
                    result.Quantity = data.Quantity;

                    db.SaveChanges();

                    TempData["ResultMessage"] = String.Format("商品[{0}]成功編輯", data.Name);
                    return RedirectToAction("Index");
                }
            }
            else return View(data);
        }

        // 刪除商品 POST處理
        [HttpPost]
        public ActionResult Delete(int id)
        {
            using (Models.TESTEntities db = new Models.TESTEntities())
            {
                var result = (from product in db.ProductsEFs where product.Id == id select product).FirstOrDefault();

                if (result != default(Models.ProductsEF))
                {
                    db.ProductsEFs.Remove(result);
                    db.SaveChanges();

                    TempData["ResultMessage"] = String.Format("商品[{0}]成功刪除", result.Name);
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["ResultMessage"] = "指定資料不存在，無法刪除，請重新操作";
                    return RedirectToAction("Index");
                }
            }
        }
    }
}