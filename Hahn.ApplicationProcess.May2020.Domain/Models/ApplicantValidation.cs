using FluentValidation;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Hahn.ApplicationProcess.May2020.Domain.Models
{
    public class ApplicantValidation : AbstractValidator<Applicant>
    {
        public ApplicantValidation()
        {
            RuleFor(x => x.Name).MinimumLength(5).WithMessage("Name can not be less than 5 characters.");
            RuleFor(x => x.FamilyName).MinimumLength(5).WithMessage("Family name can not be less than 5 characters.");
            RuleFor(x => x.Address).MinimumLength(10).WithMessage("Address can not be less than 10 characters.");
            RuleFor(x => x.CountryOfOrigin).MustAsync(CountryOfOrigin_Validate).WithMessage("Country name is invalid");
            RuleFor(x => x.EmailAddress).EmailAddress().WithMessage("Email address is invalid");//need to check
            RuleFor(x => x.Age).InclusiveBetween(20, 60).WithMessage("Age must be in between 20 and 60");
        }

        /// <summary>
        /// Validates if specified country is available in https://restcountries.eu/rest/v2/name/aruba?fullText=true
        /// API description => https://restcountries.eu/#api-endpoints-full-name 
        /// </summary>
        /// <param name="countryName"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        private async Task<bool> CountryOfOrigin_Validate(string countryName, CancellationToken ct)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(string.Format(@"https:////restcountries.eu//rest//v2//name//{0}?fullText=true", countryName));
            if (response.IsSuccessStatusCode)
                return true;
            else
                return false;
        }
    }
}
