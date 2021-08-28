using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PhucAnh_done.DAO;
using PhucAnh_done.Models;

namespace PhucAnh_done.Controllers
{
    public class SearchController : Controller
    {
        // GET: Search
        public ActionResult Index(string Keyword, int page = 1, int pageSize=8)
        {
            using(var db=new ApplicationDbContext())
            {
                var dao = new SanPhamDAO();
                var model = dao.ListAllPaging(Keyword, page, pageSize);
                var phanLoaiSanPham = db.PhanLoaiSanPhams.ToList();
                var danhMucSanPham = db.DanhMucSanPhams.ToList();
                var loaiDanhMucSanPham = db.LoaiDanhMucSanPhams.ToList();
                var phanLoaiTheoHang = db.PhanLoaiTheoHangSps.ToList();
                var sanPham = db.SanPhams.ToList();
                var loaiDMSP = from dmsp in danhMucSanPham
                               join loaiDM in loaiDanhMucSanPham on dmsp.DanhMucSanPhamPID equals loaiDM.DanhMucSanPhamPID
                               where dmsp.DanhMucSanPhamPID == loaiDM.DanhMucSanPhamPID
                               select dmsp;

                var DanhMuc = from plsp in phanLoaiSanPham
                              join dm in danhMucSanPham on plsp.DanhMucSanPhamID equals dm.DanhMucSanPhamID
                              where dm.Status == true
                              select plsp;
                ViewData["phanLoaiTheoHang"] = phanLoaiTheoHang.ToList();
                ViewData["loaiDanhMucSanPham"] = loaiDanhMucSanPham.ToList();
                ViewData["loaiDMSP"] = loaiDMSP.ToList();
                ViewData["DanhMuc"] = DanhMuc.ToList();
                ViewBag.Keyword = Keyword;
                return View(model);
            }
        }
    }
}