using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.DBModels
{
    public class CTDH 
    {
        public int MADH {set;get;}
        [ForeignKey("MADH")]
        public DONHANG DONHANG {set;get;}

        public int MACTSP {set;get;}
        [ForeignKey("MACTSP")]
        public CHITIETSANPHAM CHITIETSANPHAM {set;get;}

        [Column(TypeName = "decimal(10,2)")]
        public decimal TONGGIA {set;get;}

        [Column(TypeName = "int")]
        public int SOLUONG {set;get;}
    }
}