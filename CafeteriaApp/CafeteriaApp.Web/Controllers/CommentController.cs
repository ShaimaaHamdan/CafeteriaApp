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
                CustomerId=comment.CustomerId,
                Customer = new CustomerViewModel()
                {
                    Id = comment.Customer.Id,
                    Credit = comment.Customer.Credit,
                    LimitedCredit = comment.Customer.LimitedCredit,
                    UserId = comment.Customer.UserId,
                    User = new UserViewModel()
                    {
                        Id = comment.Customer.User.Id,
                        UserName = comment.Customer.User.UserName,
                        FirstName = comment.Customer.User.FirstName,
                        LastName = comment.Customer.User.LastName,
                        Email = comment.Customer.User.Email,
                        EmailConfirmed = comment.Customer.User.EmailConfirmed,
                        PhoneNumber = comment.Customer.User.PhoneNumber,
                        PhoneNumberConfirmed = comment.Customer.User.PhoneNumberConfirmed,
                        PasswordHash = comment.Customer.User.PasswordHash,
                        SecurityStamp = comment.Customer.User.SecurityStamp,
                        TwoFactorEnabled = comment.Customer.User.TwoFactorEnabled,
                        LockoutEndDateUtc = comment.Customer.User.LockoutEndDateUtc,
                        LockoutEnabled = comment.Customer.User.LockoutEnabled,
                        AccessFailedCount = comment.Customer.User.AccessFailedCount,
                    },
                },

                MenuItemId=comment.MenuItemId,
                MenuItem = new MenuItemViewModel()
                {
                    Id = comment.MenuItem.Id,
                    Name = comment.MenuItem.Name,
                    Description = comment.MenuItem.Description,
                    Price = comment.MenuItem.Price,
                    Type = comment.MenuItem.Type,
                    CategoryId = comment.MenuItem.CategoryId
                },

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

            var c = appdb.Comments.Add(new Comment()
            {
                Id = comment.Id,
                MenuItemId = comment.MenuItemId,
                CustomerId = comment.CustomerId,
                Data=comment.Data
            });
            appdb.SaveChanges();
            return Ok();
        }


        [HttpPut]
        public IHttpActionResult Put(CommentViewModel comment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Not a valid data");
            }
            var c = appdb.Comments.Where(comm => comm.Id == comment.Id).FirstOrDefault<Comment>();
            if (c == null)
            {
                return NotFound();
            }
            c.Data = comment.Data;
            appdb.SaveChanges();
            return Ok();
        }
    }
}
