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
        [Required]
        [Display(Name = "Fullt Navn")]
        public string FullName { get; set; }

        [Required]
        [Display(Name = "Epost")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Fødselsdag")]
        [DataType(DataType.Date)]
        public string Birthday { get; set; }

        [Required]
        [Display(Name = "Postnummer")]
        public string ZipCode { get; set; }

        [Required]
        [Display(Name = "By")]
        public string City { get; set; }

        [Required]
        [Display(Name = "Velg passord")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }

    public class UserLoginRegisterModel
    {
        public UserLoginModel UserLoginModel { get; set; }
        public UserCreateModel UserCreateModel { get; set; }
    }
}