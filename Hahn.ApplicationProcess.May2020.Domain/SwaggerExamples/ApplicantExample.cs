using Hahn.ApplicationProcess.May2020.Domain.Models;
using Swashbuckle.AspNetCore.Filters;

namespace Hahn.ApplicationProcess.May2020.Domain.SwaggerExamples
{
    public class ApplicantExample : IExamplesProvider<Applicant>
    {
        public Applicant GetExamples()
        {
            return new Applicant()
            {
                Name="Nischal",
                FamilyName="Subedi",
                Address="Butwal,Rupandehi",
                CountryOfOrigin="Nepal",
                EmailAddress="abc@gmail.com",
                Age=25,
                Hired=false
            };
        }
    }
}