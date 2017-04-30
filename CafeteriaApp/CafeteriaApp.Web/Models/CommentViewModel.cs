using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CafeteriaApp.Web.Models
{
    public class CommentViewModel
    {
        public int Id { get; set; }

        //-------------------------
        public int CustomerId { get; set; }

        //-------------------------

        public int MenuItemId { get; set; }

        //-------------------------
        public string Data { get; set; }

        //-------------------------
        public CustomerViewModel Customer { get; set; }
        //-------------------------
        public MenuItemViewModel MenuItem { get; set; }

    }
}