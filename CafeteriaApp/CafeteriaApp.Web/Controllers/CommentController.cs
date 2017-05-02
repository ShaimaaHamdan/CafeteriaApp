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
    [RoutePrefix("api/Comment")]
    public class CommentController : ApiController
    {
        public AppDb appdb = new AppDb();

        [Route("GetByMenuItem/{id}")]
        public IHttpActionResult GetByMenuItem(int id)
        {
            //lamda expression
            var comments = appdb.Comments.Where(item => item.MenuItemId == id).Select(comment => new CommentViewModel()
            {
                Id = comment.Id,
                Data = comment.Data,

                Customer = new CustomerViewModel()
                {

                    Id = comment.Customer.Id,
                    UserId = comment.Customer.UserId,
                    User = new UserViewModel()
                    {
                        FirstName = comment.Customer.User.FirstName
                    }

                }


                 //MenuItem = new MenuItemViewModel()
                 //{
                 //    Name = menuitem.Category.Name,
                 //    Id = menuitem.Category.Id,
                 //    CafeteriaId = menuitem.Category.CafeteriaId,
                 //    ImageData = menuitem.Category.Image,
                 //}

            }).ToList();

            return Ok(new { comments = comments });

        }



        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var commentToDelete = appdb.Comments.FirstOrDefault(o => o.Id == id);
            if (commentToDelete != null)
            {
                appdb.Comments.Remove(commentToDelete);
                appdb.SaveChanges();
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        //---------------------------------------------------------------------------------------

        [HttpPost]
        //[Route("addcomment")]
       
        public IHttpActionResult Add(CommentViewModel comment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data.");
            }

            Comment Comm;

            Comm = new Comment()
                {
                     //Id= comment.Id,
                      Data=comment.Data,
                       CustomerId=  1,    //comment.CustomerId,
                        MenuItemId=  8     //comment.MenuItemId
            };

                appdb.Comments.Add(Comm);
                appdb.SaveChanges();
          
            appdb.SaveChanges();
            return Ok();
        }











        //------------------------------------------------------------------------------------
        // GET: api/Comment
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET: api/Comment/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST: api/Comment
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT: api/Comment/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE: api/Comment/5
        //public void Delete(int id)
        //{
        //}
    }
}
