using DAL.Entities;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class LoaiSachBUS
    {
        private LoaiSachDAL loaiSachDAL = new LoaiSachDAL();

        public List<LoaiSach> GetAllLoaiSach()
        {
            return loaiSachDAL.GetAllLoaiSach();
        }
    }
}
