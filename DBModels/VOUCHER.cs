using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.DBModels
{
    public class VOUCHER 
    {
        [Key]
        public int MAVOUCHER {set;get;}
        
        [StringLength(255)]
        [Column(TypeName = "varchar")]
        [Display(Name = "Mã code voucher")]
        public string TENVOUCHER {set;get;}

        [Column(TypeName = "int")]
        [Display(Name = "Số lượng")]
        public int SOLUONG {set;get;}

        [DataType(DataType.Date)]
        [Display(Name = "Ngày bắt đầu")]
        public DateTime THOIGIANBD {set;get;}

        [DataType(DataType.Date)]
        [Display(Name = "Ngày kết thúc")]
        public DateTime THOIGIANKT {set;get;}

        [Column(TypeName = "text")]
        [Display(Name = "Mô tả")]
        public string MOTA {set;get;}

        [Column(TypeName = "int")]
        [Display(Name = "Giá trị giảm(%)")]
        public int GIATRIGIAM {set;get;}
    }
}