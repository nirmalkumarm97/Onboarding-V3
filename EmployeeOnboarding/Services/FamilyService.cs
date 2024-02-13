using EmployeeOnboarding.Data;
using EmployeeOnboarding.Data.Enum;
using EmployeeOnboarding.Models;
using EmployeeOnboarding.ViewModels;
using Microsoft.EntityFrameworkCore;
using OnboardingWebsite.Models;

namespace EmployeeOnboarding.Services
{

    public class FamilyService
    {

        private readonly ApplicationDbContext _context;
        public FamilyService(ApplicationDbContext context)
        {
            _context = context;
        }

        int index1 = 1; // Initialize the Company_no sequence

        public async Task<List<EmployeeFamilyDetails>> AddFamily(int empId, List<FamilyVM> families)
        {
            try
            {
                List<EmployeeFamilyDetails> familyVMs = new List<EmployeeFamilyDetails>();
                foreach (var family in families)
                {
                    var existingFamily = _context.EmployeeFamilyDetails.FirstOrDefault(e => e.EmpGen_Id == empId && e.Family_no == index1);

                    if (existingFamily != null)
                    {
                        // Update existing record
                        existingFamily.Relationship = family.Relationship;
                        existingFamily.Name = family.Name;
                        DateOnly DOB = DateOnly.Parse(family.DOB);
                        existingFamily.DOB = DOB;
                        existingFamily.Occupation = family.Occupation;
                        existingFamily.contact = family.contact;
                        existingFamily.Date_Modified = DateTime.UtcNow;
                        existingFamily.Modified_by = empId.ToString();
                        existingFamily.Status = "A";

                    }
                    else
                    {
                        var _family = new EmployeeFamilyDetails()
                        {
                            EmpGen_Id = empId,
                            Family_no = index1,
                            Relationship = family.Relationship,
                            Name = family.Name,
                            DOB = DateOnly.Parse(family.DOB),
                            Occupation = family.Occupation,
                            contact = family.contact,
                            Date_Created = DateTime.UtcNow,
                            Date_Modified = DateTime.UtcNow,
                            Created_by = empId.ToString(),
                            Modified_by = empId.ToString(),
                            Status = "A"
                        };
                        familyVMs.Add(_family);
                    }
                    index1++;
                }
                _context.EmployeeFamilyDetails.AddRange(familyVMs);
                _context.SaveChanges();
                return familyVMs;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<GetFamilyVM> GetFamily(int empId)
        {
            var family = _context.EmployeeFamilyDetails.Where(e => e.EmpGen_Id == empId && e.Family_no != null).Select(e => new GetFamilyVM
            {
                Relationship = e.Relationship,
                Name = e.Name,
                DOB = e.DOB,
                Occupation = e.Occupation,
                contact = e.contact
            })
                .ToList();

            return family;
        }

    }
}
