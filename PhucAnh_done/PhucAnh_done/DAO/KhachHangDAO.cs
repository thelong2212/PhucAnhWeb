﻿using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PhucAnh_done.Models;

namespace PhucAnh_done.DAO
{
    public class KhachHangDAO
    {
        ApplicationDbContext db = null;
        private static KhachHangDAO instance;
        static object key = new object();
        public static KhachHangDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (key)//bất đồng bộ , chiếm dụng tài nguyên....
                    {
                        instance = new KhachHangDAO();
                    }
                }
                return instance;
            }
        }
        public KhachHangDAO() { db = new ApplicationDbContext(); }
        public int CreateKhachHang(KhachHang kh)
        {
            try
            {
                db.KhachHangs.Add(kh);
                db.SaveChanges();
                return kh.KhachHangID;
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public bool Update(KhachHang entity)
        {
            try
            {
                var khachHang = db.KhachHangs.Find(entity.KhachHangID);
                khachHang.HoTen = entity.HoTen;
                khachHang.NgaySinh = entity.NgaySinh;
                khachHang.SoDienThoai = entity.SoDienThoai;
                khachHang.Email = entity.Email;
                khachHang.GhiChu = entity.GhiChu;
                khachHang.DiaChi = entity.DiaChi;
                if (entity.GioiTinh == true || entity.GioiTinh == false)
                {
                    khachHang.GioiTinh = entity.GioiTinh;
                }
                else
                {
                    khachHang.GioiTinh = true;
                }
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var khachHang = db.KhachHangs.Find(id);
                db.KhachHangs.Remove(khachHang);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public KhachHang ViewDetail(int khachHangID)
        {
            return db.KhachHangs.Find(khachHangID);
        }
        public IEnumerable<KhachHang> ListAllpaging(string searchString, int page, int pageSize)
        {
            IQueryable<KhachHang> model = db.KhachHangs;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.HoTen.Contains(searchString) || x.Email.Contains(searchString));
            } 

            return model.OrderByDescending(x => x.KhachHangID).ToPagedList(page, pageSize);
        }

    }
}