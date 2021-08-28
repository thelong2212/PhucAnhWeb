﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PhucAnh_done.Models;

namespace PhucAnh_done.Common
{
    public class DonHangDAO
    {
        private readonly ApplicationDbContext db;
        // insert, update, delete
        private static DonHangDAO instance;
        static object key = new object();
        public static DonHangDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DonHangDAO();
                }
                return instance;
            }
        }
        public DonHangDAO()
        {
            db = new ApplicationDbContext();
        }
        // insert 
        public int insertDonHang(DonHang donHang)
        {
            try
            {
                db.DonHangs.Add(donHang);
                db.SaveChanges();
                return donHang.DonHangID;
            }
            catch (Exception)
            {
                return 0; // them that bai
            }
        }
    }
}