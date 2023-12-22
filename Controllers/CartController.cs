using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Carts.Models;

namespace Carts.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        public ActionResult Index()
        {
            return View();
        }

        // 取得當前購物車
        public ActionResult GetCart()
        {
            return PartialView("_CartPartial");
        }

        [HttpPost]
        // 加入購物車，並回傳到購物車頁面
        public ActionResult AddToCart(int id)
        {
            var currentCart = CartOperation.GetCurrentCart();
            currentCart.AddProduct(id);

            return PartialView("_CartPartial");
        }

        [HttpPost]
        // 從購物車刪除商品，並回傳到購物車頁面
        public ActionResult RemoveFromCart(int id)
        {
            var currentCart = CartOperation.GetCurrentCart();
            currentCart.RemoveProduct(id);

            return PartialView("_CartPartial");
        }

        [HttpPost]
        // 清空購物車商品，並回傳到購物車頁面
        public ActionResult ClearCart()
        {
            var currentCart = CartOperation.GetCurrentCart();
            currentCart.ClearCart();

            return PartialView("_CartPartial");
        }
    }
}