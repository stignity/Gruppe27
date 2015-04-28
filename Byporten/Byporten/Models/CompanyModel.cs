using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;
namespace Byporten.Models
{
    public class PostModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string CreateDate { get; set; }
        public string ExpireDate { get; set; }
        public string ImageURL { get; set; }
        public string ExternalLinkURL { get; set; }

    }

    public class StoreModel
    {
        public int Id { get; set; }
        public string Navn { get; set; }
        public string Kategori { get; set; }
        public string Beskrivelse { get; set; }
        public string Logo { get; set; }
        public string Telefon { get; set; }
        public string Hjemmeside { get; set; }

    }

    public class PositionModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string CreateDate { get; set; }
        public string ExpireDate { get; set; }
        public string ImageURL { get; set; }
        public string ExternalLinkURL { get; set; }

    }

    public class OfferModel
    {
        public int Id { get; set; }
        public string Tittel { get; set; }
        public string Innhold { get; set; }
        public string Startdato { get; set; }
        public string Sluttdato { get; set; }
        public string Bilde { get; set; }
    }

    public class CompanyDBContext 
    {
        public List<PostModel> PostModels { get; set; }
        public List<StoreModel> StoreModels { get; set; }
        public List<PositionModel> PositionModels { get; set; }
        public List<OfferModel> OfferModels { get; set; }
    }
}