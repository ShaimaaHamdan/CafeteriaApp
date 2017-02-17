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
    [RoutePrefix("api/Customer")]
    public class CustomerController : ApiController
    {
        public AppDb appdb = new AppDb();

        public IHttpActionResult Get()
        {
            //lamda expression
            var customers = appdb.Customers.Select(customer => new CustomerViewModel()
            {
                Id = customer.Id,
                Credit = customer.Credit,
                LimitedCredit = customer.LimitedCredit,
                UserId = customer.UserId,
                User = new UserViewModel()
                {
                    Id = customer.User.Id,
                    UserName = customer.User.UserName,
                    FirstName = customer.User.FirstName,
                    LastName = customer.User.LastName,
                    Email = customer.User.Email,
                    EmailConfirmed = customer.User.EmailConfirmed,
                    PhoneNumber = customer.User.PhoneNumber,
                    PhoneNumberConfirmed = customer.User.PhoneNumberConfirmed,
                    PasswordHash = customer.User.PasswordHash,
                    SecurityStamp = customer.User.SecurityStamp,
                    TwoFactorEnabled = customer.User.TwoFactorEnabled,
                    LockoutEndDateUtc = customer.User.LockoutEndDateUtc,
                    LockoutEnabled = customer.User.LockoutEnabled,
                    AccessFailedCount = customer.User.AccessFailedCount,
                }

            }).ToList();

            return Ok(customers);

        }

        public IHttpActionResult Get(int id)
        {
            var customer = appdb.Customers.FirstOrDefault(c => c.Id == id);
            if (customer == null)
            {
                return NotFound();
            }

            CustomerViewModel model = new CustomerViewModel()
            {
                Id = customer.Id,
                Credit = customer.Credit,
                LimitedCredit = customer.LimitedCredit,
                UserId = customer.UserId,
                User = new UserViewModel()
                {
                    Id = customer.User.Id,
                    UserName = customer.User.UserName,
                    FirstName = customer.User.FirstName,
                    LastName = customer.User.LastName,
                    Email = customer.User.Email,
                    EmailConfirmed = customer.User.EmailConfirmed,
                    PhoneNumber = customer.User.PhoneNumber,
                    PhoneNumberConfirmed = customer.User.PhoneNumberConfirmed,
                    PasswordHash = customer.User.PasswordHash,
                    SecurityStamp = customer.User.SecurityStamp,
                    TwoFactorEnabled = customer.User.TwoFactorEnabled,
                    LockoutEndDateUtc = customer.User.LockoutEndDateUtc,
                    LockoutEnabled = customer.User.LockoutEnabled,
                    AccessFailedCount = customer.User.AccessFailedCount,
                }

            };


            return Ok(model);
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var customerToDelete = appdb.Customers.FirstOrDefault(c => c.Id == id);
            if (customerToDelete != null)
            {
                appdb.Customers.Remove(customerToDelete);
                appdb.SaveChanges();
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public IHttpActionResult Add(CustomerViewModel customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data.");
            }

            var c = appdb.Customers.Add(new Customer()
            {
                Id = customer.Id,
                Credit = customer.Credit,
                LimitedCredit = customer.LimitedCredit,
                UserId = customer.UserId,               
            });
            appdb.SaveChanges();
            return Ok();
        }

        [HttpPut]
        public IHttpActionResult Put(CustomerViewModel customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Not a valid data");
            }

            var existingcustomer = appdb.Customers.Where(x => x.Id == customer.Id).FirstOrDefault<Customer>();

            if (existingcustomer != null)
            {
                existingcustomer.Id = customer.Id;
                existingcustomer.Credit = customer.Credit;
                existingcustomer.LimitedCredit = customer.LimitedCredit;
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
