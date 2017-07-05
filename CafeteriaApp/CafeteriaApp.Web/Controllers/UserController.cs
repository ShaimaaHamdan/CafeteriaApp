using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CafeteriaApp.Data.Contexts;
using CafeteriaApp.Data.Models;
using CafeteriaApp.Web.Models;
using System.Web.Security;
//using System.Collections;
namespace CafeteriaApp.Web.Controllers
{
    [RoutePrefix("api/User")]
    public class UserController : ApiController
    {
        public AppDb appdb = new AppDb();

        public IHttpActionResult Get()
        {
            //lamda expression
            var users = appdb.Persons.Select(user => new UserViewModel()
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    UserName = user.UserName,
                    Email = user.Email,
                    //EmailConfirmed = user.EmailConfirmed,
                    PasswordHash = user.PasswordHash,
                    PhoneNumber = user.PhoneNumber,
                    //PhoneNumberConfirmed = user.PhoneNumberConfirmed,
                    //AccessFailedCount = user.AccessFailedCount,
                    //TwoFactorEnabled = user.TwoFactorEnabled,
                    LockoutEndDateUtc = user.LockoutEndDateUtc,
                    //LockoutEnabled = user.LockoutEnabled,
                    //SecurityStamp = user.SecurityStamp
                    
                })
                .ToList();

            return Ok(new { users = users });

        }

        public IHttpActionResult Get(string id)
        {
            var user = appdb.Persons.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            var userModel = new UserViewModel()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                Email = user.Email,
                EmailConfirmed = user.EmailConfirmed,
                PasswordHash = user.PasswordHash,
                PhoneNumber = user.PhoneNumber,
                PhoneNumberConfirmed = user.PhoneNumberConfirmed,
                AccessFailedCount = user.AccessFailedCount,
                TwoFactorEnabled = user.TwoFactorEnabled,
                LockoutEndDateUtc = user.LockoutEndDateUtc,
                LockoutEnabled = user.LockoutEnabled,
                SecurityStamp = user.SecurityStamp
            };

            return Ok(new { user = userModel });
        }

        [Route("Getallroles")]
        public IHttpActionResult Getallroles()
        {
            var roles = appdb.Roles.Select(role => new RoleViewModel()
            {
                Id = role.Id,
                Name = role.Name
            }).ToList();
            return Ok(new { roles = roles });
        }

        [HttpPost]
        public IHttpActionResult Add(UserViewModel user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data.");
            }


            var u = appdb.Persons.Add(new User()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                Email = user.Email,
                EmailConfirmed = user.EmailConfirmed,
                PasswordHash = user.PasswordHash,
                PhoneNumber = user.PhoneNumber,
                PhoneNumberConfirmed = user.PhoneNumberConfirmed,
                AccessFailedCount = user.AccessFailedCount,
                TwoFactorEnabled = user.TwoFactorEnabled,
                LockoutEndDateUtc = user.LockoutEndDateUtc??DateTime.Now, // assign default value when it's null
                LockoutEnabled = user.LockoutEnabled,
                SecurityStamp = user.SecurityStamp

            });
            appdb.SaveChanges();
            return Ok();
        }

        [HttpPut]
        public IHttpActionResult PUT([FromBody]UserViewModel user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Not a valid data");
            }

            var existingUser = appdb.Persons.Where(u => u.Id == user.Id).FirstOrDefault<User>();

            if (existingUser != null)
            {
                existingUser.Id = user.Id;
                existingUser.FirstName = user.FirstName;
                existingUser.LastName = user.LastName;
                existingUser.UserName = user.UserName;
                existingUser.Email = user.Email;
                existingUser.EmailConfirmed = user.EmailConfirmed;
                existingUser.PasswordHash = user.PasswordHash;
                existingUser.PhoneNumber = user.PhoneNumber;
                existingUser.PhoneNumberConfirmed = user.PhoneNumberConfirmed;
                existingUser.AccessFailedCount = user.AccessFailedCount;
                existingUser.TwoFactorEnabled = user.TwoFactorEnabled;
                existingUser.LockoutEndDateUtc = user.LockoutEndDateUtc??DateTime.Now;
                existingUser.LockoutEnabled = user.LockoutEnabled;
                existingUser.SecurityStamp = user.SecurityStamp;
                if (existingUser.Roles.Count()==0)
                {
                    existingUser.Roles.Add(new Role()
                    {
                        //Id = user.Roles.FirstOrDefault().Id,
                        Id=1,
                        Name="Customer"
                        //Name = user.Roles.FirstOrDefault().Name
                    });
                }
                else
                {
                    existingUser.Roles.Clear();
                    existingUser.Roles.Add(new Role() {
                        Id=user.Roles[0].Id,
                        Name=user.Roles[0].Name
                    });
                }
                //appdb.userRoles.Add(new UserRole()
                //{
                  //  UserId=user.Id,
                   // RoleId="1"
                //});
                appdb.SaveChanges();
            }
            else
            {
                return NotFound();
            }
            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult Delete(string id)
        {
            var userToDelete = appdb.Persons.FirstOrDefault(c => c.Id == id);
            if (userToDelete != null)
            {
                appdb.Persons.Remove(userToDelete);
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
