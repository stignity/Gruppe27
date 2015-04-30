using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Byporten.Models
{
    public class UserRegModel
    {
        [Required(ErrorMessage="Feltet er påkrevet")]
        [Display(Name = "Navn*")]
        [RegularExpression(@"^[ÆØÅæøåa-zA-Z ]+$", ErrorMessage="Kun alfabetiske tegn")]
        [MinLength(3, ErrorMessage="Navnet er for kort"), MaxLength(100, ErrorMessage="Navnet er for langt")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Feltet er påkrevet")]
        [Display(Name = "Kjønn*")]
        public string Gender { get; set; }

        []
        [Required(ErrorMessage = "Feltet er påkrevet")]
        [Display(Name = "Epost*")]
        [DataType(DataType.EmailAddress, ErrorMessage="Det må være en gyldig e-postadresse")]
        [Remote("CheckEmail", "Home", ErrorMessage="Email er i bruk, velg en annen")]
        [MinLength(5), MaxLength(200)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Feltet er påkrevet")]
        [Display(Name="Gjenta Epost*")]
        [DataType(DataType.EmailAddress, ErrorMessage="Feil e-post")]
        public string RepeatEmail { get; set; }

        [Required(ErrorMessage = "Feltet er påkrevet")]
        [MinLength(8), MaxLength(15, ErrorMessage="Må være et gyldig nummer")]
        [Display(Name="Mobilnummer")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Feltet er påkrevet")]
        [Display(Name = "Bursdag*")]
        public string Birthdate { get; set; }

        [Display(Name="Interesser")]
        public string Interests { get; set; }

        [Display(Name="Antall barn")]
        [MaxLength(5, ErrorMessage="Venligst velg antall barn")]
        public string Children { get; set; }

        [MinLength(3, ErrorMessage="For kort")]
        [Display(Name="Postkode")]
        public string ZipCode { get; set; }

        [MinLength(3, ErrorMessage="Fort kort")]
        [Display(Name="By")]
        public string City { get; set; }

        [Required(ErrorMessage = "Feltet er påkrevet")]
        [DataType(DataType.Password)]
        [MinLength(7, ErrorMessage="Passordet må inneholde minst 7 tegn"), MaxLength(250)]
        [Display(Name = "Passord")]
        public string Password { get; set; }

        [Display(Name="Hvordan fikk du vite om Byporten+?")]
        public string Knowledge { get; set; }
    
    }
    public class UserLoginModel {

        [Required(ErrorMessage="Kan ikke være tom")]
        [DataType(DataType.EmailAddress, ErrorMessage="Må være en epost")]
        public string Email { get; set; }

        [Required(ErrorMessage="Husk passord")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
    public class AdminModel
    {

        [Required(ErrorMessage="Feltet er påkrevet")]
        [Display(Name = "Brukernavn")]
        [StringLength(100)]
        public string Username { get; set; }

        [Required(ErrorMessage = "Feltet er påkrevet")]
        [Display(Name = "Passord")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}