using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CafeteriaApp.Data.Contexts;
using CafeteriaApp.Data.Models;
using CafeteriaApp.Web.Models;
using CafeteriaApp.Web.Helpers;

namespace CafeteriaApp.Web.Controllers
{
    [RoutePrefix("api/Category")]
    public class CategoryController : ApiController
    {
        image_handle image = new image_handle();

        public AppDb appdb = new AppDb();
        public IHttpActionResult Get()
        {
            var categories = appdb.Categories.Select(category1 => new CategoryViewModel()
            {
                CafeteriaId = category1.CafeteriaId,
                Id = category1.Id,
                Name = category1.Name,
                //ImageData = category1.Image,
                ImageUrl = category1.ImageUrl,
                Cafeteria = new CafeteriaViewModel()
                {

                    Name = category1.Cafeteria.Name,
                    Id = category1.Cafeteria.Id,
                  //  ImageData = category1.Cafeteria.Image,
                    ImageUrl = category1.Cafeteria.ImageUrl
                }
            }).ToList();

            return Ok(new { categories = categories });
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
                //ImageData = category.Image,
                ImageUrl = category.ImageUrl,
                Cafeteria = new CafeteriaViewModel()
                {
                    Name = category.Cafeteria.Name,
                    Id = category.Cafeteria.Id,
                  //  ImageData = category.Cafeteria.Image,
                    ImageUrl = category.Cafeteria.ImageUrl
                }
            };
            return Ok(new { category  = model});


        }

        [Route("GetByCafetria/{id}")]
        public IHttpActionResult GetByCafetria(int id)
        {
            var cafetria = appdb.Cafeterias.FirstOrDefault(c => c.Id == id);
            if (cafetria == null)
            {
                return NotFound();
            }
            var categories = appdb.Categories.Where(item => item.CafeteriaId == id).Select(category1 => new CategoryViewModel()
            {
                CafeteriaId = category1.CafeteriaId,
                Id = category1.Id,
                Name = category1.Name,
                //ImageData = category1.Image,
                ImageUrl = category1.ImageUrl,
                Cafeteria = new CafeteriaViewModel()
                {

                    Name = category1.Cafeteria.Name,
                    Id = category1.Cafeteria.Id,
                  //  ImageData = category1.Cafeteria.Image,
                    ImageUrl = category1.Cafeteria.ImageUrl

                }
            }).ToList();

            return Ok(new
            {
                categories = categories,
                cafetria = new CafeteriaViewModel()
                {
                    Name = cafetria.Name,
                    Id = cafetria.Id,
                   // ImageData = cafetria.Image,
                    ImageUrl = cafetria.ImageUrl
                }
            });
        }

        [HttpPost]
        public IHttpActionResult Add(CategoryViewModel c)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data.");
            }
            
            var m = appdb.Categories.Add(new Category()
            {

                CafeteriaId = c.CafeteriaId,
                Id = c.Id,
                Name = c.Name
            });
            appdb.SaveChanges();
            if (c.ImageData != null)
            {
               // m.Image = c.ImageData;
                image.save_category_images(c.ImageData, m.Id);
                var imgurl = "/Content/admin/category/" + m.Id + ".png";
                m.ImageUrl = imgurl;
                appdb.SaveChanges();
            }
           
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
           // var oldimage = existingCategory.Image;
            if (existingCategory != null)
            {
                existingCategory.Id = c.Id;
                existingCategory.Name = c.Name;
               // existingCategory.Image = c.ImageData;
            }
            else
            {
                return NotFound();
            }
            if (c.ImageData != null)
            {
               // if (oldimage != c.ImageData)
                //{
                    image.save_category_images(c.ImageData, c.Id);
                    //if (oldimage == null)
                   // {
                        var imgurl = "/Content/admin/category/" + c.Id + ".png";
                        existingCategory.ImageUrl = imgurl;
                        
                   // }
                //}
            appdb.SaveChanges();
            }       
            return Ok();
        }


        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var categoryDeleted = appdb.Categories.FirstOrDefault(d => d.Id == id);

            if (categoryDeleted != null)
            {
                image.delete_image("/Content/admin/category/" + categoryDeleted.Id + ".png");
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
