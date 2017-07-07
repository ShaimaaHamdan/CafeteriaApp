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
                OrderItems = order.OrderItems.Select(orderitem => new OrderItemViewModel
                {
                    Id = orderitem.Id,
                    Quantity = orderitem.Quantity,
                    MenuItemId = orderitem.MenuItemId,
                    OrderId = orderitem.OrderId,
                    MenuItem = new MenuItemViewModel()
                    {
                        Id = orderitem.MenuItem.Id,
                        Name = orderitem.MenuItem.Name,
                        Description = orderitem.MenuItem.Description,
                        Price = orderitem.MenuItem.Price,
                        Type = orderitem.MenuItem.Type,
                        CategoryId = orderitem.MenuItem.CategoryId
                    }

                }).ToList(),
                //OrderItems =(new OrderItemViewModel() {
                //})
                customer = new CustomerViewModel()
                {
                   Id = order.Customer.Id,
                   Credit = order.Customer.Credit,
                   LimitedCredit = order.Customer.LimitedCredit,
                   UserId = order.Customer.UserId

                }
               
                          

            }).ToList();

            return Ok(new { orders = orders});

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
                OrderItems = order.OrderItems?.Select(orderitem => new OrderItemViewModel
                {
                    Id = orderitem.Id,
                    Quantity = orderitem.Quantity,
                    MenuItemId = orderitem.MenuItemId,
                    OrderId = orderitem.OrderId,
                    MenuItem = new MenuItemViewModel()
                    {
                        Id = orderitem.MenuItem.Id,
                        Name = orderitem.MenuItem.Name,
                        Description = orderitem.MenuItem.Description,
                        Price = orderitem.MenuItem.Price,
                        Type = orderitem.MenuItem.Type,
                        CategoryId = orderitem.MenuItem.CategoryId
                    }

                }).ToList(),
                customer = new CustomerViewModel()
                {
                    Id = order.Customer.Id,
                    Credit = order.Customer.Credit,
                    LimitedCredit = order.Customer.LimitedCredit,
                    UserId = order.Customer.UserId

                }
            };


            return Ok(new { order = model});
        }

        [HttpGet]
        [Route("GetbyCustomerId/{id}")]
        public IHttpActionResult GetbyCustomerId(int id)
        {
            var order = appdb.Orders.Where(o => o.CustomerId == id).OrderByDescending(i=>i.Id).FirstOrDefault();
            if (order == null)
            {
                return Ok(new {  });
            }


            var model = new OrderViewModel()
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

                },
                OrderItems = order.OrderItems?.Select(orderitem => new OrderItemViewModel {
                    Id = orderitem.Id,
                    Quantity = orderitem.Quantity,
                    MenuItemId = orderitem.MenuItemId,
                    OrderId = orderitem.OrderId,
                    MenuItem = new MenuItemViewModel()
                    {
                        Id = orderitem.MenuItem.Id,
                        Name = orderitem.MenuItem.Name,
                        Description = orderitem.MenuItem.Description,
                        Price = orderitem.MenuItem.Price,
                        Type = orderitem.MenuItem.Type,
                        CategoryId = orderitem.MenuItem.CategoryId
                    }

                }).ToList()
            };


            return Ok(new { order = model });
        }
        [HttpGet]
        [Route("GetAllbyCustomerId/{id}")]
        public IHttpActionResult GetAllbyCustomerId(int id)
        {
            var orders1 = appdb.Orders.Where(o => o.CustomerId == id).ToList();
            var orders = orders1.Select(order => new OrderViewModel()
            {
                Id = order.Id,
                OrderTime = order.OrderTime,
                OrderStatus = order.OrderStatus,
                DeliveryTime = order.DeliveryTime,
                DeliveryPlace = order.DeliveryPlace,
                PaymentMethod = order.PaymentMethod,
                PaymentDone = order.PaymentDone,
                customerid = order.CustomerId,
                OrderItems = order.OrderItems.Select(orderitem => new OrderItemViewModel
                {
                    Id = orderitem.Id,
                    Quantity = orderitem.Quantity,
                    MenuItemId = orderitem.MenuItemId,
                    OrderId = orderitem.OrderId,
                    MenuItem = new MenuItemViewModel()
                    {
                        Id = orderitem.MenuItem.Id,
                        Name = orderitem.MenuItem.Name,
                        Description = orderitem.MenuItem.Description,
                        Price = orderitem.MenuItem.Price,
                        Type = orderitem.MenuItem.Type,
                        CategoryId = orderitem.MenuItem.CategoryId
                    }

                }).ToList(),
                customer = new CustomerViewModel()
                {
                    Id = order.Customer.Id,
                    Credit = order.Customer.Credit,
                    LimitedCredit = order.Customer.LimitedCredit,
                    UserId = order.Customer.UserId

                }



            }).ToList();

            return Ok(new { orders = orders });
        }
        [HttpGet]
        [Route("Getlastorder/{id}")]
        public IHttpActionResult Getlastorder(int id)
        {
            var orders = appdb.Orders.Where(o => o.CustomerId == id).ToList();
            var order = orders[orders.Count() - 1];
            if (order == null)
            {
                return Ok(new { });
            }


            var model = new OrderViewModel()
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

                },
                OrderItems = order.OrderItems?.Select(orderitem => new OrderItemViewModel
                {
                    Id = orderitem.Id,
                    Quantity = orderitem.Quantity,
                    MenuItemId = orderitem.MenuItemId,
                    OrderId = orderitem.OrderId,
                    MenuItem = new MenuItemViewModel()
                    {
                        Id = orderitem.MenuItem.Id,
                        Name = orderitem.MenuItem.Name,
                        Description = orderitem.MenuItem.Description,
                        Price = orderitem.MenuItem.Price,
                        Type = orderitem.MenuItem.Type,
                        CategoryId = orderitem.MenuItem.CategoryId
                    }

                }).ToList()
            };


            return Ok(new { order = model });
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
            var z = order;
            if (existingOrder != null)
            {
                existingOrder.Customer.LimitedCredit = order.customer.LimitedCredit;
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
