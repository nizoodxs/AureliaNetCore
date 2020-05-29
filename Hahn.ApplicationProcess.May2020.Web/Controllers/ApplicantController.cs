using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation.Results;
using Hahn.ApplicationProcess.May2020.Data;
using Hahn.ApplicationProcess.May2020.Domain.Models;
using Hahn.ApplicationProcess.May2020.Domain.SwaggerExamples;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace Hahn.ApplicationProcess.May2020.Web.Controllers
{
    [Route("api")]
    public class ApplicantController : ControllerBase
    {
        private readonly AppDbContext context;

        public ApplicantController(AppDbContext _ctx)
        {
            context = _ctx;
        }

        [Route("{id}")]
        [HttpGet]
        [SwaggerResponseExample(200, typeof(ApplicantExample))]
        public async Task<ActionResult<Applicant>> Get(int id)
        {
            Applicant applicant = context.Applicants.FirstOrDefault(x => x.ID == id);
            if (applicant != null)
            {
                return applicant;
            }
            else
            {
                return NotFound();
            }

        }

        [Route("create")]
        [HttpPost]
        [SwaggerRequestExample(typeof(Applicant), typeof(ApplicantExample))]
        [SwaggerResponse(201,"Applicant created successfully",typeof(ApplicantExample))]
        [SwaggerResponseExample(201, typeof(ApplicantExample))]
        [SwaggerResponse(400, "Invalid values for applicant", typeof(InvalidApplicantExample))]
        [SwaggerResponseExample(400, typeof(InvalidApplicantExample))]
        public async Task<ActionResult<Applicant>> Create([FromBody] Applicant applicant)
        {
            ApplicantValidation validationRules = new ApplicantValidation();
            ValidationResult validationResult = validationRules.Validate(applicant);
            if (!validationResult.IsValid)
            {
                Dictionary<string, string> errResult = new Dictionary<string, string>();
                foreach (ValidationFailure failure in validationResult.Errors)
                {
                    errResult.Add(failure.PropertyName, failure.ErrorMessage);
                }

                var result = new JsonResult(errResult);
                result.StatusCode = 400;
                return result;
            }
            else
            {
                context.Applicants.Add(applicant);
                await context.SaveChangesAsync();
                return CreatedAtAction(nameof(Get), new { id = applicant.ID }, applicant);
            }
        }

        [Route("update/{applicantId}")]
        [HttpPut]
        [SwaggerRequestExample(typeof(Applicant), typeof(ApplicantExample))]
        [SwaggerResponse(204, "Applicant updated successfully")]
        [SwaggerResponse(400, "Invalid values for applicant", typeof(InvalidApplicantExample))]
        [SwaggerResponseExample(400, typeof(InvalidApplicantExample))]
        public async Task<ActionResult<Applicant>> Update(int applicantId, [FromBody] Applicant newData)
        {
            var applicant = context.Applicants.FirstOrDefault(x => x.ID == applicantId);
            JObject errObj = new JObject();

            if (applicant == null)
            {
                errObj.Add("Error", "Applicant does not exists");
                var result = new JsonResult(errObj);
                result.StatusCode = 400;
                return result;
            }

            ApplicantValidation validationRules = new ApplicantValidation();
            ValidationResult validationResult = validationRules.Validate(newData);
            if (!validationResult.IsValid)
            {
                foreach (ValidationFailure failure in validationResult.Errors)
                {
                    errObj.Add(failure.PropertyName, failure.ErrorMessage);
                }

                var result = new JsonResult(errObj);
                result.StatusCode = 400;
                return result;
            }

            applicant.Name = newData.Name;
            applicant.FamilyName = newData.FamilyName;
            applicant.Address = newData.Address;
            applicant.CountryOfOrigin = newData.CountryOfOrigin;
            applicant.EmailAddress = newData.EmailAddress;
            applicant.Age = newData.Age;
            applicant.Hired = newData.Hired;

            context.Entry(applicant).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await context.SaveChangesAsync();

            return NoContent();
        }

        [Route("delete/{applicantId}")]
        [HttpDelete]
        [SwaggerResponse(200, "Delete successful")]
        [SwaggerResponseExample(200, typeof(ApplicantExample))]
        [SwaggerResponse(404, "Applicant not found")]
        public async Task<ActionResult<Applicant>> Delete(int applicantId)
        {
            var applicant = context.Applicants.First(x => x.ID == applicantId);
            if (applicant == null)
            {
                return NotFound();
            }
            context.Applicants.Remove(applicant);
            await context.SaveChangesAsync();

            return applicant;
        }
    }
}
