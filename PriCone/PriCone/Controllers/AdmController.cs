using PriCone.Models;
using PriCone.Models.dataModels;
using PriCone.Models.viewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System;
using System.IO;

namespace PriCone.Controllers
{
    public class AdmController : Controller
    {
        // GET: Adm/TrangChu
        public ActionResult TrangChu()
        {
            User u = Session["User"] as User;
            if (Session["User"] == null || u.Role == false)
            {
                return RedirectToAction("Login", "Char");
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult ThemNhanVat()
        {
            User u = Session["User"] as User;
            if (Session["User"] == null || u.Role == false)
            {
                return RedirectToAction("Login", "Char");
            }
            else
            {
                List<Guild> listGuild = new DAOController().getListGuild();
                List<Characters> listChar = new DAOController().getAllChar();
                ViewBag.listGuild = listGuild;
                ViewBag.listChar = listChar;
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ThemNhanVat(addViewModel model)
        {
            {
                //Tạo mã nhân vật
                DateTime time = DateTime.Now;
                string day = DateTime.Now.ToString("dd");
                string month = DateTime.Now.ToString("MM");
                string year = DateTime.Now.ToString("yyyy");
                string Min = DateTime.Now.ToString("mm");
                string sec = DateTime.Now.ToString("ss");

                string MaNhanVat = "CH" + day + "" + Min + "" + sec;

                //lấy dữ liệu từ front-end
                string TenNhanVat = model.CharName;
                string ChieuCao = model.Height;
                string CanNang = model.Weight;
                DateTime SinhNhat = (DateTime)model.Birthday;
                string NhomMau = model.BloodType;
                string ChungToc = model.Race;
                string SoThich = model.Hobbies;
                string LongTieng = model.VA;
                string MieuTa = model.Description;
                string ChiTiet = model.Detail;
                string BangHoi = model.GuildId;
                DateTime NgayThem = time;

                //lấy ảnh đã chọn từ front-end
                string fileName = System.IO.Path.GetFileNameWithoutExtension(model.Icon.FileName);
                //string extension = System.IO.Path.GetExtension(create.ImageFile.FileName);
                fileName = fileName + ".jpg";
                string pathSave = "~/Images/charICon/" + fileName;
                fileName = System.IO.Path.Combine(Server.MapPath("~/Images/charICon/"), MaNhanVat + ".jpg");
                model.Icon.SaveAs(fileName);

                Characters characters = new Characters();
                characters.GuildId = BangHoi;
                characters.CharId = MaNhanVat;
                characters.CharName = TenNhanVat;
                characters.Height = ChieuCao;
                characters.Weight = CanNang;
                characters.Birthday = SinhNhat;
                characters.BloodType = NhomMau;
                characters.Race = ChungToc;
                characters.Hobbies = SoThich;
                characters.VA = LongTieng;
                characters.Description = MieuTa;
                characters.Detail = ChiTiet;
                characters.Icon = MaNhanVat + ".jpg";
                characters.Likes = 0;
                characters.Released = NgayThem;

                var skillId = "SK" + day + "" + Min + "" + sec;

                Skill skill = new Skill
                {
                    CharId = MaNhanVat,
                    SkillId = skillId,
                    EnSkill = "Chưa thiết lập",
                    EnUB = "Chưa thiết lập",
                    ExSkill = "Chưa thiết lập",
                    Skill1 = "Chưa thiết lập",
                    Skill2 = "Chưa thiết lập",
                    UB = "Chưa thiết lập"
                };

                if (new DAOController().saveChar(characters) && new DAOController().addSkill(skill))
                {
                    return RedirectToAction("ThemNhanVat", "Adm");
                }
                else
                {
                    ModelState.AddModelError("", "Có lỗi xảy ra rồi");
                    return View(model);
                }
            }
        }

        public ActionResult getChar(string Id)
        {
            Characters charInf = new DAOController().getChar(Id);
            List<Guild> listGuild = new DAOController().getListGuild();
            ViewBag.listGuild = listGuild;
            return View(charInf);
        }

        public ActionResult updateChar(addViewModel addViewModel)
        {
            //lấy dữ liệu từ front-end\
            string MaNhanVat = Request["CharId"];
            string TenNhanVat = addViewModel.CharName;
            string ChieuCao = addViewModel.Height;
            string CanNang = addViewModel.Weight;
            DateTime SinhNhat = (DateTime)addViewModel.Birthday;
            string NhomMau = addViewModel.BloodType;
            string ChungToc = addViewModel.Race;
            string SoThich = addViewModel.Hobbies;
            string LongTieng = addViewModel.VA;
            string MieuTa = addViewModel.Description;
            string ChiTiet = addViewModel.Detail;
            string BangHoi = addViewModel.GuildId;

            //Xóa ảnh cũ đi nếu có
            try
            {
                string path = System.Web.Hosting.HostingEnvironment.MapPath("Images\\charICon" + MaNhanVat + ".jpg");
                FileInfo file = new FileInfo(path);
                if (file.Exists)//check file exsit or not  
                {
                    file.Delete();
                }
            }
            catch (Exception ioExp)
            {
                Console.WriteLine(ioExp.Message);
            }
            //lấy ảnh đã chọn từ front-end
            string fileName = System.IO.Path.GetFileNameWithoutExtension(addViewModel.Icon.FileName);
            //string extension = System.IO.Path.GetExtension(create.ImageFile.FileName);
            fileName = fileName + ".jpg";
            string pathSave = "~/Images/charICon/" + fileName;
            fileName = System.IO.Path.Combine(Server.MapPath("~/Images/charICon"), MaNhanVat + ".jpg");

            //save ảnh lại
            addViewModel.Icon.SaveAs(fileName);

            Characters characters = new Characters();
            characters.CharId = MaNhanVat;
            characters.GuildId = BangHoi;
            characters.CharName = TenNhanVat;
            characters.Height = ChieuCao;
            characters.Weight = CanNang;
            characters.Birthday = SinhNhat;
            characters.BloodType = NhomMau;
            characters.Race = ChungToc;
            characters.Hobbies = SoThich;
            characters.VA = LongTieng;
            characters.Description = MieuTa;
            characters.Detail = ChiTiet;
            characters.Icon = MaNhanVat + ".jpg";

            if (new DAOController().updateChar(characters))
            {
                return RedirectToAction("TrangChu", "Char");
            }
            else
            {
                ModelState.AddModelError("", "Có lỗi xảy ra rồi");
                return RedirectToAction("ThemNhanVat", "Adm");
            }
        }

        public ActionResult deleteChar(string Id)
        {
            // sau này sẽ delete skill, feedback rồi này nọ.....
            try
            {
                if(new DAOController().deleteChar(Id))
                {
                    return RedirectToAction("ThemNhanVat", "Adm");
                }
                else
                {
                    ModelState.AddModelError("", "Có lỗi xảy ra rồi");
                    return RedirectToAction("ThemNhanVat", "Adm");
                }
            }catch(Exception e)
            {
                ModelState.AddModelError("", "Có lỗi xảy ra rồi");
                e.Message.ToString();
                return RedirectToAction("ThemNhanVat", "Adm");
            }
        }

        public ActionResult cardManager(string Id)
        {
            User u = Session["User"] as User;
            if (Session["User"] == null || u.Role == false)
            {
                return RedirectToAction("Login", "Char");
            }
            else
            {
                if (Id == null)
                {
                    var viewModel = new addCard
                    {
                        listChar = new DAOController().getAllChar(),
                    };
                    return View(viewModel);
                }
                else
                {
                    var viewModel = new addCard
                    {
                        CharId = Id,
                        listChar = new DAOController().getAllChar(),
                        MH = new DAOController().getCardDetail("MH", Id),
                        MD = new DAOController().getCardDetail("MD", Id),
                        MX = new DAOController().getCardDetail("MX", Id),
                    };
                    return View(viewModel);
                }
            }
        }

        public ActionResult getCardDetail(string Id)
        {
            var viewModel = new addCard
            {
                CharId = Id,
                listCard = new DAOController().getAllCard("CA", Id),
                MH = new DAOController().getCardDetail("MH", Id),
                MD = new DAOController().getCardDetail("MD", Id),
                MX = new DAOController().getCardDetail("MX", Id),
            };
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult addCardDetail(addCard addCard)
        {
            if(addCard == null)
            {
                List<Characters> listChar = new DAOController().getAllChar();
                ViewBag.listChar = listChar;
                return RedirectToAction("cardManager", "Adm");
            }
            else
            {
                //Tạo mã ảnh
                DateTime time = DateTime.Now;
                string day = DateTime.Now.ToString("dd");
                string month = DateTime.Now.ToString("MM");
                string year = DateTime.Now.ToString("yyyy");
                string Min = DateTime.Now.ToString("mm");
                string sec = DateTime.Now.ToString("ss");

                string MaAnh = addCard.flag + day + "" + Min + "" + sec;

                //Xóa ảnh cũ đi nếu có
                try
                {
                    string path = System.Web.Hosting.HostingEnvironment.MapPath("Images\\charCard" + addCard.flag + addCard.CharId + ".png");
                    FileInfo file = new FileInfo(path);
                    if (file.Exists)//check file exsit or not  
                    {
                        file.Delete();
                    }
                }
                catch (Exception ioExp)
                {
                    Console.WriteLine(ioExp.Message);
                }

                //lấy ảnh đã chọn từ front-end
                string fileName = System.IO.Path.GetFileNameWithoutExtension(addCard.Card.FileName);
                //string extension = System.IO.Path.GetExtension(create.ImageFile.FileName);
                fileName = fileName + ".png";
                string pathSave = "~/Images/charCard/" + fileName;
                var flag = addCard.flag;
                if (addCard.flag.Equals("CA"))
                {
                    flag = Min + sec + addCard.flag;
                }

                fileName = System.IO.Path.Combine(Server.MapPath("~/Images/charCard/"), flag + addCard.CharId + ".png");
                addCard.Card.SaveAs(fileName);

                Card card = new Card();
                card.CardId = MaAnh;
                card.CharId = addCard.CharId;
                card.Card1 = flag + addCard.CharId + ".png";

                if (new DAOController().addCardDetail(card))
                {
                    return RedirectToAction("cardManager", "Adm", new { Id = addCard.CharId });
                }
                else
                {
                    ModelState.AddModelError("", "Có lỗi xảy ra rồi");
                    return RedirectToAction("cardManager", "Adm", new { Id = addCard.CharId });
                }
            }
        }

        [HttpPost]
        public ActionResult deleteCard(string IdCard)
        {
            //Xóa ảnh cũ đi nếu có
            if (new DAOController().deleteCard(IdCard))
            {
                string path = System.Web.Hosting.HostingEnvironment.MapPath("~/Images/charCard/" + IdCard);
                FileInfo file = new FileInfo(path);
                if (file.Exists)//check file exsit or not  
                {
                    file.Delete();
                }
                return RedirectToAction("cardManager", "Adm");
            }
            else
            {
                ModelState.AddModelError("", "Có lỗi xảy ra rồi");
                return RedirectToAction("cardManager", "Adm");
            }
        }

        public ActionResult skillManager()
        {
            var charList = new DAOController().getAllChar();
            return View(charList);
        }

        public ActionResult getSkill(string Id)
        {
            var skill = new DAOController().getSkill(Id);
            return View("getSkill", skill);
        }

        public ActionResult updateSkill(Skill skill)
        {
            Skill ski = new Skill
            {
                SkillId = skill.SkillId,
                EnSkill = skill.EnSkill,
                EnUB = skill.EnUB,
                ExSkill = skill.ExSkill,
                Skill1 = skill.Skill1,
                Skill2 = skill.Skill2,
                UB = skill.UB
            };
            if (new DAOController().updateSkill(ski))
            {
                var charList = new DAOController().getAllChar();
                return RedirectToAction("skillManager", charList);
            }
            else
            {
                ModelState.AddModelError("", "Có lỗi xảy ra rồi");
                var charList = new DAOController().getAllChar();
                return RedirectToAction("skillManager", charList);
            }
        }
    }

    //Xóa tất cả hình, xài vòng for xóa card và xóa icon theo charId
}