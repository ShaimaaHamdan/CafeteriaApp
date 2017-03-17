using CafeteriaApp.Data.Contexts;
using CafeteriaApp.Data.Models;
using CafeteriaApp.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CafeteriaApp.Web.Controllers
{
    public class AdditionController : ApiController
    {
        public AppDb appdb = new AppDb();

        //get all additions
        public IHttpActionResult Get()
        {
            //lamda expression
            var additions = appdb.Additions.Select(addition1 => new AdditionViewModel()
            {

                Id = addition1.Id,
                Name = addition1.Name,
            }).ToList();

            return Ok(new { additions = additions });

        }

        //get additions by id
        public IHttpActionResult Get(int id)
        {
            var addition = appdb.Additions.FirstOrDefault(a => a.Id == id);
            if (addition == null)
            {
                return NotFound();
            }

            var additionModel = new AdditionViewModel()
            {
                Id = addition.Id,
                Name = addition.Name,
            };
            return Ok(new { addition = additionModel });
        }
        //add additions 
        [HttpPost]
        public IHttpActionResult Add(AdditionViewModel a)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data.");
            }
            var m = appdb.Additions.Add(new Addition()
            {
                Id = a.Id,
                Name = a.Name,
            });
            appdb.SaveChanges();
            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var additionDeleted = appdb.Additions.FirstOrDefault(d => d.Id == id);
            if (additionDeleted != null)
            {
                appdb.Additions.Remove(additionDeleted);
                appdb.SaveChanges();
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPut]
        public IHttpActionResult PUT(AdditionViewModel a)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Not a valid data");
            }

            var existingAddition = appdb.Additions.Where(x => x.Id == a.Id).FirstOrDefault<Addition>();

            if (existingAddition != null)
            {
                existingAddition.Id = a.Id;
                existingAddition.Name = a.Name;

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
