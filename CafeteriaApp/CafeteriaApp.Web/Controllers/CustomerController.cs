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
                    },



                })
                .ToList();

            return Ok(new {customers = customers});

        }

        public IHttpActionResult Get(string id)
        {
            var customer = appdb.Customers.FirstOrDefault(c => c.UserId == id);
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
                    ImageData = customer.User.Image,
                },
                Dependents = customer.Dependents.Select(i => new DependentViewModel()
                    {
                        Id = i.Id,
                        Name = i.Name,
                        Age = i.Age,
                        ImageData = i.Image,
                        SchoolYear = i.SchoolYear,
                        DependentCredit = i.DependentCredit,
                        DayLimit = i.DayLimit,

                    })
                    .ToList(),

                Favourites = customer.Favourites.Select(i => new MenuItemViewModel()
                    {
                        Id = i.Id,
                        Name = i.Name,
                        Price = i.Price,
                        Description = i.Description,
                        Type = i.Type,
                        ImageData = i.Image,

                    })
                    .ToList(),
                Restricts = customer.Restricts.Select(i => new MenuItemViewModel()
                    {
                        Id = i.Id,
                        Name = i.Name,
                        Price = i.Price,
                        Description = i.Description,
                        Type = i.Type,
                        ImageData = i.Image,

                    })
                    .ToList()
            };

            return Ok(new {customer = model});
        }

        [Route("GetDependentByCustomer/{id}")]
        public IHttpActionResult GetDependentByCustomer(string id)
        {

            var customer = appdb.Customers.FirstOrDefault(c => c.UserId == id);
            var dependents = appdb.Dependents.Where(item => item.Customer.UserId == id)
                .Select(dependent => new DependentViewModel()
                {
                    Name = dependent.Name,
                    Age = dependent.Age,
                    Id = dependent.Id,
                    ImageData = dependent.Image,
                    SchoolYear = dependent.SchoolYear,
                    DayLimit = dependent.DayLimit,
                    DependentCredit = dependent.DependentCredit,

                })
                .ToList();

            return Ok(new
            {
                dependents = dependents,
                customer = new CustomerViewModel()
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
                    
                }
            });
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
                
                existingcustomer.Credit = customer.Credit;
                existingcustomer.User.FirstName = customer.User.FirstName;
                existingcustomer.User.LastName = customer.User.LastName;
                existingcustomer.User.PhoneNumber = customer.User.PhoneNumber;
                existingcustomer.User.Image = customer.User.ImageData;
                appdb.SaveChanges();
            }
            else
            {
                return NotFound();
            }


            return Ok();
        }

        [Route("GetDependentById/{id}")]
        public IHttpActionResult GetDependentById(int id)
        {
            var dependent = appdb.Dependents.FirstOrDefault(c => c.Id == id);

            if (dependent == null)
                return NotFound();

            var model = new DependentViewModel()
            {
                Name = dependent.Name,
                Age = dependent.Age,
                Id = dependent.Id,
                ImageData = dependent.Image,
                SchoolYear = dependent.SchoolYear,
                DayLimit = dependent.DayLimit,
                DependentCredit = dependent.DependentCredit,
                CustomerId = dependent.CustomerId,
                Restricts = dependent.Restricts.Select(i=>new MenuItemViewModel()
                {
                    Id = i.Id,
                    Name = i.Name,
                    Price = i.Price,
                    Type = i.Type,
                    Description = i.Description
                }).AsQueryable().ToList()
            };

            return Ok(new { dependent = model });
        }

        [HttpPost]
        [Route("addDependents")]
        public IHttpActionResult AddDependents(DependentViewModel dependent)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data.");
            }

            var d = appdb.Dependents.Add(new Dependent()
            {
                Name = dependent.Name,
                Age = dependent.Age,
                Id = dependent.Id,
                Image = dependent.ImageData,
                SchoolYear = dependent.SchoolYear,
                DayLimit = dependent.DayLimit,
                DependentCredit = dependent.DependentCredit,
                CustomerId = dependent.CustomerId
            });
            appdb.SaveChanges();
            return Ok();
        }


        [HttpPut]
        [Route("editDependent")]
        public IHttpActionResult EditDependents(DependentViewModel dependent)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data.");
            }

            var dependentToUpdate = appdb.Dependents.FirstOrDefault(d => d.Id == dependent.Id);
            if (dependentToUpdate != null)
            {
                dependentToUpdate.Age = dependent.Age;
                dependentToUpdate.DayLimit = dependent.DayLimit;
                dependentToUpdate.DependentCredit = dependent.DependentCredit;
                dependentToUpdate.Image = dependent.ImageData;
                dependentToUpdate.Name = dependent.Name;
                dependentToUpdate.SchoolYear = dependent.SchoolYear;
            }

            appdb.SaveChanges();
            return Ok();
        }


        [HttpDelete]
        [Route("DeleteCustomerDependent")]
        public IHttpActionResult DeleteCustomerDependent(int id)
        {
            var dependentToDelete = appdb.Dependents.FirstOrDefault(d => d.Id == id);
            if (dependentToDelete != null)
            {
                appdb.Dependents.Remove(dependentToDelete);
                appdb.SaveChanges();
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        [Route("addDependentRestricts")]
        public IHttpActionResult AddDependentRestricts([FromBody]DependentViewModel dependent)
        {

            var dependentObject = appdb.Dependents.FirstOrDefault(i => i.Id == dependent.Id);
            if (dependentObject != null)
            {
                foreach (var restrict in dependent.Restricts)
                {
                    var restrictToAdd = appdb.MenuItems.FirstOrDefault(ad => ad.Id == restrict.Id);
                    dependentObject.Restricts.Add(restrictToAdd);
                }

            }

            appdb.SaveChanges();
            return Ok();
        }
        [HttpDelete]
        [Route("DeleteDependentRestricts")]
        public IHttpActionResult DeleteDependentRestricts(DependentRestrictViewModel model)
        {
            var dependent = appdb.Dependents.FirstOrDefault(m => m.Id == model.DependentId);
            if (dependent != null)
            {
                var restrictToRemove = appdb.MenuItems.FirstOrDefault(i => i.Id == model.MenuItemId);
                dependent.Restricts.Remove(restrictToRemove);
                appdb.SaveChanges();
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }


        [HttpPost]
        [Route("addFavourites")]
        public IHttpActionResult AddFavourites([FromBody]CustomerViewModel customer)
        {

            var customerObject = appdb.Customers.FirstOrDefault(i => i.Id == customer.Id);
            if (customerObject != null)
            {
                foreach (var favourite in customer.Favourites)
                {
                    var favouriteToAdd = appdb.MenuItems.FirstOrDefault(ad => ad.Id == favourite.Id);
                    customerObject.Favourites.Add(favouriteToAdd);
                }

            }

            appdb.SaveChanges();
            return Ok();
        }

        [HttpDelete]
        [Route("DeleteCustomerFavourites")]
        public IHttpActionResult DeleteCustomerFavourites(CustomerFavouriteViewModel model)
        {
            var customer = appdb.Customers.FirstOrDefault(m => m.Id == model.CustomerId);
            if (customer != null)
            {
                var favouriteToRemove = appdb.MenuItems.FirstOrDefault(i => i.Id == model.MenuItemId);
                customer.Favourites.Remove(favouriteToRemove);
                appdb.SaveChanges();
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        [Route("addRestricts")]
        public IHttpActionResult AddRestricts([FromBody]CustomerViewModel customer)
        { 

            var customerObject = appdb.Customers.FirstOrDefault(i => i.Id == customer.Id);
            if (customerObject != null)
            {
                foreach (var restrict in customer.Restricts)
                {
                    var restrictToAdd = appdb.MenuItems.FirstOrDefault(ad => ad.Id == restrict.Id);
                    customerObject.Restricts.Add(restrictToAdd);
                }

            }

            appdb.SaveChanges();
            return Ok();
        }

        [HttpDelete]
        [Route("DeleteCustomerRestricts")]
        public IHttpActionResult DeleteCustomerRestricts(CustomerRestrictViewModel model)
        {
            var customer = appdb.Customers.FirstOrDefault(m => m.Id == model.CustomerId);
            if (customer != null)
            {
                var restrictToRemove = appdb.MenuItems.FirstOrDefault(i => i.Id == model.MenuItemId);
                customer.Restricts.Remove(restrictToRemove);
                appdb.SaveChanges();
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

    }

    public class CustomerFavouriteViewModel
    {
        public int MenuItemId { get; set; }
        public int CustomerId { get; set; } 
    }
    public class CustomerRestrictViewModel 
    {
        public int MenuItemId { get; set; }
        public int CustomerId { get; set; }
    }
    public class DependentRestrictViewModel
    {
        public int MenuItemId { get; set; }
        public int DependentId { get; set; }
    }
}


