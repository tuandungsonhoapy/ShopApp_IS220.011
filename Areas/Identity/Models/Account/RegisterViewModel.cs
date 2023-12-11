// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace App.Areas.Identity.Models.AccountViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Phải nhập {0}")]
        [EmailAddress(ErrorMessage = "Sai định dạng Email")]
        [Display(Name = "Email")]
        public string Email { get; set; }


        [Required(ErrorMessage = "Phải nhập {0}")]
        [StringLength(100, ErrorMessage = "{0} phải dài từ {2} đến {1} ký tự.", MinimumLength = 2)]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Lặp lại mật khẩu")]
        [Compare("Password", ErrorMessage = "Mật khẩu lặp lại không chính xác.")]
        public string ConfirmPassword { get; set; }


        [DataType(DataType.Text)]
        [Display(Name = "Tên tài khoản")]
        [Required(ErrorMessage = "Phải nhập {0}")]
        [StringLength(100, ErrorMessage = "{0} phải dài từ {2} đến {1} ký tự.", MinimumLength = 3)]
        public string UserName { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Tên khách hàng")]
        [Required(ErrorMessage = "Phải nhập {0}")]
        [StringLength(100, ErrorMessage = "Phải nhập tên người đăng ký.", MinimumLength = 3)]
        public string TenKH { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Số điện thoại")]
        [Required(ErrorMessage = "Phải nhập {0}")]
        [StringLength(11, ErrorMessage = "Phải nhập số điện thoại.", MinimumLength = 10)]
        public string SDT { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Giới tính")]
        [Required(ErrorMessage = "Phải chọn {0}")]
        public string GioiTinh { get; set; }

    }
}
