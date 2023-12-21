using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DAFW_IS220.Models;

namespace App.DBModels
{
    public class THONGTINGIAOHANG
    {
        [Key]
        public int MATTGH { set; get; }

        public string MATK {set;get;}
        [ForeignKey("MATK")]
        public AppUser TAIKHOAN {set;get;}

        [StringLength(255)]
        [Column(TypeName = "varchar")]
        public string TENNGUOINHAN { set; get; }

        [StringLength(255)]
        [Column(TypeName = "varchar")]
        public string DIACHI { set; get; }

        [StringLength(11)]
        [Column(TypeName = "varchar")]
        public string SDT { set; get; }

        [DataType(DataType.Date)]
        public DateTime NGAYTAO { set; get; }

        [DataType(DataType.Date)]
        public DateTime NGAYHETDUNG { set; get; }        
    }
}