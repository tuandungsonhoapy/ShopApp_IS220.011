using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace DAFW_IS220.Models
{
    public class AppUser : IdentityUser
    {
        [StringLength(200)]
        [Column(TypeName = "varchar")]
        public string TENKH { set; get; }

        [StringLength(5)]
        [Column(TypeName = "varchar")]
        public string GIOITINH { set; get; }

        [StringLength(255)]
        [Column(TypeName = "varchar")]
        public string? DIACHI { set; get; }

        // [Required]     
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]  
        [DataType(DataType.Date)]
        public DateTime NGAYDK { get; set; }
    }
}