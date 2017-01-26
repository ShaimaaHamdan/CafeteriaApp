//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net;
//using System.Net.Http;
//using System.Web.Http;
//using CafeteriaApp.Data.Contexts;
//using CafeteriaApp.Data.Models;
//using CafeteriaApp.Web.Models;

//namespace CafeteriaApp.Web.Controllers
//{
//    public class CategoryController : ApiController
//    {

//        public AppDb appdb = new AppDb();

//        //public IHttpActionResult GetAllCategories()
//        //{
//        //    var category = appdb.Categories.Select(category1 => new CategoryViewModel()
//        //    {
//        //        CafeteriaId = category1.CafeteriaId,
//        //        Id = category1.Id,
//        //        Name = category1.Name,
//        //        Cafeteria = new CafeteriaViewModel()
//        //        {
//        //            Name = category1.Cafeteria.Name,
//        //            Id = category1.Cafeteria.Id,
//        //            CafeteriaId = category1.CafeteriaId,
//        //        }
//        //    }).ToList();

//        //    return Ok(category);
//        //}

//        //public IHttpActionResult Get(int id)
//        //{
//        //    var cate = appdb.Categories.FirstOrDefault(c => c.Id == id);
//        //    if (cate == null)
//        //    {
//        //        return NotFound();
//        //    }

//        //    CategoryViewModel cat = new CategoryViewModel()
//        //    {
//        //        Id = cate.Id,
//        //        Name = cate.Name,
//        //        Cafeteria = new CafeteriaViewModel()
//        //        {
//        //            Name = cate.Cafeteria.Name,
//        //            Id = cate.Cafeteria.Id,
//        //            CafeteriaId = Cate.Cafeteria.CafeteriaId,
//        //        }
//        //    };
//        //    return Ok(cat);


//        //}



//        //[HttpPost]
//        //public IHttpActionResult Add(Category c)
//        //{
//        //    if (!ModelState.IsValid)
//        //    {
//        //        return BadRequest("Invalid data.");
//        //    }
//        //    var m = appdb.Categories.Add(new Category()
//        //    {
//        //        Id = c.Id,
//        //        Name = c.Name,
//         //         Cafeteria = new CafeteriaViewModel()
//        //        {
//        //            Name = cate.Cafeteria.Name,
//        //            Id = cate.Cafeteria.Id,
//        //            CafeteriaId = Cate.Cafeteria.CafeteriaId,
//        //        }

//        //    });
//        //    appdb.SaveChanges();
//        //    return Ok();
//        //}

//        //[HttpPut]
//        //public IHttpActionResult PUT(CategoryViewModel c)
//        //{
//        //    if (!ModelState.IsValid)
//        //    {
//        //        return BadRequest("Not a valid data");
//        //    }

//        //    var existingCategory= appdb.Categories.Where(x => x.Id == c.Id).FirstOrDefault<Category>();

//        //    if (existingCategory != null)
//        //    {
//        //        existingCategory.Id = c.Id;
//        //        existingCategory.Name = c.Name;
//        //        Cafeteria = new CafeteriaViewModel()
//        //        {
//        //            Name = existingCategory.Cafeteria.Name,
//        //            Id = existingCategory.Cafeteria.Id,
//        //            CafeteriaId = existingCategory.Cafeteria.CafeteriaId,
//        //        }
//        //       appdb.SaveChanges();
//        //    }
//        //    else
//        //    {
//        //        return NotFound();
//        //    }


//        //    return Ok();
//        //}


//        [HttpDelete]
//        public IHttpActionResult Delete(int id)
//        {
//            var categoryDeleted = appdb.Categories.FirstOrDefault(d => d.Id == id);
            
//            if (categoryDeleted != null)
//            {
//                appdb.Categories.Remove(categoryDeleted);
//                appdb.SaveChanges();
//                return Ok();
//            }
//            else
//            {
//                return NotFound();
//            }
//        }


//    }
//}
