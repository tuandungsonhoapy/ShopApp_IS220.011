using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.DBModels
{
    public class PL_SP 
    {
        [Key]
        [DisplayName("Mã phân loại")]
        public int MAPL {set;get;}
        
        [StringLength(100)]
        [Required]
        [Column(TypeName = "varchar")]
        [DisplayName("Tên phân loại")]
        public string TENPL {set;get;}
    }
}