using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DB_CK{
    public class CT_CB {
        public string MACH {set;get;}
        [ForeignKey("MACH")]
        public CHUYENBAY CHUYENBAY {set;get;}

        public string MAHK {set;get;}
        [ForeignKey("MAHK")]
        public HANHKHACH HANHKHACH {set;get;}

        [StringLength(5)]
        [Column(TypeName = "varchar")]
        public string SOGHE {set;get;}

        [Column(TypeName = "tinyint(1)")]
        public bool LOAIGHE { get; set; }
    }
}