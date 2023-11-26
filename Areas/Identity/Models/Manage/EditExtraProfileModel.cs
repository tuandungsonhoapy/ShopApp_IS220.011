using System;
using System.ComponentModel.DataAnnotations;

namespace App.Areas.Identity.Models.ManageViewModels
{
  public class EditExtraProfileModel
  {
      [Display(Name = "Tên tài khoản")]
      public string UserName { get; set; }

      [Display(Name = "Địa chỉ email")]
      public string UserEmail { get; set; }
      [Display(Name = "Số điện thoại")]
      public string PhoneNumber { get; set; }

      [Display(Name = "Tên khách hàng")]
      [StringLength(200)]
      public string? TenKH { get; set; }

      [Display(Name = "Giới tính")]
      [StringLength(5)]
      public string? GioiTinh {set;get;}

      [Display(Name = "Ngày đăng ký")]
      [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
      public DateTime? NGAYDK { get; set; }
  }
}