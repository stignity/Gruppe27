//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Byporten
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    
    public partial class createpost
    {
        public int Id { get; set; }

        [Required(ErrorMessage="Tittel er p�krevd.")]
        [MinLength(6, ErrorMessage="Tittel m� v�re lengre enn 6 tegn."), MaxLength(150, ErrorMessage="Tittelen er for lang.")]
        public string Title { get; set; }

        [Required(ErrorMessage="Dette er p�krevd")]
        [DataType(DataType.Text)]
        [MinLength(1, ErrorMessage="For lite innhold."), MaxLength(3000, ErrorMessage="For mye innhold")]
        public string Content { get; set; }

        [Required(ErrorMessage="Artikkelen m� ha en startdato")]
        public string CreateDate { get; set; }

        public string ExpireDate { get; set; }
        public string ImageURL { get; set; }
        public string ExternalLinkURL { get; set; }
    }
}
