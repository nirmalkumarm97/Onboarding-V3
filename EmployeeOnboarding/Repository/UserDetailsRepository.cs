using DocumentFormat.OpenXml.InkML;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Wordprocessing;
using EmployeeOnboarding.Contracts;
using EmployeeOnboarding.Data;
using EmployeeOnboarding.Data.Enum;
using EmployeeOnboarding.Request;
using EmployeeOnboarding.Response;
using EmployeeOnboarding.ViewModels;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using OnboardingWebsite.Models;
using System;
//using Org.BouncyCastle.Ocsp;
using System.Runtime.CompilerServices;
using System.Text.Encodings.Web;

namespace EmployeeOnboarding.Repository
{
    public class UserDetailsRepository : IUserDetailsRepository
    {
        private ApplicationDbContext _context;
        private IEmailSender _emailSender;

        public UserDetailsRepository(ApplicationDbContext context , IEmailSender emailSender)
        {
            _context = context;

        }

        private string SaveImageFile(string image, string Id, string fileName)
        {
            if (image == null)
            {
                return null;
            }
            var certificateBytes = Convert.FromBase64String(image);

            var empFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "C:\\A_Onboarding\\OnboardingFiles", Id);
            if (!Directory.Exists(empFolderPath))
            {
                Directory.CreateDirectory(empFolderPath);
            }
            var filePath = Path.Combine(empFolderPath, fileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                fileStream.WriteAsync(certificateBytes, 0 , certificateBytes.Length);
            }
            return filePath;
        }

        private string SaveCertificateFile(string certificateFile, string Id, string fileName)
        {
            if (certificateFile == null)
            {
                return null;
            }
            var certificateBytes = Convert.FromBase64String(certificateFile);

            var empFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "C:\\A_Onboarding\\OnboardingFiles", Id);
            if (!Directory.Exists(empFolderPath))
            {
                Directory.CreateDirectory(empFolderPath);
            }
            var filePath = Path.Combine(empFolderPath, fileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                fileStream.WriteAsync(certificateBytes, 0, certificateBytes.Length);
            }
            return filePath; // Return the file path
        }

        public async Task<int> AddPersonalInfo(bool directadd , PersonalInfoRequest personalInfoRequest)
        {
            using (IDbContextTransaction transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    if (personalInfoRequest != null)
                    {
                        if (directadd == true)
                        {
                            var checkemail = _context.Login.Where(x => x.EmailId == personalInfoRequest.generalVM.Personal_Emailid).FirstOrDefault();
                            if (checkemail != null)
                            {
                                checkemail.Modified_by = personalInfoRequest.loginId.ToString();
                                checkemail.Created_by = personalInfoRequest.loginId.ToString();
                                checkemail.Date_Created = DateTime.Now;
                                checkemail.Date_Modified = DateTime.Now;
                                checkemail.Status = "A";
                                checkemail.EmailId = personalInfoRequest.generalVM.Personal_Emailid;
                                checkemail.Name = personalInfoRequest.generalVM.Empname;
                            }
                            else
                            {
                                string tempPass = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 8); // Change length as needed

                                Login login = new Login()
                                {
                                    Modified_by = personalInfoRequest.loginId.ToString(),
                                    Created_by = personalInfoRequest.loginId.ToString(),
                                    Date_Created = DateTime.Now,
                                    Date_Modified = DateTime.Now,
                                    Status = "A",
                                    Password = tempPass,
                                    EmailId = personalInfoRequest.generalVM.Personal_Emailid,
                                    Name = personalInfoRequest.generalVM.Empname,
                                    Role = "U",
                                };
                                _context.Login.Add(login);
                                var callbackUrl = "https://onboarding-dev.ideassionlive.in/";
                                await _emailSender.SendEmailAsync(personalInfoRequest.generalVM.Personal_Emailid, "Confirm Your Email", $"Please enter into your login by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'> clicking here</a>. Your email is {personalInfoRequest.generalVM.Personal_Emailid} and password is {tempPass}");

                            }
                            _context.SaveChanges();

                        }
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

                                existingGeneral.Profile_pic = SaveImageFile(personalInfoRequest.generalVM.Profile_pic, personalInfoRequest.loginId.ToString(), "Profile.jpg");

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
                            if (personalInfoRequest.colleagues.Count > 0)
                            {
                                int index2 = 0;
                                List<EmployeeColleagueDetails> colleagueVMs = new List<EmployeeColleagueDetails>();
                                foreach (var colleague in personalInfoRequest.colleagues)
                                {
                                    var existingColleague = _context.EmployeeColleagueDetails.FirstOrDefault(e => e.EmpGen_Id == GenId && e.colleague_no == index2);

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
                                    index2++;
                                }
                                _context.EmployeeColleagueDetails.AddRange(colleagueVMs);
                            }

                            //EmergencyContactdetails
                            if (personalInfoRequest.emergencies.Count > 0)
                            {
                                int index3 = 0;
                                List<EmployeeEmergencyContactDetails> emergencyVMs = new List<EmployeeEmergencyContactDetails>();
                                foreach (var emergency in personalInfoRequest.emergencies)
                                {
                                    var existingemergency = _context.EmployeeEmergencyContactDetails.FirstOrDefault(e => e.EmpGen_Id == GenId && e.emergency_no == index3);

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
                                    index3++;

                                }

                                _context.EmployeeEmergencyContactDetails.AddRange(emergencyVMs);
                            }

                            //Required Details

                            var existingRequired = _context.EmployeeRequiredDocuments.FirstOrDefault(e => e.EmpGen_Id == GenId);

                            if (existingRequired != null)
                            {
                                existingRequired.Aadhar = SaveCertificateFile(personalInfoRequest.RequiredDocuments.Aadhar, GenId.ToString(), "Aadhar.pdf");
                                existingRequired.Pan = SaveCertificateFile(personalInfoRequest.RequiredDocuments.Pan, GenId.ToString(), "Pan.pdf");
                                existingRequired.Driving_license = SaveCertificateFile(personalInfoRequest.RequiredDocuments.Driving_license, GenId.ToString(), "Driving_license.pdf");
                                existingRequired.Passport = SaveCertificateFile(personalInfoRequest.RequiredDocuments.Passport, GenId.ToString(), "Passport.pdf");
                                existingRequired.Date_Modified = DateTime.UtcNow;
                                existingRequired.Modified_by = GenId.ToString();
                                existingRequired.Status = "A";
                            }
                            else
                            {
                                // Add new record
                                var _required = new EmployeeRequiredDocuments()
                                {
                                    EmpGen_Id = GenId,
                                    Aadhar = SaveCertificateFile(personalInfoRequest.RequiredDocuments.Aadhar, GenId.ToString(), "Aadhar.pdf"),
                                    Pan = SaveCertificateFile(personalInfoRequest.RequiredDocuments.Pan, GenId.ToString(), "Pan.pdf"),
                                    Driving_license = SaveCertificateFile(personalInfoRequest.RequiredDocuments.Driving_license, GenId.ToString(), "Driving_license.pdf"),
                                    Passport = SaveCertificateFile(personalInfoRequest.RequiredDocuments.Passport, GenId.ToString(), "Passport.pdf"),
                                    Date_Created = DateTime.UtcNow,
                                    Date_Modified = DateTime.UtcNow,
                                    Created_by = GenId.ToString(),
                                    Modified_by = GenId.ToString(),
                                    Status = "A"
                                };

                                _context.EmployeeRequiredDocuments.Add(_required);
                            }
                            _context.SaveChanges();
                            transaction.Commit();
                            return GenId;
                        
                    }
                    else
                    {
                        throw new NullReferenceException("Null Exception");
                    }
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    throw new Exception(e.Message);
                }
            }
        }

        private static byte[] GetFile(string filepath)
        {
            if (System.IO.File.Exists(filepath))
            {
                System.IO.FileStream fs = System.IO.File.OpenRead(filepath);
                byte[] file = new byte[fs.Length];
                int br = fs.Read(file, 0, file.Length);
                if (br != fs.Length)
                {
                    throw new IOException("Invalid path");
                }
                return file;
            }
            return null;
        }

        public async Task<PersonalInfoResponse> GetPersonalInfo(int? Id , string? email)
        {
            try
            {
                PersonalInfoResponse personalInfoResponse = new PersonalInfoResponse();

                    GeneralInfoResponse? _general = _context.EmployeeGeneralDetails.Where(n => n.Login_ID == Id || n.Personal_Emailid == email).Select(general => new GeneralInfoResponse()
                    {
                        Id = general.Id,
                        LoginId = general.Login_ID,
                        Empname = general.Empname,
                        Personal_Emailid = general.Personal_Emailid,
                        Contact_no = general.Contact_no,
                        DOB = general.DOB,
                        Nationality = general.Nationality,
                        Gender = general.Gender,//((Gender)general.Gender).ToString(),
                        MaritalStatus = general.MaritalStatus,//((MartialStatus)general.Gender).ToString(),
                        DateOfMarriage = general.DateOfMarriage,
                        BloodGrp = general.BloodGrp,//EnumExtensionMethods.GetEnumDescription((BloodGroup)general.BloodGrp),
                        Profile_Pic = GetFile(general.Profile_pic),

                    }).FirstOrDefault();
                    personalInfoResponse.GenId = _general.Id;
                    personalInfoResponse.loginId = _general.LoginId;
                    personalInfoResponse.generalVM = _general;

                    var _contact = _context.EmployeeContactDetails.Where(n => n.EmpGen_Id == _general.Id).Select(contact => new ContactResponse()
                    {
                        Address1 = contact.Address1,
                        Address2 = contact.Address2,
                        Country_Id = contact.Country_Id,
                        State_Id = contact.State_Id,
                        City_Id = contact.City_Id,
                        Pincode = contact.Pincode,
                        AddressType = contact.Address_Type,
                    }).ToList();
                    personalInfoResponse.contact = _contact;

                    var family = _context.EmployeeFamilyDetails.Where(e => e.EmpGen_Id == _general.Id && e.Family_no != null).Select(e => new FamilyResponse()
                    {
                        Relationship = e.Relationship,
                        Name = e.Name,
                        DOB = e.DOB,
                        Occupation = e.Occupation,
                        contact = e.contact
                    }).ToList();

                    personalInfoResponse.families = family;


                    var _hobby = _context.EmployeeHobbyMembership.Where(n => n.EmpGen_Id == _general.Id).Select(hobby => new HobbyResponse()
                    {
                        ProfessionalBody = hobby.ProfessionalBody,
                        ProfessionalBody_name = hobby.ProfessionalBody_name,
                        Hobbies = hobby.Hobbies,

                    }).FirstOrDefault();

                    personalInfoResponse.hobby = _hobby;

                    var colleague = _context.EmployeeColleagueDetails.Where(e => e.EmpGen_Id == _general.Id && e.colleague_no != null).Select(e => new ColleagueResponse
                    {
                        Empid = e.Employee_id,
                        Colleague_Name = e.Colleague_Name,
                        Location = e.Location
                    })
                      .ToList();

                    if (colleague.Count > 0)
                    {
                        personalInfoResponse.colleagues = colleague;
                    }


                    var emergencyContact = _context.EmployeeEmergencyContactDetails.Where(e => e.EmpGen_Id == _general.Id && e.emergency_no != null).Select(e => new EmergencyContactResponse
                    {
                        Relationship = e.Relationship,
                        Relation_name = e.Relation_name,
                        Contact_number = e.Contact_number,
                        Contact_address = e.Contact_address,
                    })
                       .ToList();

                    if (emergencyContact.Count > 0)
                    {
                        personalInfoResponse.emergencies = emergencyContact;
                    }

                    RequiredDocumentsRespose requiredDocuments = _context.EmployeeRequiredDocuments.Where(x => x.EmpGen_Id == _general.Id).Select(e => new RequiredDocumentsRespose
                    {
                        Aadhar = GetFile(e.Aadhar),
                        Driving_license = GetFile(e.Driving_license),
                        Pan = GetFile(e.Pan),
                        Passport = GetFile(e.Passport)
                    }).FirstOrDefault();

                    personalInfoResponse.RequiredDocuments = requiredDocuments;


                    return personalInfoResponse;
            }
           
            catch (Exception e)
            {
                throw;
            }
        }
        public async Task<string> GetStatusByLoginId(int loginId)
        {

            string data = await _context.Login.Where(x => x.Id == loginId).Select(x => x.Invited_Status).FirstOrDefaultAsync();
            return data;

        }

    }
}

