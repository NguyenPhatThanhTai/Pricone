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
    public class AdmController : Controller
    {
        // GET: Adm/TrangChu
        public ActionResult TrangChu()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ThemNhanVat()
        {
            List<Guild> listGuild = new DAOController().getListGuild();
            List<Characters> listChar = new DAOController().getAllChar();
            ViewBag.listGuild = listGuild;
            ViewBag.listChar = listChar;
            return View();
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

                if (new DAOController().saveChar(characters))
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
    }
}