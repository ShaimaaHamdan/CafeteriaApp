﻿using System;
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

    [RoutePrefix("api/MenuItem")]
    public class MenuItemController : ApiController
    {
        image_handle image = new image_handle();
        public AppDb appdb = new AppDb();

        public IHttpActionResult Get()
        {
            //lamda expression
            var menuItems = appdb.MenuItems.Select(menuitem => new MenuItemViewModel()
            {
                CategoryId = menuitem.CategoryId,
                Description = menuitem.Description,
                Id = menuitem.Id,
                Name = menuitem.Name,
                //ImageData = menuitem.Image,
                ImageUrl = menuitem.ImageUrl,
                alternatetext = menuitem.alternatetext,
                Price = menuitem.Price,
                Type = menuitem.Type,
                Category = new CategoryViewModel()
                {
                    Name = menuitem.Category.Name,
                    Id = menuitem.Category.Id,
                    CafeteriaId = menuitem.Category.CafeteriaId,
                    //ImageData = menuitem.Category.Image,
                    ImageUrl = menuitem.Category.ImageUrl
                },
                Comments = appdb.Comments.Select(c => new CommentViewModel()
                {
                    Id = c.Id,
                    Data=c.Data,
                    MenuItemId = menuitem.Id,
                    CustomerId = c.CustomerId
                }).ToList()
            }).ToList();

            return Ok(new { menuItems = menuItems });

        }

        public IHttpActionResult Get(int id)
        {
            var menuitem = appdb.MenuItems.FirstOrDefault(m => m.Id == id);
            if (menuitem == null)
            {
                return NotFound();
            }

            MenuItemViewModel model = new MenuItemViewModel()
            {
                CategoryId = menuitem.CategoryId,
                Description = menuitem.Description,
                Id = menuitem.Id,
                Name = menuitem.Name,
                //ImageData = menuitem.Image,
                ImageUrl = menuitem.ImageUrl,
                alternatetext = menuitem.alternatetext,
                Price = menuitem.Price,
                Type = menuitem.Type,
                Category = new CategoryViewModel()
                {
                    Name = menuitem.Category.Name,
                    Id = menuitem.Category.Id,
                    CafeteriaId = menuitem.Category.CafeteriaId,
                    //ImageData = menuitem.Category.Image,
                    ImageUrl = menuitem.Category.ImageUrl
                },
                Additions = menuitem.Additions.Select(i=>new AdditionViewModel()
                {
                    Id = i.Id,
                    Name = i.Name
                }).ToList(),
                Comments = appdb.Comments.Select(c => new CommentViewModel()
                {
                    Id = c.Id,
                    Data=c.Data,
                    MenuItemId = menuitem.Id,
                    CustomerId = c.CustomerId
                }).ToList()
            };


            return Ok(new { menuitem =model});
        }

        [Route("GetByCategory/{id}")]
        public IHttpActionResult GetByCategory(int id)
        {
            //lamda expression
            var menuItems = appdb.MenuItems.Where(item => item.CategoryId == id).Select(menuitem => new MenuItemViewModel()
            {
                CategoryId = menuitem.CategoryId,
                Description = menuitem.Description,
                Id = menuitem.Id,
                Name = menuitem.Name,
                Price = menuitem.Price,
                //ImageData = menuitem.Image,
                ImageUrl = menuitem.ImageUrl,
                alternatetext = menuitem.alternatetext,
                Type = menuitem.Type,
                Category = new CategoryViewModel()
                {
                    Name = menuitem.Category.Name,
                    Id = menuitem.Category.Id,
                    CafeteriaId = menuitem.Category.CafeteriaId,
                    //ImageData = menuitem.Category.Image,
                    ImageUrl = menuitem.Category.ImageUrl
                },
                Comments = menuitem.Comments.Select(c => new CommentViewModel()
                {
                    Id = c.Id,
                    Data =c.Data,
                    MenuItemId=menuitem.Id,
                    //MenuItem = new MenuItemViewModel()
                    //{
                    //    Id = c.MenuItem.Id,
                    //    Name = c.MenuItem.Name,
                    //    Description = c.MenuItem.Description,
                    //    Price = c.MenuItem.Price,
                    //    Type = c.MenuItem.Type,
                    //    CategoryId = c.MenuItem.CategoryId
                    //},
                    CustomerId =c.CustomerId
                    //Customer = new CustomerViewModel()
                    //{
                    //    Id = c.Customer.Id,
                    //    Credit = c.Customer.Credit,
                    //    LimitedCredit = c.Customer.LimitedCredit,
                    //    UserId = c.Customer.UserId,
                    //    User = new UserViewModel()
                    //    {
                    //        Id = c.Customer.User.Id,
                    //        UserName = c.Customer.User.UserName,
                    //        FirstName = c.Customer.User.FirstName,
                    //        LastName = c.Customer.User.LastName,
                    //        Email = c.Customer.User.Email,
                    //        EmailConfirmed = c.Customer.User.EmailConfirmed,
                    //        PhoneNumber = c.Customer.User.PhoneNumber,
                    //        PhoneNumberConfirmed = c.Customer.User.PhoneNumberConfirmed,
                    //        PasswordHash = c.Customer.User.PasswordHash,
                    //        SecurityStamp = c.Customer.User.SecurityStamp,
                    //        TwoFactorEnabled = c.Customer.User.TwoFactorEnabled,
                    //        LockoutEndDateUtc = c.Customer.User.LockoutEndDateUtc,
                    //        LockoutEnabled = c.Customer.User.LockoutEnabled,
                    //        AccessFailedCount = c.Customer.User.AccessFailedCount
                    //    }
                    //}
                }).ToList()
            }).ToList();

            return Ok(new { menuItems =  menuItems });

        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var menuItemToDelete = appdb.MenuItems.FirstOrDefault(m => m.Id == id);
            if (menuItemToDelete != null)
            {
                image.delete_image("/Content/admin/menuitem/" + menuItemToDelete.Id + ".png");
                appdb.MenuItems.Remove(menuItemToDelete);
                appdb.SaveChanges();
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
        [HttpPost]
        public IHttpActionResult Add(MenuItemViewModel menuitem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data.");
            }
            
            var m = appdb.MenuItems.Add(new MenuItem()
            {

                CategoryId = menuitem.CategoryId,
                Description = menuitem.Description,
                Id = menuitem.Id,
                Name = menuitem.Name,
                
                ImageUrl = menuitem.ImageUrl,   
                alternatetext = menuitem.alternatetext,
                Price = menuitem.Price,
                Type = menuitem.Type
            });
            if (menuitem.ImageData != null)
            {
                //m.Image = menuitem.ImageData;
                string s = DateTime.Now.ToString().Replace(@"/", "-").Replace(':', '-');
                image.save_menuitem_images(menuitem.ImageData,s);
                var imgurl = "/Content/admin/menuitem/" + s + ".png";
                m.ImageUrl = imgurl;
            }
            appdb.SaveChanges();
            return Ok();
        }

        [HttpPut]
        public IHttpActionResult Put(MenuItemViewModel menuitem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Not a valid data");
            }

            var existingMenuitem = appdb.MenuItems.Where(x => x.Id == menuitem.Id).FirstOrDefault<MenuItem>();
            //var oldimage = existingMenuitem.Image;
            if (existingMenuitem != null)
            {
                existingMenuitem.Id = menuitem.Id;
                existingMenuitem.Name = menuitem.Name;
                existingMenuitem.Type = menuitem.Type;
                existingMenuitem.Price = menuitem.Price;
                existingMenuitem.Description = menuitem.Description;
               // existingMenuitem.Image = menuitem.ImageData;
            }
            else
            {
                return NotFound();
            }
            if (menuitem.ImageData != null)
            {
                if (existingMenuitem.ImageUrl != null)
                {
                    image.delete_image(existingMenuitem.ImageUrl);
                }
                string s = DateTime.Now.ToString().Replace(@"/", "-").Replace(':', '-');
                image.save_menuitem_images(menuitem.ImageData,s);
                var imgurl = "/Content/admin/menuitem/" + s + ".png";
                existingMenuitem.ImageUrl = imgurl;
            }
            appdb.SaveChanges();
            return Ok();
        }

        [HttpPost]
        [Route("addAdditions")]
        public IHttpActionResult AddAdditions([FromBody]MenuItemViewModel menuitem)
        {

            var menuItemObject = appdb.MenuItems.FirstOrDefault(i => i.Id == menuitem.Id);
            if (menuItemObject != null)
            {
                foreach (var addition in menuitem.Additions)
                {
                    var additionToAdd = appdb.Additions.FirstOrDefault(ad => ad.Id == addition.Id);
                    menuItemObject.Additions.Add(additionToAdd);
                }
               
            }
            
            appdb.SaveChanges();
            return Ok();
        }

        [HttpDelete]
        [Route("DeleteMenuItemAddition")]
        public IHttpActionResult DeleteMenuItemAddition(MenuItemAdditionViewModel model)
        {
            var menuItem = appdb.MenuItems.FirstOrDefault(m => m.Id == model.MenuItemId);
            if (menuItem != null)
            {
                var addtionToRemove = appdb.Additions.FirstOrDefault(i => i.Id == model.AdditionId);
                menuItem.Additions.Remove(addtionToRemove);
                appdb.SaveChanges();
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

    }



    public class MenuItemAdditionViewModel
    {
        public int MenuItemId { get; set; }
        public int AdditionId { get; set; }
    }
}




