namespace DAL.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Sach")]
    public partial class Sach
    {
        [Key]
        [StringLength(6)]
        public string MaSach { get; set; }

        [Required]
        [StringLength(200)]
        public string TenSach { get; set; }

        public int NamXB { get; set; }

        public int MaLoai { get; set; }
        public virtual LoaiSach LoaiSach { get; set; }
    }
}
