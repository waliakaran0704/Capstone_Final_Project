using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Capstone_Project.Models
{
    public class ResetPasswordModel
    {
        [Required(ErrorMessage = "Required")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Required")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
        public string ResetCode { get; set; }
    }
}