using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DB_CK{
    public class HANHKHACH {
        [Key]
        [StringLength(5)]
        [Column(TypeName = "varchar")]
        public string MAHK {set;get;}

        [StringLength(100)]
        [Column(TypeName = "varchar")]
        public string HOTEN {set;get;}

        [StringLength(200)]
        [Column(TypeName = "varchar")]
        public string DIACHI {set;get;}

        [StringLength(11)]
        [Column(TypeName = "varchar")]
        public string DIENTHOAI {set;get;}
    }
}