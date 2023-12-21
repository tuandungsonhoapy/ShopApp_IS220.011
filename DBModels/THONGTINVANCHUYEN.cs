using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DAFW_IS220.Models;

namespace App.DBModels
{
    public class THONGTINVANCHUYEN
    {
        public int MADH { set; get; }
        [ForeignKey("MADH")]
        public DONHANG DONHANG {set;get;}

        public int MATTGH {set;get;}
        [ForeignKey("MATTGH")]
        public THONGTINGIAOHANG THONGTINGIAOHANG {set;get;}

        [DataType(DataType.Date)]
        public DateTime NGAYGIAO { set; get; }

        [DataType(DataType.Date)]
        public DateTime NGAYKETTHUC { set; get; }        
    }
}