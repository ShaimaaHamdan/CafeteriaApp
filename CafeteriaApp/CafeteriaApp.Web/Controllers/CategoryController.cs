using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CafeteriaApp.Data.Contexts;
using CafeteriaApp.Data.Models;

namespace CafeteriaApp.Web.Controllers
{
    public class CategoryController : ApiController
    {
        public AppDb appdb = new AppDb();
        public IHttpActionResult GetCategory(int id)
        {
            var category = appdb.Categories.FirstOrDefault(c => c.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }

        public ICollection<Category> GetAllCategories()
        {
            return appdb.Categories.ToList();
        }

        public IHttpActionResult AddCategory(Category c)
        {
            if (c != null)
            {
                appdb.Categories.Add(c);
                appdb.SaveChanges();
                return Ok(c);
            }
            else
            {
                return BadRequest();
            }
        }

        public IHttpActionResult DeleteCategory(int id)
        {
            var categoryToDelete = appdb.Categories.FirstOrDefault(c => c.Id == id);
            if (categoryToDelete != null)
            {
                appdb.Categories.Remove(categoryToDelete);
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
