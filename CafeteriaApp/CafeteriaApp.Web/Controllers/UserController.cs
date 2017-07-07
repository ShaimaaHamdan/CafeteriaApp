using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CafeteriaApp.Data.Contexts;
using CafeteriaApp.Data.Models;
using CafeteriaApp.Web.Models;
using Microsoft.AspNet.Identity;
using CafeteriaApp.Web.Controllers;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace CafeteriaApp.Web.Controllers
{
    [RoutePrefix("api/User")]
    public class UserController : ApiController
    {
        public AppDb appdb = new AppDb();
        //[Authorize]
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
            List<RoleViewModel> r = new List<RoleViewModel>();
            if (user.Roles.Count() != 0)
            {
                var roles = appdb.Persons.Where(u => u.Id == user.Id).FirstOrDefault().Roles;
                
                foreach (var role in roles)
                {
                    r.Add(new RoleViewModel()
                    {
                        Id = role.Id,
                        Name = role.Name                    
                    });
                }
            }
            //RoleViewModel[] r = new RoleViewModel[roles.Count()];
            //int j = 0;
            //for(int i = 0;i<roles.Count();i++)
            //{
            //    r[i]=new RoleViewModel()
            //    {
            //        Id=roles[i].Id
            //    }
            //}

            var userModel = new UserViewModel()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                Email = user.Email,
                Roles = r,
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
                Name = role.Name,
                //Persons = role.Persons;
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
        public IHttpActionResult PUT(UserViewModel user)
        {
            //var login = new LoginController();
            //var User = login.UserManager.FindById(user.Id);
            //User.Email = user.Email;
            //login.UserManager.Update(User);
             
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
                //existingUser.UserName = user.UserName;
                //existingUser.Email = user.Email;
                //existingUser.EmailConfirmed = user.EmailConfirmed;
                //existingUser.PasswordHash = user.PasswordHash;
                existingUser.PhoneNumber = user.PhoneNumber;
                //existingUser.PhoneNumberConfirmed = user.PhoneNumberConfirmed;
                //existingUser.AccessFailedCount = user.AccessFailedCount;
                //existingUser.TwoFactorEnabled = user.TwoFactorEnabled;
                //existingUser.LockoutEndDateUtc = user.LockoutEndDateUtc??DateTime.Now;
                //existingUser.LockoutEnabled = user.LockoutEnabled;
                //existingUser.SecurityStamp = user.SecurityStamp;
                appdb.SaveChanges();
            }
            else
            {
                return NotFound();
            }
            return Ok();
        }
        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        //public async System.Threading.Tasks.Task<System.Web.Mvc.ActionResult>


        [HttpPut]
        [Route("update/{id}")]
        public IHttpActionResult update(UserViewModel u)
        {
            var user = appdb.Persons.Where(p => p.Id == u.Id).FirstOrDefault<User>();
            user.FirstName = u.FirstName;
            user.LastName = u.LastName;
            user.Id = u.Id;
            user.PhoneNumber = u.PhoneNumber;
            //user.Email = u.Email;

            appdb.SaveChanges();
            //int f = 0;
            foreach(var u1 in UserManager.Users)
            {
                if (u1.Email == u.Email && u.Id!=u1.Id)
                {
                    //ModelState.AddModelError("", "email must be unique");
                    //f = 1;
                    return Ok(0);
                }
            }
            //if (f == 0)
            //{
                var existingUser = UserManager.FindById(u.Id);
                existingUser.Email = u.Email;
                existingUser.UserName = u.Email;
                if (existingUser.Roles.Count() > 0)
                {
                    var roles = UserManager.GetRoles(u.Id);
                    UserManager.RemoveFromRoles(existingUser.Id, roles.ToArray());
                    //UserManager.Update(existingUser);
                }
                string[] s = new string[u.Roles.Count()];
                for (int i = 0; i < u.Roles.Count(); i++)
                {
                    s[i] = u.Roles[i].Name;
                }
                UserManager.AddToRoles(u.Id, s);
                //ModelState.AddModelError("", "email must be unique");
                UserManager.Update(existingUser);          
            //}
            return Ok(1);
        }

        [HttpPost]
        [Route("createUser")]
        public IHttpActionResult createUser(RegisterViewModel user)
        {
            //int f = 0;
            foreach(var u in UserManager.Users)
            {
                if (u.Email == user.Email)
                {
                    //ModelState.AddModelError("", "email must be unique");
                    //f = 1;
                    return Ok(0);
                    //return View(user);
                }
            }
            //if (f == 0)
            //{
                UserManager.Create(new ApplicationUser { Email = user.Email, UserName = user.Email });
                var newuser = UserManager.FindByEmail(user.Email);
                UserManager.AddPassword(newuser.Id, user.Password);
                UserManager.Update(newuser);
            //}
            return Ok(1);
        }

        [Route("assignRoles/{id}")]
        [HttpPut]
        public IHttpActionResult assignRoles([FromBody]UserViewModel user)
        {
            var existingUser = appdb.Persons.FirstOrDefault(u => u.Id == user.Id);
            if (existingUser != null)
            {
                foreach (var role in user.Roles)
                {
                    var roleToAdd = appdb.Roles.FirstOrDefault(r => r.Id == role.Id);
                    existingUser.Roles.Add(roleToAdd);
                }
                appdb.SaveChanges();
                return Ok();
            }
            else
            {
                return NotFound();
            }
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
