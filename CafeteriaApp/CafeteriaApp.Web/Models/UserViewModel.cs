using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CafeteriaApp.Web.Models
{
    public class UserViewModel
    {
        public string Id { get; set; }

        public string Email { get; set; }

        public Boolean EmailConfirmed { get; set; }

        public string PasswordHash { get; set; }

        public string SecurityStamp { get; set; }

        public string PhoneNumber { get; set; }

        public Boolean PhoneNumberConfirmed { get; set; }

        public Boolean TwoFactorEnabled { get; set; }

        public DateTime LockoutEndDateUtc { get; set; }

        public Boolean LockoutEnabled { get; set; }

        public int IAccessFailedCountd { get; set; }

        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}