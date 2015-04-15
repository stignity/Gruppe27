public partial class createpost
    {
        public int Id { get; set; }

        [Required(ErrorMessage="Tittel er påkrevd.")]
        [MinLength(6, ErrorMessage="Tittel må være lengre enn 6 tegn."), MaxLength(150, ErrorMessage="Tittelen er for lang.")]
        public string Title { get; set; }

        [Required(ErrorMessage="Dette er påkrevd")]
        [DataType(DataType.Text)]
        [MinLength(1, ErrorMessage="For lite innhold."), MaxLength(3000, ErrorMessage="For mye innhold")]
        public string Content { get; set; }

        [Required(ErrorMessage="Artikkelen må ha en startdato")]
        public string CreateDate { get; set; }

        public string ExpireDate { get; set; }
        public string ImageURL { get; set; }
        public string ExternalLinkURL { get; set; }
    }