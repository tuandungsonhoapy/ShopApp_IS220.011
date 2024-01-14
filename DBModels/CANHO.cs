using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DB_CK{
    public class CANHO {
        [Key]
        [StringLength(5)]
        [Column(TypeName = "varchar")]
        public string MaCanHo {set;get;}

        [StringLength(100)]
        [Column(TypeName = "varchar")]
        public string TenChuHo {set;get;}
    }
}