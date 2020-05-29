using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using FluentValidation.Results;
using Hahn.ApplicationProcess.May2020.Data;
using Hahn.ApplicationProcess.May2020.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<ActionResult<Applicant>> Create([FromBody] Applicant applicant)
        {
            ApplicantValidation validationRules = new ApplicantValidation();
            ValidationResult validationResult = validationRules.Validate(applicant);
            if (!validationResult.IsValid)
            {
                Dictionary<string, string> errResult = new Dictionary<string, string>();
                foreach (ValidationFailure result in validationResult.Errors)
                {
                    errResult.Add(result.PropertyName, result.ErrorMessage);
                }
                return new JsonResult(errResult);
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
        public async Task<ActionResult<Applicant>> Update(int applicantId, [FromBody] Applicant newData)
        {
            var applicant = context.Applicants.FirstOrDefault(x => x.ID == applicantId);
            
            if (applicant == null)
                return BadRequest();

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
