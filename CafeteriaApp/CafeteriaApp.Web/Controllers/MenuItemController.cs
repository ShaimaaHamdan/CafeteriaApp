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

    [RoutePrefix("api/MenuItem")]
    public class MenuItemController : ApiController
    {
        public AppDb appdb = new AppDb();

        public IHttpActionResult Get()
        {
            //lamda expression
            var menuItems = appdb.MenuItems.Select(menuitem => new MenuItemViewModel()
            {
                CategoryId = menuitem.CategoryId,
                Description = menuitem.Description,
                Id = menuitem.Id,
                Name = menuitem.Name,
                Price = menuitem.Price,
                Type = menuitem.Type,
                Category = new CategoryViewModel()
                {
                    Name = menuitem.Category.Name,
                    Id = menuitem.Category.Id,
                    CafeteriaId = menuitem.Category.CafeteriaId,
                }
            }).ToList();

            return Ok(new { menuItems = menuItems });

        }

        public IHttpActionResult Get(int id)
        {
            var menuitem = appdb.MenuItems.FirstOrDefault(m => m.Id == id);
            if (menuitem == null)
            {
                return NotFound();
            }

            MenuItemViewModel model = new MenuItemViewModel()
            {
                CategoryId = menuitem.CategoryId,
                Description = menuitem.Description,
                Id = menuitem.Id,
                Name = menuitem.Name,
                Price = menuitem.Price,
                Type = menuitem.Type,
                Category = new CategoryViewModel()
                {
                    Name = menuitem.Category.Name,
                    Id = menuitem.Category.Id,
                    CafeteriaId = menuitem.Category.CafeteriaId,
                }


            };


            return Ok(new { menuitem =model});
        }

        [Route("GetByCategory/{id}")]
        public IHttpActionResult GetByCategory(int id)
        {
            //lamda expression
            var menuItems = appdb.MenuItems.Where(item => item.CategoryId == id).Select(menuitem => new MenuItemViewModel()
            {
                CategoryId = menuitem.CategoryId,
                Description = menuitem.Description,
                Id = menuitem.Id,
                Name = menuitem.Name,
                Price = menuitem.Price,
                Type = menuitem.Type,
                Category = new CategoryViewModel()
                {
                    Name = menuitem.Category.Name,
                    Id = menuitem.Category.Id,
                    CafeteriaId = menuitem.Category.CafeteriaId,
                }
            }).ToList();

            return Ok(new { menuItems =  menuItems });

        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var menuItemToDelete = appdb.MenuItems.FirstOrDefault(m => m.Id == id);
            if (menuItemToDelete != null)
            {
                appdb.MenuItems.Remove(menuItemToDelete);
                appdb.SaveChanges();
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
        [HttpPost]
        public IHttpActionResult Add(MenuItemViewModel menuitem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data.");
            }

            var m = appdb.MenuItems.Add(new MenuItem()
            {

                CategoryId = menuitem.CategoryId,
                Description = menuitem.Description,
                Id = menuitem.Id,
                Name = menuitem.Name,
                Price = menuitem.Price,
                Type = menuitem.Type,
            });
            appdb.SaveChanges();
            return Ok();
        }

        [HttpPut]
        public IHttpActionResult Put(MenuItemViewModel menuitem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Not a valid data");
            }

            var existingMenuitem = appdb.MenuItems.Where(x => x.Id == menuitem.Id).FirstOrDefault<MenuItem>();

            if (existingMenuitem != null)
            {
                existingMenuitem.Id = menuitem.Id;
                existingMenuitem.Name = menuitem.Name;
                existingMenuitem.Type = menuitem.Type;
                existingMenuitem.Price = menuitem.Price;
                existingMenuitem.Description = menuitem.Description;
                //  existingMenuitem.CategoryId = menuitem.CategoryId;
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




