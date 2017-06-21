using CafeteriaApp.Data.Contexts;
using CafeteriaApp.Data.Models;
using CafeteriaApp.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CafeteriaApp.Web.Controllers
{
    public class OrderNotificationController : ApiController
    {
        public AppDb appdb = new AppDb();
        // GET: api/OrderNotification
        public IHttpActionResult Getbycustomer(int id)
        {
            var notifications = appdb.OrderNotifications.Select(n => new OrderNotificationViewModel()
            {
                Id=n.Id,
                data=n.data,
                customerid=n.customerid,
                orderid=n.orderid,
                customer = new CustomerViewModel()
                {
                    Id = n.customer.Id,
                    Credit = n.customer.Credit,
                    LimitedCredit = n.customer.LimitedCredit,
                    UserId = n.customer.UserId,
                    User = new UserViewModel()
                    {
                        Id = n.customer.User.Id,
                        UserName = n.customer.User.UserName,
                        FirstName = n.customer.User.FirstName,
                        LastName = n.customer.User.LastName,
                        Email = n.customer.User.Email,
                        EmailConfirmed = n.customer.User.EmailConfirmed,
                        PhoneNumber = n.customer.User.PhoneNumber,
                        PhoneNumberConfirmed = n.customer.User.PhoneNumberConfirmed,
                        PasswordHash = n.customer.User.PasswordHash,
                        SecurityStamp = n.customer.User.SecurityStamp,
                        TwoFactorEnabled = n.customer.User.TwoFactorEnabled,
                        LockoutEndDateUtc = n.customer.User.LockoutEndDateUtc,
                        LockoutEnabled = n.customer.User.LockoutEnabled,
                        AccessFailedCount = n.customer.User.AccessFailedCount,
                    },
                },
                order = new OrderViewModel()
                {
                    Id = n.order.Id,
                    OrderTime = n.order.OrderTime,
                    OrderStatus = n.order.OrderStatus,
                    DeliveryTime = n.order.DeliveryTime,
                    DeliveryPlace = n.order.DeliveryPlace,
                    PaymentMethod = n.order.PaymentMethod,
                    PaymentDone = n.order.PaymentDone,
                    customerid = n.order.CustomerId

                },
            }).ToList();

            return Ok(new { notifications = notifications });
        }

        // POST: api/OrderNotification
        [HttpPost]
        public IHttpActionResult Add(OrderNotificationViewModel notification)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data.");
            }


            var m = appdb.OrderNotifications.Add(new OrderNotification()
            {
                Id = notification.Id,
                data=notification.data,
                customerid = notification.customerid,
                orderid = notification.orderid
            });

            appdb.SaveChanges();
            return Ok();
        }

        // DELETE: api/OrderNotification/5
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var notification = appdb.OrderNotifications.FirstOrDefault(o => o.Id == id);
            if (notification != null)
            {
                appdb.OrderNotifications.Remove(notification);
                appdb.SaveChanges();
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
    }
}
