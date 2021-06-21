using PriCone.Models;
using PriCone.Models.dataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PriCone.Controllers
{
    public class CharController : Controller
    {
        [HttpGet]
        public ActionResult TrangChu()
        {
            List<Guild> guildList = new DAOController().getAllGuild();
            ViewBag.guild = guildList;
            List<Characters> list = new DAOController().getAllChar();
            return View(list);
        }

        [HttpGet]
        public ActionResult ChiTiet(String Id)
        {
            if (Id == null)
            {
                return RedirectToAction("TrangChu");
            }
            else
            {
                Characters characters = new DAOController().detailChar(Id);
                return View(characters);
            }
        }
    }
}