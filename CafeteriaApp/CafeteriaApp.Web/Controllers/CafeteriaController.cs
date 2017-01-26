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
            var cafeteria = appdb.Cafeterias.Select(cafeteria1 => new CafeteriaViewModel()
            {
                Id = cafeteria1.Id,
                name = cafeteria1.name,
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

            CafeteriaViewModel model = new CafeteriaViewModel()
            {
                Id = cafeteria.Id,
                name = cafeteria.name,
            };
            return Ok(model);
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
                name = cafeteria.name,
            });
            appdb.SaveChanges();
            return Ok();
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
                existingCafeteria.name = cafeteria.name;
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
