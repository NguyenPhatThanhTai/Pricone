using PriCone.Models.checkValid;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PriCone.Models.viewModels
{
    public class registerViewModel
    {
        [StringLength(8)]
        public string UserId { get; set; }

        public bool Role { get; set; }

        [Required]
        [StringLength(100)]
        [MinLength(10, ErrorMessage = "UserName ít nhất 10 kí tự!")]
        public string Username { get; set; }

        [Required]
        [StringLength(100)]
        [MinLength(10, ErrorMessage = "Mật khẩu ít nhất 10 kí tự!")]
        public string Password { get; set; }

        [StringLength(100)]
        public string FullName { get; set; }

        [Column(TypeName = "date")]
        //[checkBirthDay(ErrorMessage = "Ngày sinh không được lớn hơn ngày hiện tại!")]
        public DateTime? Birthday { get; set; }

        [StringLength(200)]
        public string Address { get; set; }

        [StringLength(11)]
        [MinLength(11, ErrorMessage = "Số điện thoại ít nhất 11 số!")]
        public string Phone { get; set; }

        [Required]
        [StringLength(100)]
        public string Email { get; set; }

        public bool? Status { get; set; }
        public HttpPostedFileBase Avatar { get; set; }
    }
}