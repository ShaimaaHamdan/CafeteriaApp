using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CafeteriaApp.Data.Contexts;
using CafeteriaApp.Data.Models;
using CafeteriaApp.Web.Models;

namespace CafeteriaApp.Web.Controllers
{
    [RoutePrefix("api/OrderItem")]
    public class OrderItemController : ApiController
    {
        public AppDb appdb = new AppDb();

        public IHttpActionResult Get()
        {
            //lamda expression
            var orderItems = appdb.OrderItems.Select(orderitem => new OrderItemViewModel()
            {
                Id = orderitem.Id,
                Quantity = orderitem.Quantity,
                MenuItemId = orderitem.MenuItemId,
                OrderId=orderitem.OrderId,
                Order = new OrderViewModel()
                {
                    Id = orderitem.Order.Id,
                    OrderTime = orderitem.Order.OrderTime,
                    OrderStatus = orderitem.Order.OrderStatus,
                    DeliveryTime = orderitem.Order.DeliveryTime,
                    DeliveryPlace = orderitem.Order.DeliveryPlace,
                    PaymentMethod = orderitem.Order.PaymentMethod,
                    PaymentDone = orderitem.Order.PaymentDone,
                    customerid = orderitem.Order.CustomerId                    
                   
                },
                MenuItem = new MenuItemViewModel()
                {
                    Id = orderitem.MenuItem.Id,
                    Name = orderitem.MenuItem.Name,
                    Description = orderitem.MenuItem.Description,
                    Price = orderitem.MenuItem.Price,
                    Type = orderitem.MenuItem.Type,
                    CategoryId = orderitem.MenuItem.CategoryId
                }                            
                
            }).ToList();

            return Ok(orderItems);

        }

        public IHttpActionResult Get(int id)
        {
            var orderitem = appdb.OrderItems.FirstOrDefault(o => o.Id == id);
            if (orderitem == null)
            {
                return NotFound();
            }

            OrderItemViewModel model = new OrderItemViewModel()
            {

                Id = orderitem.Id,
                Quantity = orderitem.Quantity,
                MenuItemId = orderitem.MenuItemId,
                OrderId = orderitem.OrderId,
                Order = new OrderViewModel()
                {
                    Id = orderitem.Order.Id,
                    OrderTime = orderitem.Order.OrderTime,
                    OrderStatus = orderitem.Order.OrderStatus,
                    DeliveryTime = orderitem.Order.DeliveryTime,
                    DeliveryPlace = orderitem.Order.DeliveryPlace,
                    PaymentMethod = orderitem.Order.PaymentMethod,
                    PaymentDone = orderitem.Order.PaymentDone,
                    customerid = orderitem.Order.CustomerId

                },
                MenuItem = new MenuItemViewModel()
                {
                    Id = orderitem.MenuItem.Id,
                    Name = orderitem.MenuItem.Name,
                    Description = orderitem.MenuItem.Description,
                    Price = orderitem.MenuItem.Price,
                    Type = orderitem.MenuItem.Type,
                    CategoryId = orderitem.MenuItem.CategoryId
                }

            };


            return Ok(model);
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var orderItemToDelete = appdb.OrderItems.FirstOrDefault(o => o.Id == id);
            if (orderItemToDelete != null)
            {
                appdb.OrderItems.Remove(orderItemToDelete);
                appdb.SaveChanges();
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        //[Route("AddToCart")]
        public IHttpActionResult Add(OrderItemViewModel orderitem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data.");
            }

            Order order;

            if(orderitem.OrderId == 0)
            {
                order = new Order()
                {
                    OrderTime = DateTime.Now,
                    CustomerId = orderitem.CustomerId,
                    DeliveryTime = DateTime.Now,
                    PaymentMethod = "Test",
                    PaymentDone = true,
                    DeliveryPlace = "test",
                    OrderStatus = "TEEST"
                };

                appdb.Orders.Add(order);
                appdb.SaveChanges();
            }
            else
            {
                order = appdb.Orders.FirstOrDefault(i => i.Id == orderitem.OrderId);
            }

            var m = appdb.OrderItems.Add(new OrderItem()
            {

                Id = orderitem.Id,
                Quantity = orderitem.Quantity,
                MenuItemId = orderitem.MenuItemId,
                OrderId = order.Id,
            });
            appdb.SaveChanges();
            return Ok();
        }

        [HttpPut]
        public IHttpActionResult Put(OrderItemViewModel orderitem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Not a valid data");
            }

            var existingOrderitem = appdb.OrderItems.Where(x => x.Id == orderitem.Id).FirstOrDefault<OrderItem>();

            if (existingOrderitem != null)
            {
                existingOrderitem.Id = orderitem.Id;
                existingOrderitem.Quantity = orderitem.Quantity;
                
                appdb.SaveChanges();
            }
            else
            {
                return NotFound();
            }


            return Ok();
        }
    }
}
