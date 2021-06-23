using PriCone.Models;
using PriCone.Models.dataModels;
using PriCone.Models.viewModels;
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
            if(Session["User"] == null)
            {
                ViewBag.Login = "Login";
                ViewBag.CheckLogin = "Đăng nhập/Đăng ký";
            }
            else
            {
                ViewBag.Login = "TrangChu";
                ViewBag.CheckLogin = Session["User"].ToString();
            }

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

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string user, string pass)
        {
            if(new DAOController().checkLogin(user, pass))
            {
                Session["User"] = user;
                return RedirectToAction("TrangChu", "Char");
            }
            else
            {
                ModelState.AddModelError("", "Sai mật khẩu, tài khoản hoặc tài khoản đã bị khóa!");
                return View();
            }
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(registerViewModel user)
        {
            //Tạo mã người dùng
            DateTime time = DateTime.Now;
            string day = DateTime.Now.ToString("dd");
            string month = DateTime.Now.ToString("MM");
            string year = DateTime.Now.ToString("yyyy");
            string Min = DateTime.Now.ToString("mm");
            string sec = DateTime.Now.ToString("ss");

            string MaNguoiDung = "US" + day + "" + Min + "" + sec;

            User use = new User();
            use.UserId = MaNguoiDung;
            use.Username = user.Username;
            use.Password = user.Password;
            use.FullName = user.FullName;
            use.Birthday = user.Birthday;
            use.Address = user.Address;
            use.Phone = user.Phone;
            use.Email = user.Email;
            use.Avatar = "Default";
            use.Status = true;

            if(new DAOController().registerUser(use))
            {
                return RedirectToAction("Login", "Char");
            }
            else
            {
                ModelState.AddModelError("", "Đăng ký không thành công!");
                return View();
            }
        }
    }
}