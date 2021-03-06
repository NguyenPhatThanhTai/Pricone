using PriCone.Models;
using PriCone.Models.dataModels;
using PriCone.Models.viewModels;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using PagedList;

namespace PriCone.Controllers
{
    public class CharController : Controller
    {
        [HttpGet]
        public ActionResult TrangChu(int? page)
        {
            if (page == null) page = 1;
            List<Guild> pagedList = new DAOController().guildListPagedList();
            int pageSize = 3;
            int pageNumber = (page ?? 1);

            if (Session["User"] == null)
            {
                ViewBag.Login = "Login";
                ViewBag.display = "none";
                ViewBag.CheckLogin = "Đăng nhập/Đăng ký";
            }
            else
            {
                ViewBag.display = "static";
                User u = Session["User"] as User;
                ViewBag.Login = "User";
                ViewBag.idUser = u.UserId;
                if (u.Role == true)
                {
                    ViewBag.CheckLogin = u.FullName + " - Admin";
                }
                else
                {
                    ViewBag.CheckLogin = u.FullName + " - User";
                }
            }
            List<Characters> charTop = new DAOController().getCharTop();
            ViewBag.charTop = charTop;
            return View(pagedList.ToPagedList(pageNumber, pageSize));
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
                if (Session["User"] == null)
                {
                    ViewBag.Login = "Login";
                    ViewBag.display = "none";
                    ViewBag.CheckLogin = "Đăng nhập/Đăng ký";
                }
                else
                {
                    ViewBag.Login = "TrangChu";
                    ViewBag.display = "static";
                    User u = Session["User"] as User;
                    if (u.Role == true)
                    {
                        ViewBag.CheckLogin = u.FullName + " - Admin";
                    }
                    else
                    {
                        ViewBag.CheckLogin = u.FullName + " - User";
                    }
                }
                Characters characters = new DAOController().detailChar(Id);
                return View(characters);
            }
        }

        [HttpPost]
        public ActionResult updateLike(Liking like)
        {
            if (Session["User"] == null)
            {
                return View("Login");
            }
            else
            {
                string day = DateTime.Now.ToString("dd");
                string month = DateTime.Now.ToString("MM");
                string sec = DateTime.Now.ToString("ss");
                User u = Session["User"] as User;

                Liking likes = new Liking
                {
                    LikeId = "LI" + day + "" + month + "" + sec,
                    CharId = like.CharId,
                    UserId = u.UserId
                };
                new DAOController().updateLike(likes);
                Characters chars = new DAOController().detailChar(like.CharId);
                return View("sectionChiTiet", chars);
            }
        }

        [HttpPost]
        public ActionResult updateSave(Saved sav)
        {
            if (Session["User"] == null)
            {
                return View("Login");
            }
            else
            {
                string day = DateTime.Now.ToString("dd");
                string month = DateTime.Now.ToString("MM");
                string sec = DateTime.Now.ToString("ss");
                User u = Session["User"] as User;

                Saved save = new Saved
                {
                    SaveId = "SA" + day + "" + month + "" + sec,
                    CharId = sav.CharId,
                    UserId = u.UserId
                };
                new DAOController().updateSave(save);
                Characters chars = new DAOController().detailChar(sav.CharId);
                return View("sectionChiTiet", chars);
            }
        }

        public ActionResult setSection(string flag, string Id)
        {
            if (flag.Equals("sectionChiTiet"))
            {
                if (Session["User"] != null)
                {
                    User u = Session["User"] as User;
                    bool checkLike = new DAOController().checkLike(u.UserId, Id);
                    ViewBag.checkLike = checkLike;
                    bool checkSave = new DAOController().checkSave(u.UserId, Id);
                    ViewBag.checkSave = checkSave;
                }
                Characters characters = new DAOController().detailChar(Id);
                return View("sectionChiTiet", characters);
            }
            else if (flag.Equals("sectionCard"))
            {
                List<Card> listCard = new DAOController().getListCard(Id);
                return View("sectionCard", listCard);
            }
            else if (flag.Equals("sectionSkill"))
            {
                Skill skill = new DAOController().getSkill(Id);
                return View("sectionSkill", skill);
            }
            return null;
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string user, string pass)
        {
            if (new DAOController().checkLogin(user, pass) != null)
            {
                User u = new DAOController().checkLogin(user, pass);
                if (u.Role == true)
                {
                    ModelState.AddModelError("", "Sai mật khẩu, tài khoản hoặc tài khoản đã bị khóa!");
                    return View();
                }
                else
                {
                    Session["User"] = u;
                    return RedirectToAction("TrangChu", "Char");
                }
            }
            else
            {
                ModelState.AddModelError("", "Sai mật khẩu, tài khoản hoặc tài khoản đã bị khóa!");
                return View();
            }
        }

        public ActionResult Logout()
        {
            Session.Remove("User");
            ViewBag.Login = "Login";
            ViewBag.CheckLogin = "Đăng nhập/Đăng ký";
            return RedirectToAction("Login");
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(registerViewModel user)
        {
            if (!ModelState.IsValid)
            {
                return View("Register", user);
            }
            else
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

                if (new DAOController().registerUser(use))
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

        public ActionResult comment(string Id)
        {
            List<Feedback> feedbacks = new DAOController().comment(Id);
            return View(feedbacks);
        }

        [HttpPost]
        public ActionResult comment(Feedback feedback)
        {
            //Tạo mã feedback
            DateTime time = DateTime.Now;
            string day = DateTime.Now.ToString("dd");
            string month = DateTime.Now.ToString("MM");
            string year = DateTime.Now.ToString("yyyy");
            string Min = DateTime.Now.ToString("mm");
            string sec = DateTime.Now.ToString("ss");

            feedback.FeedId = "FB" + day + "" + Min + "" + sec;
            feedback.Avatar = feedback.GuestName.Substring(0, 1);
            feedback.Created = time;

            if (new DAOController().addComment(feedback))
            {
                List<Feedback> feedbacks = new DAOController().comment(feedback.CharId);
                return View(feedbacks);
            }
            else
            {
                ModelState.AddModelError("", "Lỗi rồi...");
                List<Feedback> feedbacks = new DAOController().comment(feedback.CharId);
                return View(feedbacks);
            }
        }

        [HttpPost]
        public ActionResult deleteComment(string FeedId, string Id)
        {
            if (new DAOController().deleteComent(FeedId))
            {
                List<Feedback> feedbacks = new DAOController().comment(Id);
                return RedirectToAction("comment", feedbacks);
            }
            else
            {
                ModelState.AddModelError("", "Lỗi rồi...");
                List<Feedback> feedbacks = new DAOController().comment(Id);
                return RedirectToAction("comment", feedbacks);
            }
        }

        public ActionResult User(string Id)
        {
            if (Id == null)
            {
                return RedirectToAction("TrangChu");
            }
            else
            {
                if (Session["User"] == null)
                {
                    ViewBag.Login = "Login";
                    ViewBag.display = "none";
                    ViewBag.CheckLogin = "Đăng nhập/Đăng ký";
                }
                else
                {
                    ViewBag.Login = "TrangChu";
                    ViewBag.display = "static";
                    User u = Session["User"] as User;
                    if (u.Role == true)
                    {
                        ViewBag.CheckLogin = u.FullName + " - Admin";
                    }
                    else
                    {
                        ViewBag.CheckLogin = u.FullName + " - User";
                    }
                }
                if (new DAOController().getUser(Id) != null)
                {
                    UserView usv = new UserView
                    {
                        user = new DAOController().getUser(Id),
                        listLike = new DAOController().userLikeList(Id),
                        listSaved = new DAOController().userSavedList(Id)
                    };
                    return View(usv);
                }
                else
                {
                    return View("TrangChu");
                }
            }
        }

        public ActionResult getUser(string Id)
        {
            if(new DAOController().getUser(Id) != null)
            {
                return View(new DAOController().getUser(Id));
            }
            else
            {
                return RedirectToAction("TrangChu");
            }
        }

        public ActionResult updateUser(User user)
        {
            if (new DAOController().updateUser(user))
            {
                UserView usv = new UserView
                {
                    user = new DAOController().getUser(user.UserId),
                    listLike = new DAOController().userLikeList(user.UserId),
                    listSaved = new DAOController().userSavedList(user.UserId)
                };
                Session.Remove("User");
                Session["User"] = new DAOController().getUser(user.UserId);
                if (Session["User"] == null)
                {
                    ViewBag.Login = "Login";
                    ViewBag.display = "none";
                    ViewBag.CheckLogin = "Đăng nhập/Đăng ký";
                }
                else
                {
                    ViewBag.Login = "TrangChu";
                    ViewBag.display = "static";
                    User u = Session["User"] as User;
                    if (u.Role == true)
                    {
                        ViewBag.CheckLogin = u.FullName + " - Admin";
                    }
                    else
                    {
                        ViewBag.CheckLogin = u.FullName + " - User";
                    }
                }
                return View("User", usv);
            }
            else
            {
                return View("TrangChu");
            }
        }

        public ActionResult gacha()
        {
            return View();
        }

        public ActionResult getcha(int number)
        {
            if (number == null)
            {
                return RedirectToAction("TrangChu");
            }
            else
            {
                return View("getGacha", new DAOController().gacha(number));
            }
        }

        public ActionResult covid19(){
            if (Session["User"] == null)
            {
                ViewBag.Login = "Login";
                ViewBag.display = "none";
                ViewBag.CheckLogin = "Đăng nhập/Đăng ký";
            }
            else
            {
                ViewBag.display = "static";
                User u = Session["User"] as User;
                ViewBag.Login = "User";
                ViewBag.idUser = u.UserId;
                if (u.Role == true)
                {
                    ViewBag.CheckLogin = u.FullName + " - Admin";
                }
                else
                {
                    ViewBag.CheckLogin = u.FullName + " - User";
                }
            }
            return View();
        }
    }
}