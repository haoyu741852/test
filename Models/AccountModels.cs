using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Carts.Models
{
    public class AccountModels
    {
    }

    public class LoginViewModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "{0} 的長度至少必須為 {2} 個字元!", MinimumLength = 1)]
        [Display(Name = "暱稱")]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "電子郵件")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} 的長度至少必須為 {2} 個字元!", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "密碼")]
        public string Pw { get; set; }
    }

    public class RegisterViewModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "{0} 的長度至少必須為 {2} 個字元!", MinimumLength = 1)]
        [Display(Name = "暱稱")]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "電子郵件")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} 的長度至少必須為 {2} 個字元!", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "密碼")]
        public string Pw { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "確認密碼")]
        [Compare("Pw", ErrorMessage = "密碼和確認密碼不相符!")]
        public string ConfirmPw { get; set; }
    }
}