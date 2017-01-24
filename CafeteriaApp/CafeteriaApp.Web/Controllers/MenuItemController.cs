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

            return Ok(menuItems);

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


            return Ok(model);
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
    }
}
