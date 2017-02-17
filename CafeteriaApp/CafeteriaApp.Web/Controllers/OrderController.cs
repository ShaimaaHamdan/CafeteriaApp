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
    [RoutePrefix("api/Order")]
    public class OrderController : ApiController
    {
        public AppDb appdb = new AppDb();

        public IHttpActionResult Get()
        {
            //lamda expression
            var orders = appdb.Orders.Select(order => new OrderViewModel()
            {
                Id = order.Id,
                OrderTime = order.OrderTime,
                OrderStatus = order.OrderStatus,
                DeliveryTime = order.DeliveryTime,
                DeliveryPlace = order.DeliveryPlace,
                PaymentMethod = order.PaymentMethod,
                PaymentDone = order.PaymentDone,
                customerid = order.CustomerId,
                customer = new CustomerViewModel()
                {
                   Id = order.Customer.Id,
                   Credit = order.Customer.Credit,
                   LimitedCredit = order.Customer.LimitedCredit,
                   UserId = order.Customer.UserId

                }                

            }).ToList();

            return Ok(orders);

        }

        public IHttpActionResult Get(int id)
        {
            var order = appdb.Orders.FirstOrDefault(o => o.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            OrderViewModel model = new OrderViewModel()
            {

                Id = order.Id,
                OrderTime = order.OrderTime,
                OrderStatus = order.OrderStatus,
                DeliveryTime = order.DeliveryTime,
                DeliveryPlace = order.DeliveryPlace,
                PaymentMethod = order.PaymentMethod,
                PaymentDone = order.PaymentDone,
                customerid= order.CustomerId,
                customer = new CustomerViewModel()
                {
                    Id = order.Customer.Id,
                    Credit = order.Customer.Credit,
                    LimitedCredit = order.Customer.LimitedCredit,
                    UserId = order.Customer.UserId

                }
            };


            return Ok(model);
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var orderToDelete = appdb.Orders.FirstOrDefault(o => o.Id == id);
            if (orderToDelete != null)
            {
                appdb.Orders.Remove(orderToDelete);
                appdb.SaveChanges();
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public IHttpActionResult Add(OrderViewModel order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data.");
            }

            var o = appdb.Orders.Add(new Order()
            {
                Id = order.Id,
                OrderTime = order.OrderTime,
                OrderStatus = order.OrderStatus,
                DeliveryTime = order.DeliveryTime,
                DeliveryPlace = order.DeliveryPlace,
                PaymentMethod = order.PaymentMethod,
                PaymentDone = order.PaymentDone,
                CustomerId = order.customerid,
            });
            appdb.SaveChanges();
            return Ok();
        }

        [HttpPut]
        public IHttpActionResult Put(OrderViewModel order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Not a valid data");
            }

            var existingOrder = appdb.Orders.Where(x => x.Id == order.Id).FirstOrDefault<Order>();

            if (existingOrder != null)
            {
                existingOrder.Id = order.Id;
                existingOrder.OrderStatus = order.OrderStatus;
                existingOrder.OrderTime = order.OrderTime;
                existingOrder.DeliveryTime = order.DeliveryTime;
                existingOrder.DeliveryPlace = order.DeliveryPlace;
                existingOrder.PaymentMethod = order.PaymentMethod;
                existingOrder.PaymentDone = order.PaymentDone;
                
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
