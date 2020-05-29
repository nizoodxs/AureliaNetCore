using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Hahn.ApplicationProcess.May2020.Domain.Models
{
    public class Applicant
    {
        /// <summary>
        /// Unique id that represent the applicant
        /// </summary>
        /// /// <example>1</example>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        /// <summary>
        /// First name of the applicant
        /// </summary>
        /// /// <example>Nischal</example>
        public string Name { get; set; }
        /// <summary>
        /// Family name of the applicant
        /// </summary>
        /// /// <example>Subedi</example>
        public string FamilyName { get; set; }
        /// <summary>
        /// Permanent address of the applicant
        /// </summary>
        /// /// <example>Butwal,Rupandehi</example>
        public string Address { get; set; }
        /// <summary>
        /// Birth country of the applicant
        /// </summary>
        /// /// <example>Nepal</example>
        public string CountryOfOrigin { get; set; }
        /// <summary>
        /// Email of the applicant
        /// </summary>
        /// /// <example>abc@mail.com</example>
        public string EmailAddress { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// /// <example>25</example>
        public int Age { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// /// <example>false</example>
        public bool Hired { get; set; }

        public Applicant()
        {
            Hired = false;
        }

    }
}
