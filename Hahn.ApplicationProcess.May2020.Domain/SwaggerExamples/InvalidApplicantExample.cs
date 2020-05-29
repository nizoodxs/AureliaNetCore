using Newtonsoft.Json.Linq;
using Swashbuckle.AspNetCore.Filters;

namespace Hahn.ApplicationProcess.May2020.Domain.SwaggerExamples
{
    public class InvalidApplicantExample : IExamplesProvider<object>
    {
        public object GetExamples()
        {
            var errJObj = new JObject();
            errJObj.Add("Name","Name can not be less than 5 characters.");
            errJObj.Add("FamilyName","Family name can not be less than 5 characters.");
            errJObj.Add("Address","Address can not be less than 10 characters.");
            errJObj.Add("CountryOfOrigin","Country name is invalid");
            errJObj.Add("EmailAddress","Email address is invalid");
            errJObj.Add("Age","Age must be in between 20 and 60");
            return errJObj;
        }
    }
}
