using PriCone.Models.dataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PriCone.Models.viewModels
{
    public class UserView
    {
        public User user { get; set; }
        public List<Liking> listLike {get; set;}
        public List<Saved> listSaved { get; set; }
    }
}