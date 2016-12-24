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
    public class CafeteriaController : ApiController
    {
        public AppDb appdb = new AppDb();
        public IHttpActionResult GetCafeteria(int id)
        {
            var cafeteria = appdb.Cafeterias.FirstOrDefault(c => c.Id == id);
            if (cafeteria == null)
            {
                return NotFound();
            }
            return Ok(cafeteria);
        }
        public IHttpActionResult AddCafeteria(Cafeteria c)
        {
            if (c != null)
            {
                appdb.Cafeterias.Add(c);
                appdb.SaveChanges();
                return Ok(c);
            }
            else
            {
                return BadRequest();
            }
        }
        public IHttpActionResult PutCafeteria(int id,Cafeteria c)
        {
            Cafeteria c1=appdb.Cafeterias.Find(id);
            if (c1 != null)
            {
                c1.name=c.name;
                appdb.SaveChanges();
                return Ok(c1);
            }
            else
            {
                return NotFound();
            }
        }
        public IQueryable<Cafeteria> GetAllCafeterias()
        {
            return appdb.Cafeterias;
        }
        public IHttpActionResult DeleteCafeteria(string name)
        {
            var cafeteriaToDelete = appdb.Cafeterias.FirstOrDefault(c => c.name == name);
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
    }
}
