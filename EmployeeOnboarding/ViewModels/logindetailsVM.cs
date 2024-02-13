﻿using FluentMigrator.Infrastructure;
using System.ComponentModel.DataAnnotations;

namespace EmployeeOnboarding.ViewModels
{
    public class logindetailsVM
    {
        // public string Name { get; set; }
        public string Emailid { get; set; }

        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
