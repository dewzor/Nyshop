﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Webshop.Data;
using Webshop.Models;
using Webshop.ViewModels;

namespace Webshop.Services
{
    public class ShoppingCart
    {

        private readonly StoreContext _db;
        private readonly string _cartId;

        public ShoppingCart(HttpContextBase context) 
            : this(context, new StoreContext())
        {
            
        }
        public ShoppingCart(HttpContextBase httpContext, StoreContext storeContext)
        {
            _db = storeContext;
            _cartId = GetCartId(httpContext);
        }

        public async Task AddAsync(int productId)
        {
            var product = await _db.Products
                .SingleOrDefaultAsync(p => p.ProductId == productId);

            if (product == null)
            {
                // THrow exception later
                return;
            }

            var cartItem = await _db.CartItems
                .SingleOrDefaultAsync(c => c.ProductId == productId && c.CartId == _cartId);

            if (cartItem != null)
            {
                cartItem.Count++;
            }
            else
            {
                cartItem = new CartItem
                {
                    ProductId = productId,
                    CartId = _cartId,
                    Count = 1,
                    DateCreated = DateTime.Now
                };

                _db.CartItems.Add(cartItem);
            }
            await _db.SaveChangesAsync();
        }

        public async Task<int> RemoveAsync(int productId)
        {
            var cartItem = await _db.CartItems
                .SingleOrDefaultAsync(c => c.ProductId == productId && c.CartId == _cartId);

            var itemCount = 0;

            if (cartItem == null)
            {
                return itemCount;
            }

            if (cartItem.Count > 1)
            {
                cartItem.Count--;
                itemCount = cartItem.Count;
            }

            else
            {
                _db.CartItems.Remove(cartItem);
            }

            await _db.SaveChangesAsync();
            return itemCount;
        }

        public async Task<IEnumerable<CartItem>> GetCartItemsAsync()
        {
            return await _db.CartItems.Include("Product")
                .Where(c => c.CartId == _cartId).ToArrayAsync();
        }

        public async Task<PaymentResult> CheckoutAsync(CheckoutViewModel model)
        {
            var items = await GetCartItemsAsync();
            var order = new Order()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Address = model.Address,
                City = model.City,
                State = model.State,
                PostalCode = model.PostalCode,
                Country = model.Country,
                Phone = model.Phone,
                Email = model.Email,
                OrderDate = DateTime.Now
            };

            foreach (var item in items)
            {
                var detail = new OrderDetail()
                {
                    ProductId = item.ProductId,
                    UnitPrice = item.Product.Price,
                    Quantity = item.Count
                };

                order.Total += (item.Product.Price*item.Count);

                order.OrderDetails.Add(detail);
            }

            model.Total = order.Total;

            var gateway = new BtreeGateway();
            var result = gateway.ProcessPayment(model);

            if (result.Succeeded)
            {
                order.TransactionId = result.TransactionId;
                _db.Orders.Add(order);
                _db.CartItems.RemoveRange(items);
                await _db.SaveChangesAsync();
            }

            //TODO Assign transaction id


            //Följande görs bara om ordern authorizas
            
            return result;

        }

    private string GetCartId(HttpContextBase http)
        {
            var cookie = http.Request.Cookies.Get("ShoppingCart"); // Looks if httpcontextbase holds a cookie called ShoppingCart
            var cartId = string.Empty;

            if (cookie == null || string.IsNullOrWhiteSpace(cookie.Value)) // If ShoppingCart cookie does not exist, this creates one.
            {
                cookie = new HttpCookie("ShoppingCart");
                cartId = Guid.NewGuid().ToString(); // Is a globally unique identifyer

                cookie.Value = cartId;
                cookie.Expires = DateTime.Now.AddDays(7);

                http.Response.Cookies.Add(cookie);
            }

            else
            {
                cartId = cookie.Value;
            }

            return cartId;
        }
    }
}