﻿namespace EmployeeOnboarding.Request
{
    public class RequiredInfo
    {
        public IFormFile Aadhar { get; set; }
        public IFormFile Pan { get; set; }
        public IFormFile? Driving_license { get; set; }
        public IFormFile? Passport { get; set; }

    }
}
