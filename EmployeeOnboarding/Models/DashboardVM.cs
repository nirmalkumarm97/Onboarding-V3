﻿using EmployeeOnboarding.Data;

namespace EmployeeOnboarding.Models
{
    public class DashboardVM
    {
        public int EmpGen_Id { get; set; }
        public string Empid { get; set; }
        public string Empname { get; set; }
       // public string designation { get; set; }
        public double Contact_no { get; set; }
        public string Email { get; set; }
        public string education { get; set; }
        public int? UserId { get; set; }
        public string Status { get; set; }

    }
}
