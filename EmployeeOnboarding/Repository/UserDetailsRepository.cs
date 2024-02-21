using DocumentFormat.OpenXml.Office2010.Excel;
using EmployeeOnboarding.Contracts;
using EmployeeOnboarding.Data;
using EmployeeOnboarding.Request;
using EmployeeOnboarding.ViewModels;
using OnboardingWebsite.Models;
//using Org.BouncyCastle.Ocsp;
using System.Runtime.CompilerServices;

namespace EmployeeOnboarding.Repository
{
    public class UserDetailsRepository : IUserDetailsRepository
    {
        private ApplicationDbContext _context;

        public UserDetailsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        private string SaveImageFile(IFormFile image, string Id, string fileName)
        {
            if (image == null)
            {
                return null;
            }
            var empFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "Documents", Id);
            if (!Directory.Exists(empFolderPath))
            {
                Directory.CreateDirectory(empFolderPath);
            }
            var filePath = Path.Combine(empFolderPath, fileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                image.CopyTo(fileStream);
            }
            return filePath;
        }

        private string SaveCertificateFile(IFormFile certificateFile, string Id, string fileName)
        {
            if (certificateFile == null)
            {
                return null;
            }
            var empFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "Documents", Id);
            if (!Directory.Exists(empFolderPath))
            {
                Directory.CreateDirectory(empFolderPath);
            }
            var filePath = Path.Combine(empFolderPath, fileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                certificateFile.CopyTo(fileStream);
            }
            return filePath; // Return the file path
        }

        public async Task<bool> AddPersonalInfo(PersonalInfoRequest personalInfoRequest)
        {

            //General details:

            var existingGeneral = _context.EmployeeGeneralDetails.FirstOrDefault(e => e.Login_ID == personalInfoRequest.loginId);
            int newGenId = 0;
            if (existingGeneral != null)
            {
                existingGeneral.Empname = personalInfoRequest.generalVM.Empname;
                existingGeneral.Personal_Emailid = personalInfoRequest.generalVM.Personal_Emailid;
                existingGeneral.Contact_no = personalInfoRequest.generalVM.Contact_no;

                DateOnly DOB = DateOnly.Parse(personalInfoRequest.generalVM.DOB);
                existingGeneral.DOB = DOB;

                existingGeneral.Nationality = personalInfoRequest.generalVM.Nationality;
                existingGeneral.Gender = personalInfoRequest.generalVM.Gender;
                existingGeneral.MaritalStatus = (int)personalInfoRequest.generalVM.MaritalStatus;

                DateOnly DateOfMarriage = DateOnly.Parse(personalInfoRequest.generalVM.DateOfMarriage);
                existingGeneral.DateOfMarriage = DateOfMarriage;

                existingGeneral.BloodGrp = personalInfoRequest.generalVM.BloodGrp;

                existingGeneral.Profile_pic = SaveImageFile(personalInfoRequest.generalVM.Profile_pic, personalInfoRequest.loginId.ToString(), "Profile.jpeg");

                existingGeneral.Date_Modified = DateTime.UtcNow;
                existingGeneral.Modified_by = personalInfoRequest.loginId.ToString();
                existingGeneral.Status = "A";
            }
            else
            {
                // Add new record
                var _general = new EmployeeGeneralDetails()
                {
                    Login_ID = personalInfoRequest.loginId,
                    Empname = personalInfoRequest.generalVM.Empname,
                    Personal_Emailid = personalInfoRequest.generalVM.Personal_Emailid,
                    Contact_no = personalInfoRequest.generalVM.Contact_no,
                    DOB = DateOnly.Parse(personalInfoRequest.generalVM.DOB),
                    Nationality = personalInfoRequest.generalVM.Nationality,
                    Gender = personalInfoRequest.generalVM.Gender,
                    MaritalStatus = (int)personalInfoRequest.generalVM.MaritalStatus,
                    DateOfMarriage = DateOnly.Parse(personalInfoRequest.generalVM.DateOfMarriage),
                    BloodGrp = personalInfoRequest.generalVM.BloodGrp,
                    Profile_pic = SaveImageFile(personalInfoRequest.generalVM.Profile_pic, personalInfoRequest.loginId.ToString(), "Profile.jpg"),
                    Date_Created = DateTime.UtcNow,
                    Date_Modified = DateTime.UtcNow,
                    Created_by = personalInfoRequest.loginId.ToString(),
                    Modified_by = personalInfoRequest.loginId.ToString(),
                    Status = "A"
                };
                _context.EmployeeGeneralDetails.Add(_general);
                _context.SaveChanges();
                newGenId = _general.Id;

            }

            var submit = _context.Login.FirstOrDefault(e => e.Id == personalInfoRequest.loginId);

            submit.Invited_Status = "Submitted";

            _context.Login.Update(submit);

            _context.SaveChanges();

            int GenId = existingGeneral != null ? existingGeneral.Id : newGenId;




            //Present And Permanent Address:
            foreach (var type in personalInfoRequest.contact)
            {
                var existingContact = _context.EmployeeContactDetails.FirstOrDefault(e => e.EmpGen_Id == GenId && e.Address_Type == type.AddressType);

                if (existingContact != null)
                {
                    //Update existing record
                    existingContact.Address1 = type.Address1;
                    existingContact.Address2 = type.Address2;
                    existingContact.Country_Id = type.Country_Id;
                    existingContact.State_Id = type.State_Id;
                    existingContact.City_Id = type.City_Id;
                    existingContact.Pincode = type.Pincode;
                    existingContact.Date_Modified = DateTime.UtcNow;
                    existingContact.Modified_by = GenId.ToString();
                    existingContact.Status = "A";
                }
                else
                {
                    //Add new record
                    var _contact = new EmployeeContactDetails()
                    {
                        EmpGen_Id = GenId,
                        Address_Type = type.AddressType,
                        Address1 = type.Address1,
                        Address2 = type.Address2,
                        Country_Id = type.Country_Id,
                        State_Id = type.State_Id,
                        City_Id = type.City_Id,
                        Pincode = type.Pincode,
                        Date_Created = DateTime.UtcNow,
                        Date_Modified = DateTime.UtcNow,
                        Created_by = GenId.ToString(),
                        Modified_by = GenId.ToString(),
                        Status = "A"
                    };
                    _context.EmployeeContactDetails.Add(_contact);
                }
            }


            //Family Details
            if (personalInfoRequest.families.Count > 0 && personalInfoRequest.families != null )
            {

                List<EmployeeFamilyDetails> familyVMs = new List<EmployeeFamilyDetails>();
                int index1 = 1; // Initialize the Company_no sequence

                foreach (var family in personalInfoRequest.families)
                {
                    var existingFamily = _context.EmployeeFamilyDetails.FirstOrDefault(e => e.EmpGen_Id == GenId && e.Family_no == index1);

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
                        existingFamily.Modified_by = GenId.ToString();
                        existingFamily.Status = "A";

                    }
                    else
                    {
                        var _family = new EmployeeFamilyDetails()
                        {
                            EmpGen_Id = GenId,
                            Family_no = index1,
                            Relationship = family.Relationship,
                            Name = family.Name,
                            DOB = DateOnly.Parse(family.DOB),
                            Occupation = family.Occupation,
                            contact = family.contact,
                            Date_Created = DateTime.UtcNow,
                            Date_Modified = DateTime.UtcNow,
                            Created_by = GenId.ToString(),
                            Modified_by = GenId.ToString(),
                            Status = "A"
                        };
                        familyVMs.Add(_family);
                    }
                    index1++;
                }
                _context.EmployeeFamilyDetails.AddRange(familyVMs);
            }


            // hobbies and membership

            var existingHobby = _context.EmployeeHobbyMembership.FirstOrDefault(e => e.EmpGen_Id == GenId);
            if (existingHobby != null)
            {
                existingHobby.ProfessionalBody = personalInfoRequest.hobby.ProfessionalBody;
                existingHobby.ProfessionalBody_name = personalInfoRequest.hobby.ProfessionalBody_name;
                existingHobby.Hobbies = personalInfoRequest.hobby.Hobbies;
                existingHobby.Date_Modified = DateTime.UtcNow;
                existingHobby.Modified_by = GenId.ToString();
                existingHobby.Status = "A";
            }
            else
            {
                // Add new record
                var _hobby = new EmployeeHobbyMembership()
                {
                    EmpGen_Id = GenId,
                    ProfessionalBody = personalInfoRequest.hobby.ProfessionalBody,
                    ProfessionalBody_name = personalInfoRequest.hobby.ProfessionalBody_name,
                    Hobbies = personalInfoRequest.hobby.Hobbies,
                    Date_Created = DateTime.UtcNow,
                    Date_Modified = DateTime.UtcNow,
                    Created_by = GenId.ToString(),
                    Modified_by = GenId.ToString(),
                    Status = "A"
                };

                _context.EmployeeHobbyMembership.Add(_hobby);
            }

            //colleaguesInfo

            if (personalInfoRequest.colleagues.Count > 0 && personalInfoRequest.families != null)
            {
                int index1 = 0;
                List<EmployeeColleagueDetails> colleagueVMs = new List<EmployeeColleagueDetails>();
                foreach (var colleague in personalInfoRequest.colleagues)
                {
                    var existingColleague = _context.EmployeeColleagueDetails.FirstOrDefault(e => e.EmpGen_Id == GenId && e.colleague_no == index1);

                    if (existingColleague != null)
                    {
                        existingColleague.Employee_id = colleague.Empid;
                        existingColleague.Colleague_Name = colleague.Colleague_Name;
                        existingColleague.Location = colleague.Location;
                        existingColleague.Date_Modified = DateTime.UtcNow;
                        existingColleague.Modified_by = GenId.ToString();
                        existingColleague.Status = "A";
                    }
                    else
                    {
                        var _colleague = new EmployeeColleagueDetails()
                        {
                            EmpGen_Id = GenId,
                            colleague_no = index1,
                            Employee_id = colleague.Empid,
                            Colleague_Name = colleague.Colleague_Name,
                            Location = colleague.Location,
                            Date_Created = DateTime.UtcNow,
                            Date_Modified = DateTime.UtcNow,
                            Created_by = GenId.ToString(),
                            Modified_by = GenId.ToString(),
                            Status = "A"
                        };
                        colleagueVMs.Add(_colleague);
                    }
                    index1++;
                }
                _context.EmployeeColleagueDetails.AddRange(colleagueVMs);
            }

            //EmergencyContactdetails
            if (personalInfoRequest.emergencies.Count > 0 && personalInfoRequest.families != null )
            {
                int index1 = 0;
                List<EmployeeEmergencyContactDetails> emergencyVMs = new List<EmployeeEmergencyContactDetails>();
                foreach (var emergency in personalInfoRequest.emergencies)
                {
                    var existingemergency = _context.EmployeeEmergencyContactDetails.FirstOrDefault(e => e.EmpGen_Id == GenId && e.emergency_no == index1);

                    if (existingemergency != null)
                    {
                        existingemergency.Relationship = emergency.Relationship;
                        existingemergency.Relation_name = emergency.Relation_name;
                        existingemergency.Contact_number = (long)emergency.Contact_number;
                        existingemergency.Contact_address = emergency.Contact_address;
                        existingemergency.Date_Modified = DateTime.UtcNow;
                        existingemergency.Modified_by = GenId.ToString();
                        existingemergency.Status = "A";
                    }
                    else
                    {
                        var _emergency = new EmployeeEmergencyContactDetails()
                        {
                            EmpGen_Id = GenId,
                            emergency_no = index1,
                            Relationship = emergency.Relationship,
                            Relation_name = emergency.Relation_name,
                            Contact_number = (long)emergency.Contact_number,
                            Contact_address = emergency.Contact_address,
                            Date_Created = DateTime.UtcNow,
                            Date_Modified = DateTime.UtcNow,
                            Created_by = GenId.ToString(),
                            Modified_by = GenId.ToString(),
                            Status = "A"
                        };
                        emergencyVMs.Add(_emergency);

                    }
                    index1++;

                }

                _context.EmployeeEmergencyContactDetails.AddRange(emergencyVMs);
            }

            //Required Details

            var existingRequired = _context.EmployeeRequiredDocuments.FirstOrDefault(e => e.EmpGen_Id == GenId);

            if (existingRequired != null)
            {
                existingRequired.Aadhar = SaveCertificateFile(personalInfoRequest.required.Aadhar, GenId.ToString(), "Aadhar.pdf");
                existingRequired.Pan = SaveCertificateFile(personalInfoRequest.required.Pan, GenId.ToString(), "Pan.pdf");
                existingRequired.Driving_license = SaveCertificateFile(personalInfoRequest.required.Driving_license, GenId.ToString(), "Driving_license.pdf");
                existingRequired.Passport = SaveCertificateFile(personalInfoRequest.required.Passport, GenId.ToString(), "Passport.pdf");
                existingRequired.Date_Modified = DateTime.UtcNow;
                existingRequired.Modified_by = GenId.ToString();
                existingRequired.Status = "A";
            }
            else
            {
                // Add new record
                var _required = new EmployeeRequiredDocuments()
                {
                    Aadhar = SaveCertificateFile(personalInfoRequest.required.Aadhar, GenId.ToString(), "Aadhar.pdf"),
                    Pan = SaveCertificateFile(personalInfoRequest.required.Pan, GenId.ToString(), "Pan.pdf"),
                    Driving_license = SaveCertificateFile(personalInfoRequest.required.Driving_license, GenId.ToString(), "Driving_license.pdf"),
                    Passport = SaveCertificateFile(personalInfoRequest.required.Passport, GenId.ToString(), "Passport.pdf"),
                    Date_Created = DateTime.UtcNow,
                    Date_Modified = DateTime.UtcNow,
                    Created_by = GenId.ToString(),
                    Modified_by = GenId.ToString(),
                    Status = "A"
                };

                _context.EmployeeRequiredDocuments.Add(_required);
            }
            _context.SaveChanges();
            return true;
        }
    
    }
}
