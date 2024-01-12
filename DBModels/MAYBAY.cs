using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DB_CK{
    public class MAYBAY {
        [Key]
        [StringLength(5)]
        [Column(TypeName = "varchar")]
        public string MAMB {set;get;}

        [StringLength(200)]
        [Column(TypeName = "varchar")]
        public string HANGMB {set;get;}

        public int SOCHO {set;get;}
    }
}