using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;

namespace CafeteriaApp.Web.Helpers
{
    public class image_handle
    {
        public void save_cafeteria_images(string base64String,string s)
        {
            // Convert Base64 String to byte[]
            byte[] imageBytes = Convert.FromBase64String(base64String);
            using (var imageFile = new FileStream(HttpContext.Current.Server.MapPath("~/Content/admin/cafeteria/" + s + ".png"), FileMode.Create))
            {
                imageFile.Write(imageBytes, 0, imageBytes.Length);
                imageFile.Flush();
            }
        }
        public void save_category_images(string base64String,string s)
        {
            byte[] imageBytes = Convert.FromBase64String(base64String);
            using (var imageFile = new FileStream(HttpContext.Current.Server.MapPath("~/Content/admin/category/" + s + ".png"), FileMode.Create))
            {
                imageFile.Write(imageBytes, 0, imageBytes.Length);
                imageFile.Flush();
            }
        }
        public void save_menuitem_images(string base64String,string s)
        {
            byte[] imageBytes = Convert.FromBase64String(base64String);
            using (var imageFile = new FileStream(HttpContext.Current.Server.MapPath("~/Content/admin/menuitem/" + s + ".png"), FileMode.Create))
            {
                imageFile.Write(imageBytes, 0, imageBytes.Length);
                imageFile.Flush();
            }
        }
        public void delete_image(string path)
        {
            var filepath = HttpContext.Current.Server.MapPath(path);
            if (File.Exists(filepath))
            {
                File.Delete(filepath);
            }
        }
    }
}