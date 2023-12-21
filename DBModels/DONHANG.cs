using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DAFW_IS220.Models;

namespace App.DBModels
{
    public class DONHANG
    {
        [Key]
        public int MADH { set; get; }

        public string MATK { set; get; }
        [ForeignKey("MATK")]
        public AppUser TAIKHOAN { set; get; }

        [DataType(DataType.Date)]
        public DateTime NGAYMUA { set; get; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal TONGTIEN { set; get; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal PHIVANCHUYEN { set; get; }

        [DataType(DataType.Date)]
        public DateTime? NGAYGIAO { set; get; }

        [StringLength(100)]
        [Column(TypeName = "varchar")]
        public string HINHTHUCTHANHTOAN { set; get; }

        [StringLength(100)]
        [Column(TypeName = "varchar")]
        public string TRANGTHAITHANHTOAN { set; get; }

        [DisplayName("Trạng thái đơn hàng")]
        [StringLength(100)]
        [Column(TypeName = "varchar")]
        public string TRANGTHAIDONHANG { set; get; }

        public int? MATTGH { set; get; }
        [ForeignKey("MATTGH")]
        public THONGTINGIAOHANG? THONGTINGIAOHANG { set; get; }

        [Column(TypeName = "text")]
        public string GHICHU { set; get; }

        [DataType(DataType.Date)]
        public DateTime NGAYSUADOI { set; get; }
    }
}