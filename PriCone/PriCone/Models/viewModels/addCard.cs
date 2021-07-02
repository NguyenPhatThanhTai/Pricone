using PriCone.Models.dataModels;
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

        public List<Characters> listChar { get; set; }

        public List<Card> listCard { get; set; }

        public Card MH { get; set; }
        public Card MX { get; set; }
        public Card MD { get; set; }
    }
}