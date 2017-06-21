using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CafeteriaApp.Data.Contexts;
using CafeteriaApp.Data.Models;
using CafeteriaApp.Web.Models;
using CafeteriaApp.Web.Helpers;
using System.Drawing;
using System.Drawing.Imaging;

namespace CafeteriaApp.Web.Controllers
{
    public class CafeteriaController : ApiController
    {
        image_handle image=new image_handle();
        public AppDb appdb = new AppDb();
        public IHttpActionResult Get()
        {
            //lamda expression
            var cafeterias = appdb.Cafeterias.Select(c => new CafeteriaViewModel()
            {
                Id = c.Id,
                Name = c.Name,
                //ImageData = c.Image,
                ImageUrl = c.ImageUrl
            }).ToList();

            return Ok(new  { cafeterias = cafeterias });
        }
        public IHttpActionResult Get(int id)
        {
            var cafeteria = appdb.Cafeterias.FirstOrDefault(c => c.Id == id);
            if (cafeteria == null)
            {
                return NotFound();
            }

            var cafeteriaModel = new CafeteriaViewModel()
            {
                Id = cafeteria.Id,
                Name = cafeteria.Name,
                //ImageData = cafeteria.Image,
                ImageUrl = cafeteria.ImageUrl
            };
            
            return Ok(new { cafeteria = cafeteriaModel});
        }
        [HttpPut]
        public IHttpActionResult PUT(CafeteriaViewModel cafeteria) 
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Not a valid data");
            }

            var existingCafeteria = appdb.Cafeterias.Where(x => x.Id == cafeteria.Id).FirstOrDefault<Cafeteria>();
            if (existingCafeteria != null)
            {
                existingCafeteria.Id = cafeteria.Id;
                existingCafeteria.Name = cafeteria.Name;
                 image.save_cafeteria_images(cafeteria.ImageData, cafeteria.Id);
                var imgurl = "/Content/admin/cafeteria/" + cafeteria.Id + ".png";
                existingCafeteria.ImageUrl = imgurl;
            }
            else
            {
                return NotFound();
            }
            
            appdb.SaveChanges();
            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var cafeteriaToDelete = appdb.Cafeterias.FirstOrDefault(c => c.Id == id);
            if (cafeteriaToDelete != null)
            {
                image.delete_image("/Content/admin/cafeteria/" + cafeteriaToDelete.Id + ".png");
                appdb.Cafeterias.Remove(cafeteriaToDelete);

                appdb.SaveChanges();
                
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public IHttpActionResult Add(CafeteriaViewModel cafeteria)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data.");
            }

         
            var m = appdb.Cafeterias.Add(new Cafeteria()
            {
                Id = cafeteria.Id,
                Name = cafeteria.Name          
            });

            appdb.SaveChanges();

            image.save_cafeteria_images(cafeteria.ImageData, m.Id);

            var imgurl = "/Content/admin/cafeteria/" + m.Id + ".png";
            m.ImageUrl = imgurl;
            appdb.SaveChanges();

            return Ok();
        }
    }
}
