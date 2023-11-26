using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace App.DBModels
{
    public class SIZE 
    {
        [Key]
        public int MASIZE {set;get;}
        
        [StringLength(10)]
        [Required]
        [Column(TypeName = "varchar")]
        public string Size {set;get;}
    }
}