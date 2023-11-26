using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.DBModels
{
    public class THANHTOAN 
    {
        [Key]
        public int MATHANHTOAN {set;get;}

        public int MADH {set;get;}
        [ForeignKey("MADH")]
        public DONHANG DONHANG {set;get;}

        [Column(TypeName = "decimal(10,2)")]
        public decimal SOTIEN {set;get;}

        [DataType(DataType.Date)]
        public DateTime NGAYTHANHTOAN {set;get;}
    }
}