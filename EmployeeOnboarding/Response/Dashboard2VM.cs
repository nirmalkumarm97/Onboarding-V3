﻿using EmployeeOnboarding.Data;

namespace EmployeeOnboarding.Response
{
    public class Dashboard2VM
    {
        public int Login_Id { get; set; }
        public int EmpGen_Id { get; set; }
        public string Empid { get; set; }
        public string Empname { get; set; }
        // public string designation { get; set; }
        public double Contact_no { get; set; }
        public string Email { get; set; }
        public string education { get; set; }
        public string Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? DateModified { get; set; }

    }

    public class AdminDashBoardEmployeesData
    {
        public int OverallCount { get; set; }
        public List<Dashboard2VM> result { get; set; }
    }
}