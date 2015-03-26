using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Byporten.Models
{
    public class UserLoginModel
    {
        [Required]
        [Display(Name="Epost")]
        [DataType(DataType.EmailAddress)]
        [StringLength(200)]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Passord")]
        [DataType(DataType.Password)]
        [MinLength(6), MaxLength(25)]
        public string Password { get; set; }

    }
    public class AdminModel
    {
        [Required]
        [Display(Name = "Brukernavn")]
        [StringLength(100)]
        public string Username { get; set; }

        [Required]
        [Display(Name = "Passord")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }

    public class UserCreateModel
    {

    }
}