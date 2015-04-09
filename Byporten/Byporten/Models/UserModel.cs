using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Byporten.Models
{
    public class UserRegModel
    {

        [Required]
        [Display(Name = "Navn*")]
        [MinLength(3), MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Epost*")]
        [DataType(DataType.EmailAddress)]
        [MinLength(5), MaxLength(200)]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Burdag*")]
        public string Birthdate { get; set; }

        [MinLength(3)]
        [Display(Name="Postkode")]
        public string ZipCode { get; set; }

        [MinLength(3)]
        [Display(Name="By")]
        public string City { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MinLength(7), MaxLength(250)]
        [Display(Name = "Passord")]
        public string Password { get; set; }
    
    }
    public class UserLoginModel {

        [Required]
        public string Email { get; set; }

        [Required]
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
}