﻿using DAL;
using DTO;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace BLL
{
    public class KhachHangBLL
    {
        KhachHangDAL khachHangDAL = new KhachHangDAL();
        IMongoCollection<KhachHang> collection;
        public KhachHangBLL() {
            collection = khachHangDAL.GetKhachHang();
        }
        public List<KhachHang> GetKhachHang()
        {
            var filter = Builders<KhachHang>.Filter.Empty;
            var khachHangs = khachHangDAL.GetKhachHang()
                .Find(filter)
                .ToList();
            return khachHangs;
        }
        public string Them(string ma, string ten, string tuoi, string sdt, string mail, string diem)
        {
            if (ma == "")
            {
                return "Chưa nhập mã! ";
            }
            if (ten == "")
            {
                return "Chưa nhập họ tên! ";
            }
            if (tuoi == "")
            {
                return "Chưa nhập tuổi! ";
            }
            if (sdt == "")
            {
                return "Chưa nhập số điện thoại! ";
            }
            if (mail == "")
            {
                return "Chưa nhập email! ";
            }
            if (diem == "")
            {
                return "Chưa nhập diem! ";
            }
            if (IsNumber(tuoi) == false || Convert.ToInt32(tuoi) < 0)
            {
                return "Nhập tuổi sai";
            }
            if (IsNumber(diem) == false)
            {
                return "Nhập điểm sai";
            }
            if (IsValidEmail(mail) == false)
            {
                return "Nhập mail sai";
            }
            var kh = new KhachHang
            {
                MaKH = ma,Hoten = ten,Tuoi = Convert.ToInt32(tuoi),SDT = sdt,EmailKH = mail, Diem = Convert.ToInt32(diem)
            };
            khachHangDAL.Them(kh);
            return "Thêm thành công";
        }
        public string Xoa(string ma)
        {
            var deleteFilter = Builders<KhachHang>.Filter.Eq(a=>a.MaKH, ma);
            collection.DeleteOne(deleteFilter);
            return "Xóa thành công";
        }
        public string Sua(string ma, string ten, string tuoi, string sdt, string mail, string diem)
        {
            if (ma == "")
            {
                return "Chưa nhập mã! ";
            }
            if (ten == "")
            {
                return "Chưa nhập họ tên! ";
            }
            if (tuoi == "")
            {
                return "Chưa nhập tuổi! ";
            }
            if (sdt == "")
            {
                return "Chưa nhập số điện thoại! ";
            }
            if (mail == "")
            {
                return "Chưa nhập email! ";
            }
            if (diem == "")
            {
                return "Chưa nhập diem! ";
            }
            if (IsNumber(tuoi) == false || Convert.ToInt32(tuoi) < 0)
            {
                return "Nhập tuổi sai";
            }
            if (IsNumber(diem) == false)
            {
                return "Nhập điểm sai";
            }
            if (IsValidEmail(mail) == false)
            {
                return "Nhập mail sai";
            }
            var filter = Builders<KhachHang>.Filter.Eq(a => a.MaKH, ma);
            var update = Builders<KhachHang>.Update
                .Set(a => a.Hoten, ten)
                .Set(a => a.Tuoi, Convert.ToInt32(tuoi))
                .Set(a => a.SDT, sdt)
                .Set(a => a.EmailKH, mail)
                .Set(a => a.Diem, Convert.ToInt32(diem));

            collection.UpdateOne(filter, update);
            return "Sửa thành công";
        }
        public bool IsNumber(string pText)
        {
            Regex regex = new Regex(@"^[-+]?[0-9]*.?[0-9]+$");
            return regex.IsMatch(pText);
        }
        public static bool IsValidEmail(string email)
        {
            string pattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|" + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)" + @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";
            var regex = new Regex(pattern, RegexOptions.IgnoreCase);
            return regex.IsMatch(email);
        }
    }
}
