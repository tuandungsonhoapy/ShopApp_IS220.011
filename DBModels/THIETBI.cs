using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DB_CK{
    public class THIETBI {
        [Key]
        [StringLength(5)]
        [Column(TypeName = "varchar")]
        public string MaThietBi {set;get;}

        [StringLength(100)]
        [Column(TypeName = "varchar")]
        public string TenThietBi {set;get;}
    }
}