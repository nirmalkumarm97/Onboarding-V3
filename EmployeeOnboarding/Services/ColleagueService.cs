using EmployeeOnboarding.Data;
using EmployeeOnboarding.Data.Enum;
using EmployeeOnboarding.Models;
using EmployeeOnboarding.ViewModels;
using Microsoft.EntityFrameworkCore;
using OnboardingWebsite.Models;

namespace EmployeeOnboarding.Services
{

    public class ColleagueService
    {

        private readonly ApplicationDbContext _context;
        public ColleagueService(ApplicationDbContext context)
        {
            _context = context;
        }

        int index1 = 1; // Initialize the Company_no sequence

        public async Task<List<EmployeeColleagueDetails>> AddColleague(int empId, List<ColleagueVM> colleagues)
        {
            try
            {
                List<EmployeeColleagueDetails> colleagueVMs = new List<EmployeeColleagueDetails>();
                foreach (var colleague in colleagues)
                {
                    var existingColleague = _context.EmployeeColleagueDetails.FirstOrDefault(e => e.EmpGen_Id == empId && e.colleague_no == index1);

                    if (existingColleague != null)
                    {
                        existingColleague.Employee_id = colleague.Empid;
                        existingColleague.Colleague_Name = colleague.Colleague_Name;
                        existingColleague.Location = colleague.Location;
                        existingColleague.Date_Modified = DateTime.UtcNow;
                        existingColleague.Modified_by = empId.ToString();
                        existingColleague.Status = "A";
                    }
                    else
                    {
                        var _colleague = new EmployeeColleagueDetails()
                        {
                            EmpGen_Id = empId,
                            colleague_no = index1,
                            Employee_id = colleague.Empid,
                            Colleague_Name = colleague.Colleague_Name,
                            Location = colleague.Location,
                            Date_Created = DateTime.UtcNow,
                            Date_Modified = DateTime.UtcNow,
                            Created_by = empId.ToString(),
                            Modified_by = empId.ToString(),
                            Status = "A"
                        };
                        colleagueVMs.Add(_colleague);
                    }
                    index1++;
                }
                _context.EmployeeColleagueDetails.AddRange(colleagueVMs);
                _context.SaveChanges();
                return colleagueVMs;
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public List<ColleagueVM> GetColleague(int empId)
        {
            var colleague = _context.EmployeeColleagueDetails.Where(e => e.EmpGen_Id == empId && e.colleague_no != null).Select(e => new ColleagueVM
            {
                Empid = e.Employee_id,
                Colleague_Name = e.Colleague_Name,
                Location = e.Location
            })
                .ToList();

            return colleague;
        }

    }
}
