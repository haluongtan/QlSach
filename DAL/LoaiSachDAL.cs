using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using DAL.Entities;
namespace DAL
{
    public class LoaiSachDAL
    {
        private QLSach db = new QLSach();

        public List<LoaiSach> GetAllLoaiSach()
        {
            return db.LoaiSach.ToList();
        }
    }
}
