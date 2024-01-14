using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DB_CK{
    public class NV_BT {
        public string MaNhanVien {set;get;}
        [ForeignKey("MaNhanVien")]
        public NHANVIEN NHANVIEN;

        public string MaThietBi {set;get;}
        [ForeignKey("MaThietBi")]
        public THIETBI THIETBI;

        public string MaCanHo {set;get;}
        [ForeignKey("MaCanHo")]
        public CANHO CANHO;

        public int LanThu {set;get;}

        [DataType(DataType.Date)]
        public DateTime? NgayBaoTri {set;get;}
    }
}