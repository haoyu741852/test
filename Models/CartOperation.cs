using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Carts.Models;

namespace Carts.Models
{
    public static class CartOperation
    {
        [WebMethod(EnableSession = true)] //啟用Session
        public static Cart GetCurrentCart()
        {
            if (HttpContext.Current != null)
            {
                if (HttpContext.Current.Session["Cart"] == null) HttpContext.Current.Session["Cart"] = new Cart();

                return (Cart)HttpContext.Current.Session["Cart"];
            }
            else
            {
                throw new InvalidOperationException("HttpContext.Current is null");
            }
        }
    }
}