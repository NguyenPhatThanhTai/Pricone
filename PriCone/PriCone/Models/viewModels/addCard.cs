using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PriCone.Models.viewModels
{
    public class addCard
    {
        public string CardId { get; set; }
        public string CharId { get; set; }
        public HttpPostedFileBase Card { get; set; }
        public string flag { get; set; }
    }
}