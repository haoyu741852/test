using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Carts.Models
{
    [Serializable]
    public class CartItem
    {
        //商品編號
        public int Id { get; set; }

        //商品名稱
        public string Name { get; set; }

        //商品價格
        public decimal Price { get; set; }

        //商品數量
        public int Quantity { get; set; }

        //商品小計
        public decimal Amount { get { return this.Price * this.Quantity; } }
    }
}