using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.DBModels
{
    public class SANPHAM 
    {
        [Key]
        public int MASP {set;get;}
        
        [StringLength(100)]
        [Column(TypeName = "varchar")]
        [Required(ErrorMessage = "Phải nhập {0}")]
        [DisplayName("Tên sản phẩm")]
        public string TENSP {set;get;}

        [Column(TypeName = "decimal(10,2)")]
        [DisplayName("Giá gốc")]
        public decimal GIAGOC {set;get;}

        [DisplayName("Danh mục")]
        public int MAPL {set;get;}
        [ForeignKey("MAPL")]
        [DisplayName("Danh mục")]
        public PL_SP? PL_SP {set;get;}

        [StringLength(30)]
        [Column(TypeName = "varchar")]
        [Required(ErrorMessage = "Phải nhập {0}")]
        [DisplayName("Phân loại")]
        public string PLTHOITRANG {set;get;}

        [Column(TypeName = "longtext")]
        [DisplayName("Mô tả")]
        public string MOTA {set;get;}

        [Column(TypeName = "decimal(10,2)")]
        [DisplayName("Giá bán")]
        public decimal GIABAN {set;get;}

        [StringLength(255)]
        [Required]
        [Column(TypeName = "varchar")]
        [DisplayName("Ảnh chính cho sản phẩm")]
        public string MainImg {set;get;}

        public List<HINHANH>? Image {set;get;}
    }
}