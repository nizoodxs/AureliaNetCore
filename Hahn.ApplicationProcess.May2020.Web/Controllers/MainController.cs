using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using FluentValidation.Results;
using Hahn.ApplicationProcess.May2020.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hahn.ApplicationProcess.May2020.Web.Controllers
{
    [Route("api")]
    [ApiController]
    public class MainController : ControllerBase
    {
        [HttpGet]
        public HttpResponseMessage GetApplicant(int applicantId)
        {
            return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
        }

        [HttpPost]
        public HttpResponseMessage CreateApplicant(Applicant applicant)
        {
            ApplicantValidation validationRules = new ApplicantValidation();
            ValidationResult validationResult = validationRules.Validate(applicant);

            return new HttpResponseMessage(System.Net.HttpStatusCode.Created);
        }

        [HttpPut]
        public HttpResponseMessage UpdateApplicant(Applicant applicant)
        {

        }

        [HttpDelete]
        public HttpResponseMessage DeleteApplicant(int applicantId)
        {

        }
    }
}
