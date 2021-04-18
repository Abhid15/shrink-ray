using System;
using System.ComponentModel.DataAnnotations;

namespace Model.Validation
{
    public class CustomValidation
    {
        public sealed class CheckUrlValid : ValidationAttribute
        {
            /// <summary>
            ///  In custom validation class, we are accepting full urls as valid (either http or https).
            /// </summary>
            /// <param name="Url"></param>
            /// <param name="validationContext"></param>
            /// <returns></returns>
            protected override ValidationResult IsValid(object Url, ValidationContext validationContext)
            {

                Uri uriResult;
                bool result = Uri.TryCreate(Url.ToString(), UriKind.Absolute, out uriResult)
                    && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

                if (result)
                {
                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult("Please enter a valid Url");
                }
            }
        }
    }
}