using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DB_CK{
    public class NHANVIEN {
        [Key]
        [StringLength(5)]
        [Column(TypeName = "varchar")]
        public string MaNhanVien {set;get;}

        [StringLength(100)]
        [Column(TypeName = "varchar")]
        public string TenNhanVien {set;get;}

        [StringLength(10)]
        [Column(TypeName = "varchar")]
        public string SoDienThoai {set;get;}

        [StringLength(11)]
        [Column(TypeName = "varchar")]
        public string GioiTinh {set;get;}
    }
}