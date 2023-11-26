using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.DBModels
{
    public class HINHANH 
    {
        [Key]
        public int MAHINHANH {set;get;}

        [DisplayName("Tên hình ảnh")]
        [StringLength(200)]
        [Column(TypeName = "varchar")]
        public string TENHINHANH { set; get; }

        [DisplayName("Sản phẩm")]
        public int MASP {set;get;}
        [ForeignKey("MASP")]
        public SANPHAM? SANPHAM {set;get;}
        
        [StringLength(255)]
        [Required]
        [Column(TypeName = "varchar")]
        public string LINK {set;get;}
    }
}