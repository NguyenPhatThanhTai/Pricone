using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PriCone.Models.viewModels
{
    public partial class addViewModel
    {
        public string GuildId { get; set; }
        public string CharName { get; set; }
        public string Height { get; set; }
        public string Weight { get; set; }
        public DateTime? Birthday { get; set; }
        public string BloodType { get; set; }
        public string Race { get; set; }
        public string Hobbies { get; set; }
        public string VA { get; set; }
        public string Description { get; set; }
        public string Detail { get; set; }
        public DateTime? Released { get; set; }
        public int? Likes { get; set; }
        public HttpPostedFileBase Icon { get; set; }
    }
}