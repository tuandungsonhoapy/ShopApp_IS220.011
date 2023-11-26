using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.DBModels
{
    public class VOUCHER_DONHANG 
    {
        public int MAVOUCHER {set;get;}
        [ForeignKey("MAVOUCHER")]
        public VOUCHER VOUCHER {set;get;}

        public int MADH {set;get;}
        [ForeignKey("MADH")]
        public DONHANG DONHANG {set;get;}
    }
}