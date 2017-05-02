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
    [RoutePrefix("api/FavoriteItem")]
    public class FavoriteItemController : ApiController
    {
        public AppDb appdb = new AppDb();
        // GET: api/FavoriteItem/GetbyCustomerId/{id}
        [Route("GetbyCustomerId/{id}")]
        public IHttpActionResult GetbyCustomerId(int id)
        {
            var favoriteitems = appdb.FavoriteItems.Where(f => f.CustomerId == id).ToList();
            if (favoriteitems == null)
            {
                return NotFound();
            }
            var favoriteitems1 = favoriteitems.Select(favoriteitem => new FavoriteItemViewModel()
            {
                Id = favoriteitem.Id,
                MenuItemId = favoriteitem.MenuItemId,
                MenuItem = new MenuItemViewModel()
                {
                    Id = favoriteitem.MenuItem.Id,
                    Name = favoriteitem.MenuItem.Name,
                    Description = favoriteitem.MenuItem.Description,
                    Price = favoriteitem.MenuItem.Price,
                    Type = favoriteitem.MenuItem.Type,
                    CategoryId = favoriteitem.MenuItem.CategoryId
                },
                CustomerId = favoriteitem.CustomerId,
                Customer = new CustomerViewModel()
                {
                    Id = favoriteitem.Customer.Id,
                    Credit = favoriteitem.Customer.Credit,
                    LimitedCredit = favoriteitem.Customer.LimitedCredit,
                    UserId = favoriteitem.Customer.UserId
                }

            }).ToList();
            return Ok(new { favoriteitems = favoriteitems1 });
        }

        // GET: api/FavoriteItem/5
        public IHttpActionResult Get(int id)
        {
            var favoriteitem = appdb.FavoriteItems.FirstOrDefault(f => f.Id == id);
            if (favoriteitem == null)
            {
                return NotFound();
            }

            FavoriteItemViewModel model = new FavoriteItemViewModel()
            {

                Id = favoriteitem.Id,
                MenuItemId = favoriteitem.MenuItemId,
                MenuItem = new MenuItemViewModel()
                {
                    Id = favoriteitem.MenuItem.Id,
                    Name = favoriteitem.MenuItem.Name,
                    Description = favoriteitem.MenuItem.Description,
                    Price = favoriteitem.MenuItem.Price,
                    Type = favoriteitem.MenuItem.Type,
                    CategoryId = favoriteitem.MenuItem.CategoryId
                },
                CustomerId = favoriteitem.CustomerId,
                Customer = new CustomerViewModel()
                {
                    Id = favoriteitem.Customer.Id,
                    Credit = favoriteitem.Customer.Credit,
                    LimitedCredit = favoriteitem.Customer.LimitedCredit,
                    UserId = favoriteitem.Customer.UserId
                }

            };

        
            return Ok(new { favoriteitem = model });
        }

        // POST: api/FavoriteItem
        [HttpPost]
        public IHttpActionResult Add(FavoriteItemViewModel favoriteitem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data.");
            }

            var f = appdb.FavoriteItems.Add(new FavoriteItem()
            {
                Id = favoriteitem.Id,
                MenuItemId=favoriteitem.MenuItemId,
                CustomerId = favoriteitem.CustomerId
            });
            appdb.SaveChanges();
            return Ok();
        }
   
        // DELETE: api/FavoriteItem/5
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var favoriteitemtodelete = appdb.FavoriteItems.FirstOrDefault(f => f.Id == id);
            if (favoriteitemtodelete != null)
            {
                appdb.FavoriteItems.Remove(favoriteitemtodelete);
                appdb.SaveChanges();
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
        [HttpDelete]
        [Route("DeleteAll/{id}")]
        public IHttpActionResult DeleteAll(int id) // customerid
        {
            var favoriteitemstodelete = appdb.FavoriteItems.Where(f => f.CustomerId == id);
            if (favoriteitemstodelete != null)
            {
                foreach (FavoriteItem f in favoriteitemstodelete) {
                    appdb.FavoriteItems.Remove(f);
                }
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
