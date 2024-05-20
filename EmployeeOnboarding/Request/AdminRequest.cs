using EmployeeOnboarding.Constants;
using FluentMigrator.Infrastructure;
using System.ComponentModel.DataAnnotations;

namespace EmployeeOnboarding.Request
{
    public class AdminRequest
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string? SearchCriteria { get; set; }
        public bool OrderByNew { get; set; }
    }
}
