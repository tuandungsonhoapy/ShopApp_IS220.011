using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.DBModels
{
    public class MAUSAC 
    {
        [Key]
        public int MAMAU {set;get;}
        
        [DisplayName("Tên màu")]
        [StringLength(50)]
        [Required]
        [Column(TypeName = "varchar")]
        public string TENMAU {set;get;}

        [StringLength(50)]
        [Required]
        [Column(TypeName = "varchar")]
        public string HEX {set;get;}
    }
}