using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Byporten
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            ModelValidatorProviders.Providers.Clear();
            ModelValidatorProviders.Providers.Add(new DissalowHtmlMetadataValidationProvider());
        }
        protected void Application_BeginRequest()
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1));
            Response.Cache.SetNoStore();
        }
    }

    public class DissalowHtmlAttributes : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
                return ValidationResult.Success;
            var tagWithoutClosingRegex = new Regex(@"<[^>]+>");
            var hasTags = tagWithoutClosingRegex.IsMatch(value.ToString());

            if (!hasTags)
                return ValidationResult.Success;
            return new ValidationResult("Feltet kan ikke inneholde HTML eller XML tagger!");
        }
    }

    public class DissalowHtmlMetadataValidationProvider : DataAnnotationsModelValidatorProvider
    {
        protected override IEnumerable<ModelValidator> GetValidators(ModelMetadata metadata, ControllerContext context, IEnumerable<Attribute> attributes)
            {
                if (attributes == null)
                    return base.GetValidators(metadata, context, null);
                if (string.IsNullOrEmpty(metadata.PropertyName))
                    return base.GetValidators(metadata, context, attributes);
                //DisallowHtml should not be added if a property allows html input
                var isHtmlInput = attributes.OfType<AllowHtmlAttribute>().Any();
                if (isHtmlInput)
                    return base.GetValidators(metadata, context, attributes);
               attributes = new List<Attribute>(attributes) { new DissalowHtmlAttributes() };

                return base.GetValidators(metadata, context, attributes);
            }
    }
}
