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
    [RoutePrefix("api/Category")]
    public class CategoryController : ApiController
    {

        public AppDb appdb = new AppDb();
        public IHttpActionResult Get()
        {
            var category = appdb.Categories.Select(category1 => new CategoryViewModel()
            {
                CafeteriaId = category1.CafeteriaId,
                Id = category1.Id,
                Name = category1.Name,
                Cafeteria = new CafeteriaViewModel()
                {

                    Name = category1.Cafeteria.Name,
                    Id = category1.Cafeteria.Id,

                }
            }).ToList();

            return Ok(category);
        }
        public IHttpActionResult Get(int id)
        {
            var category = appdb.Categories.FirstOrDefault(c => c.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            CategoryViewModel model = new CategoryViewModel()
            {
                CafeteriaId = category.CafeteriaId,
                Id = category.Id,
                Name = category.Name,
                Cafeteria = new CafeteriaViewModel()
                {
                    Name = category.Cafeteria.Name,
                    Id = category.Cafeteria.Id,

                }
            };
            return Ok(model);


        }

        [Route("GetByCafetria/{id}")]
        public IHttpActionResult GetByCafetria(int id)
        {
            var cafetria = appdb.Cafeterias.FirstOrDefault(c => c.Id == id);
            var categories = appdb.Categories.Where(item => item.CafeteriaId == id).Select(category1 => new CategoryViewModel()
            {
                CafeteriaId = category1.CafeteriaId,
                Id = category1.Id,
                Name = category1.Name,
                Cafeteria = new CafeteriaViewModel()
                {

                    Name = category1.Cafeteria.Name,
                    Id = category1.Cafeteria.Id,

                }
            }).ToList();

            return Ok(new
            {
                categories = categories,
                cafetria = new CafeteriaViewModel()
                {
                    Name = cafetria.Name,
                    Id = cafetria.Id,
                }
            });
        }

        [HttpPost]
        public IHttpActionResult Add(Category c)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data.");
            }
            var m = appdb.Categories.Add(new Category()
            {

                CafeteriaId = c.CafeteriaId,
                Id = c.Id,
                Name = c.Name,

            });
            appdb.SaveChanges();
            return Ok();
        }

        [HttpPut]
        public IHttpActionResult Put(CategoryViewModel c)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Not a valid data");
            }

            var existingCategory = appdb.Categories.Where(x => x.Id == c.Id).FirstOrDefault<Category>();

            if (existingCategory != null)
            {
                existingCategory.Id = c.Id;
                existingCategory.Name = c.Name;

                appdb.SaveChanges();
            }
            else
            {
                return NotFound();
            }

            return Ok();
        }


        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var categoryDeleted = appdb.Categories.FirstOrDefault(d => d.Id == id);

            if (categoryDeleted != null)
            {
                appdb.Categories.Remove(categoryDeleted);
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
