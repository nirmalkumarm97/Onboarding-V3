using EmployeeOnboarding.Data;
using EmployeeOnboarding.Data.Enum;
using EmployeeOnboarding.Models;
using EmployeeOnboarding.ViewModels;
using Microsoft.EntityFrameworkCore;
using OnboardingWebsite.Models;

namespace EmployeeOnboarding.Services
{
    public class EmergencyContactService
    {

        private readonly ApplicationDbContext _context;
        public EmergencyContactService(ApplicationDbContext context)
        {
            _context = context;
        }

        int index1 = 1; // Initialize the Company_no sequence

        public async Task<List<EmployeeEmergencyContactDetails>> AddEmergencyContact(int empId, List<EmergencyContactVM> emergencies)
        {
            List<EmployeeEmergencyContactDetails> emergencyVMs = new List<EmployeeEmergencyContactDetails>();
            foreach (var emergency in emergencies)
            {
                var existingemergency = _context.EmployeeEmergencyContactDetails.FirstOrDefault(e => e.EmpGen_Id == empId && e.emergency_no == index1);

                if (existingemergency != null)
                {
                    existingemergency.Relationship = emergency.Relationship;
                    existingemergency.Relation_name = emergency.Relation_name;
                    existingemergency.Contact_number = emergency.Contact_number;
                    existingemergency.Contact_address = emergency.Contact_address;
                    existingemergency.Date_Modified = DateTime.UtcNow;
                    existingemergency.Modified_by = empId.ToString();
                    existingemergency.Status = "A";
                }
                else
                {
                    var _emergency = new EmployeeEmergencyContactDetails()
                    {
                        EmpGen_Id = empId,
                        emergency_no = index1,
                        Relationship = emergency.Relationship,
                        Relation_name = emergency.Relation_name,
                        Contact_number = emergency.Contact_number,
                        Contact_address = emergency.Contact_address,
                        Date_Created = DateTime.UtcNow,
                        Date_Modified = DateTime.UtcNow,
                        Created_by = empId.ToString(),
                        Modified_by = empId.ToString(),
                        Status = "A"
                    };
                    emergencyVMs.Add(_emergency);

                }

                index1++;

            }
            _context.EmployeeEmergencyContactDetails.AddRange(emergencyVMs);
            _context.SaveChanges();
            var id = emergencyVMs.Select(x => x.EmpGen_Id).FirstOrDefault();

            return emergencyVMs;
        }


        public List<EmergencyContactVM> GetEmergencyContact(int empId)
        {
            var emergencyContact = _context.EmployeeEmergencyContactDetails.Where(e => e.EmpGen_Id == empId && e.emergency_no != null).Select(e => new EmergencyContactVM
            {
                Relationship = e.Relationship,
                Relation_name = e.Relation_name,
                Contact_number = e.Contact_number,
                Contact_address = e.Contact_address,
            })
                .ToList();

            return emergencyContact;
        }

    }
}

