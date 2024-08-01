using QLAdmin.Areas.Admin.Data;
using QLAdmin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace QLAdmin.Areas.Admin.Controllers
{
    public class QLNguoidungController : Controller
    {
        // GET: Admin/QLNguoidung
        private QLTuisach _context = new QLTuisach();
        public ActionResult QLNguoidung()
        {

            var lstnguoidung = (from s in _context.Customers
                           orderby s.CustomerID ascending //descending
                           select new NguoidungDisplayVM()
                           {
                               CustomerID = s.CustomerID,
                               FullName = s.FullName,
                               SoDienThoai = s.SoDienThoai,
                               Email = s.Email,
                               VaiTro  = s.VaiTro
                           }).ToList();
            ViewBag.message = lstnguoidung.Count();
            return View(lstnguoidung);
        }

        public ActionResult DetailNguoidung(int id)
        {
            var item = _context.Customers
            .Where(x => x.CustomerID == id)
            .Select(t => new NguoiDungVM
            {
                CustomerID = t.CustomerID,
                FullName = t.FullName,
                NgaySinh=t.NgaySinh,
                SoDienThoai=t.SoDienThoai,
                DiaChi = t.DiaChi,
                Email= t.Email,
                ProfileImage=t.ProfileImage,
                Username =t.Username,
                Password = t.Password,
                VaiTro = t.VaiTro

            })
            .FirstOrDefault();

            if (item == null)
            {
                return HttpNotFound();
            }

            return View(item);
        }
        [HttpGet]
        public ActionResult AddND()
        {
            return View();

        }
        [HttpPost]
        public ActionResult AddND(NguoiDungVM formData)
        {
            var item = new Customer();
            item.FullName = formData.FullName;
            item.SoDienThoai = formData.SoDienThoai;
            item.NgaySinh = formData.NgaySinh;
            item.DiaChi = formData.DiaChi;
            item.Email = formData.Email;
            item.ProfileImage = formData.ProfileImage;
            item.Username = formData.Username;
            item.Password = formData.Password;
            item.VaiTro = formData.VaiTro;
            _context.Customers.Add(item);

            _context.SaveChanges();// save to DB
            return RedirectToAction("QLnguoidung", "QLNguoidung");
        }

        [HttpGet]
        public ActionResult EditNguoidung(int id)
        {
            //var cd = _context.ChuDe.Where(x => x.MaChuDe == id).Select(item=>new ChuDeVM()
            //{
            //    MaCD = item.MaChuDe,
            //    TenCD = item.TenChuDe,
            //}).FirstOrDefault();

            var nd = (from item in _context.Customers
                      where item.CustomerID == id
                      select new NguoiDungVM()
                      {
                          CustomerID = item.CustomerID,
                          FullName = item.FullName,
                          SoDienThoai =item.SoDienThoai,
                          NgaySinh = item.NgaySinh,
                          DiaChi = item.DiaChi,
                          Email = item.Email,
                          ProfileImage = item.ProfileImage,
                          Username = item.Username,
                          Password = item.Password,
                          VaiTro = item.VaiTro
                      }).FirstOrDefault();
            if (nd == null)
            {
                return RedirectToAction("QLNguoidung", "QLNguoidung");
            }
            return View(nd);
        }
        [HttpPost]
        public ActionResult EditNguoidung(NguoiDungVM formData)
        {
            var item = _context.Customers.Where(x => x.CustomerID == formData.CustomerID).FirstOrDefault();
            if (item == null)
            {
                return RedirectToAction("Index", "QLSize");
            }

            item.FullName = formData.FullName;
            item.SoDienThoai = formData.SoDienThoai;
            item.NgaySinh = formData.NgaySinh;
            item.DiaChi = formData.DiaChi;
            item.Email = formData.Email;
            item.ProfileImage = formData.ProfileImage;
            item.Username = formData.Username;
            item.Password = formData.Password;
            item.VaiTro = formData.VaiTro;
            _context.SaveChanges();

            return RedirectToAction("QLNguoidung", "QLNguoidung");
        }
        [HttpGet]
        public ActionResult DeleteND(int id)
        {
            var item = _context.Customers.Where(x => x.CustomerID == id).Select(x => new NguoiDungVM()
            {
                CustomerID = x.CustomerID,
                FullName = x.FullName,
                SoDienThoai = x.SoDienThoai,
                NgaySinh = x.NgaySinh,
                DiaChi = x.DiaChi,
                Email = x.Email,
                ProfileImage = x.ProfileImage,
                Username = x.Username,
                Password = x.Password,
                VaiTro = x.VaiTro
            }).FirstOrDefault();
            return View(item);
        }
        [HttpPost, ActionName("DeleteND")]
        public ActionResult ConfirmDeleteND(int id)
        {
            var item = _context.Customers.Where(x => x.CustomerID == id).FirstOrDefault();
            if (item == null)
            {
                return RedirectToAction("QLNguoidung", "QLNguoidung");
            }
            _context.Customers.Remove(item);

            _context.SaveChanges();

            return RedirectToAction("QLNguoidung", "QLNguoidung");
        }
    }
}