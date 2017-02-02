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
    public class CafeteriaController : ApiController
    {
        public AppDb appdb = new AppDb();
        public IHttpActionResult Get()
        {
            //lamda expression
            var cafeteria = appdb.Cafeterias.Select(c => new CafeteriaViewModel()
            {
                Id = c.Id,
                Name = c.Name,
            }).ToList();

            return Ok(cafeteria);
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
            };
            
            return Ok(cafeteriaModel);
        }
        [HttpPut]
        public IHttpActionResult Put(CafeteriaViewModel cafeteria)
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
            var cafeteriaToDelete = appdb.Cafeterias.FirstOrDefault(c => c.Id == id);
            if (cafeteriaToDelete != null)
            {
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
                Name = cafeteria.Name,
            });
            appdb.SaveChanges();
            return Ok();
        }
    }
}
