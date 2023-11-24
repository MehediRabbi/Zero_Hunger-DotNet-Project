using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Zero_Hunger.Models
{
    public class LoginDTO
    {
        [Required(ErrorMessage ="Enter Your Email")]
        public string Email { get; set; }
        [Required(ErrorMessage ="Enter Your Password")]
        public string pass { get; set; }
    }
}