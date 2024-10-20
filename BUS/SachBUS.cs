using DAL.Entities;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class SachBUS
    {
        private SachDAL sachDAL = new SachDAL();

        public List<object> GetAllSachWithLoaiSach()
        {
            return sachDAL.GetAllSachWithLoaiSach();
        }
        public Sach GetSachByMa(string maSach)
        {
            return sachDAL.GetSachByMa(maSach);
        }

        public void DeleteSach(string maSach)
        {
            sachDAL.DeleteSach(maSach);
        }
        public void AddSach(Sach sach)
        {
            sachDAL.AddSach(sach);
        }

        public void UpdateSach(Sach sach)
        {
            sachDAL.UpdateSach(sach);
        }
        public List<Sach> SearchSach(string keyword)
        {
            return sachDAL.SearchSach(keyword);
        }
    }
}
