using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeOnboarding.Models
{
    public class PersonalInfoVM
    {
        public int Id { get; set; }
       // public string Empid { get; set; }
        public string EmpName { get; set; }
        public string FatherName { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true,DataFormatString = "{0:dd/MM/yyyy}")]
        public DateOnly DOB { get; set; }
        public string mailId { get; set; }
        public string MaritialStatus { get; set; }
        public DateOnly? DOM { get; set; }
        public string Gender { get; set; }
        public string bloodgrp { get; set; }
        public double Contactno { get; set; }
        public string? ECP { get; set; }
        public string? ECR { get; set; }
        public double? ECN { get; set; }
        public AddressVM1 PermanentAddress { get; set; }
        public AddressVM1 TemporaryAddress { get; set; }

        public bool? Disability { get; set; }
        public string? Disablility_type { get; set; }

        public string CovidSts { get; set; }
        public byte[]? CovidCerti { get; set; }
        public List<EducationDetailsVM> educationDetailsVMs { get; set; }
        public List<ExperienceVM> experienceVMs { get; set; }
    }
        
}
