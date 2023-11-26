using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.DBModels
{
    public class CHITIETSANPHAM 
    {
        [Key]
        public int MACTSP {set;get;}
        
        [DisplayName("Màu")]
        public int MAMAU {set;get;}
        [ForeignKey("MAMAU")]
        [DisplayName("Mã Màu")]
        public MAUSAC? MAUSAC {set;get;}

        [DisplayName("Size")]
        public int MASIZE {set;get;}
        [ForeignKey("MASIZE")]
        [DisplayName("Mã Size")]
        public SIZE? SIZE {set;get;}
         
        [DisplayName("Sản phẩm")]
        public int MASP {set;get;}
        [ForeignKey("MASP")]
        [DisplayName("Mã sản phẩm")]
        public SANPHAM? SANPHAM {set;get;}

        [Column(TypeName = "int")]
        [DisplayName("Số lượng")]
        public int SOLUONG {set;get;}
    }
}