using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DB_CK{
    public class CHUYENBAY {
        [Key]
        [StringLength(5)]
        [Column(TypeName = "varchar")]
        public string MACH {set;get;}

        [StringLength(100)]
        [Column(TypeName = "varchar")]
        public string CHUYEN {set;get;}

        [StringLength(100)]
        [Column(TypeName = "varchar")]
        public string DDI {set;get;}

        [StringLength(100)]
        [Column(TypeName = "varchar")]
        public string DDEN {set;get;}

        [DataType(DataType.Date)]
        public DateTime? NGAY { set; get; }

        [StringLength(100)]
        [Column(TypeName = "varchar")]
        public string GBAY {set;get;}

        [StringLength(100)]
        [Column(TypeName = "varchar")]
        public string GDEN {set;get;}

        public int THUONG {set;get;}

        public int VIP {set;get;}

        public string MAMB { set; get; }
        [ForeignKey("MAMB")]
        public MAYBAY MAYBAY { set; get; }
    }
}