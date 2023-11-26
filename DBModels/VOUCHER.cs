using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.DBModels
{
    public class VOUCHER 
    {
        [Key]
        public int MAVOUCHER {set;get;}
        
        [StringLength(255)]
        [Column(TypeName = "varchar")]
        public string TENVOUCHER {set;get;}

        [Column(TypeName = "int")]
        public int SOLUONG {set;get;}

        [DataType(DataType.Date)]
        public DateTime THOIGIANBD {set;get;}

        [DataType(DataType.Date)]
        public DateTime THOIGIANKT {set;get;}

        [Column(TypeName = "text")]
        public string MOTA {set;get;}

        [Column(TypeName = "int")]
        public int GIATRIGIAM {set;get;}
    }
}