﻿namespace EmployeeOnboarding.ViewModels
{
    public class GetEducationVM
    {
        public int GenId { get; set; }
        public int Number { get; set; }
        public int Qualification { get; set; }
        public string University { get; set; }
        public string Institution_name { get; set; }
        public string Degree_achieved { get; set; }
        public string specialization { get; set; }
        public int Passoutyear { get; set; }
        public string Percentage { get; set; }
        public byte[] Edu_certificate { get; set; }
        public string Edu_certificateName { get; set; }
    }
}
