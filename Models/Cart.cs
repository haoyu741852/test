using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Carts.Models;

namespace Carts.Models
{
    [Serializable]
    public class Cart : IEnumerable<CartItem>
    {
        public Cart()
        {
            this.cartItems = new List<CartItem>();
        }

        private List<CartItem> cartItems;

        public int Count
        {
            get
            {
                return this.cartItems.Count;
            }
        }

        public decimal TotalAmount
        {
            get
            {
                decimal tempAmount = 0;
                foreach (var cartItem in cartItems)
                {
                    tempAmount = tempAmount + cartItem.Amount;
                }

                return tempAmount;
            }
        }

        public bool AddProduct(int ProductId)
        {
            var findItem = this.cartItems.Where(s => s.Id == ProductId).Select(s => s).FirstOrDefault();

            if (findItem == default(CartItem))
            {
                using (TESTEntities db = new TESTEntities())
                {
                    var data = (from product in db.ProductsEFs where product.Id == ProductId select product).FirstOrDefault();
                    if (data != default(ProductsEF))
                    {
                        this.AddProduct(data);
                    }
                }
            }
            else
            {
                findItem.Quantity += 1;
            }

            return true;
        }
        public bool AddProduct(ProductsEF product)
        {
            var cartItem = new CartItem {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Quantity =1
            };

            this.cartItems.Add(cartItem);

            return true;
        }

        public bool RemoveProduct(int ProductId)
        {
            var findItem = this.cartItems.Where(s => s.Id == ProductId).Select(s => s).FirstOrDefault();

            if (findItem == default(CartItem))
            {
                //不存在 不動作
            }
            else
            {
                //移除商品
                this.cartItems.Remove(findItem);
            }

            return true;
        }

        public bool ClearCart()
        {
            this.cartItems.Clear();

            return true;
        }

        IEnumerator<CartItem> IEnumerable<CartItem>.GetEnumerator()
        {
            return this.cartItems.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}