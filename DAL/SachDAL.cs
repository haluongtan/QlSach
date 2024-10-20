using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class SachDAL
    {
        private QLSach db = new QLSach();

        public List<object> GetAllSachWithLoaiSach()
        {
            var listSach = from s in db.Sach
                           join l in db.LoaiSach on s.MaLoai equals l.MaLoai
                           select new
                           {
                               s.MaSach,
                               s.TenSach,
                               s.NamXB,
                               TenLoai = l.TenLoai
                           };

            return listSach.ToList<object>();
        }
        public Sach GetSachByMa(string maSach)
        {
            return db.Sach.FirstOrDefault(s => s.MaSach == maSach);
        }

        public void DeleteSach(string maSach)
        {
            Sach sach = GetSachByMa(maSach);
            if (sach != null)
            {
                db.Sach.Remove(sach);
                db.SaveChanges();
            }
        }
        public void AddSach(Sach sach)
        {
            db.Sach.Add(sach);
            db.SaveChanges(); 
        }

        public void UpdateSach(Sach sach)
        {
            Sach existingSach = db.Sach.FirstOrDefault(s => s.MaSach == sach.MaSach);
            if (existingSach != null)
            {
                existingSach.TenSach = sach.TenSach;
                existingSach.NamXB = sach.NamXB;
                existingSach.MaLoai = sach.MaLoai;

                db.SaveChanges();
            }
        }
        public List<Sach> SearchSach(string keyword)
        {
            keyword = keyword.ToLower();
            return db.Sach
                .Where(s => s.MaSach.ToLower().Contains(keyword) ||
                            s.TenSach.ToLower().Contains(keyword) ||
                            s.NamXB.ToString().Contains(keyword))
                .ToList();
        }
    }
}
