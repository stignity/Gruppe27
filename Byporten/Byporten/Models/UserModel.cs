using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Byporten.Models
{
    public class UserModel
    {
        [Required]
        [Display(Name = "Fullt Navn")]
        public string FullName { get; set; }

        [Required]
        [Display(Name = "Epost")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Bursdag")]
        public string Birthday { get; set; }

        [Required]
        [Display(Name = "Postnummer")]
        public int ZipCode { get; set; }

        [Required]
        [Display(Name = "By")]
        public string City { get; set; }

        [Required]
        [Display(Name = "Velg passord")]
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
}