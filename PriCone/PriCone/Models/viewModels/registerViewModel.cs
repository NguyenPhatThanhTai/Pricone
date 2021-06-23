using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PriCone.Models.viewModels
{
    public class registerViewModel
    {
        public string UserId { get; set; }
        public bool Role { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public DateTime? Birthday { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public HttpPostedFileBase Avatar { get; set; }
        public bool? Status { get; set; }
    }
}