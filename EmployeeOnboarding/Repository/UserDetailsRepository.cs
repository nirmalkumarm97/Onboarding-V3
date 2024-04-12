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
using System.Data.Entity;
using System.Collections.Generic;
using NuGet.Protocol.Plugins;
using EmployeeOnboarding.Data.Models;

namespace EmployeeOnboarding.Repository
{
    public class UserDetailsRepository : IUserDetailsRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IEmailSender _emailSender;
        private readonly IConfiguration _configuration;

        public UserDetailsRepository(ApplicationDbContext context, IEmailSender emailSender, IConfiguration configuration)
        {
            _context = context;
            _emailSender = emailSender;
            _configuration = configuration;
        }

        private async Task WriteToFileAsync(FileStream fileStream, byte[] data, CancellationToken cancellationToken)
        {
            await fileStream.WriteAsync(data, 0, data.Length, cancellationToken);
        }

        private async Task<string> SaveImageFile(string image, string Id, string fileName)
        {
            try
            {
                if (image == null)
                {
                    return null;
                }
                var certificateBytes = Convert.FromBase64String(image);

                var empFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "C:\\A_Onboarding\\OnboardingFiles", Id);
                var filePath = Path.Combine(empFolderPath, fileName);

                if (!Directory.Exists(empFolderPath))
                {
                    Directory.CreateDirectory(empFolderPath);
                }
                else
                {
                    File.Delete(filePath);
                }
                int writeTimeoutMilliseconds = 5000;
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    var cancellationTokenSource = new CancellationTokenSource();
                    cancellationTokenSource.CancelAfter(writeTimeoutMilliseconds);

                    // Write to the file, with a timeout
                    var writeTask = WriteToFileAsync(fileStream, certificateBytes, cancellationTokenSource.Token);

                    // Wait for either the writeTask to complete or the timeout
                    await Task.WhenAny(writeTask, Task.Delay(-1, cancellationTokenSource.Token));
                    // fileStream.WriteAsync(certificateBytes, 0, certificateBytes.Length);
                    if (writeTask.IsCompleted)
                    {
                        await writeTask; // Ensure any exceptions are thrown
                    }
                    else
                    {
                        throw new TimeoutException("Write operation timed out.");
                    }
                }
                return filePath;
            }
            catch (OperationCanceledException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private async Task<string> SaveCertificateFile(string certificateFile, string Id, string fileName)
        {
            try
            {
                if (certificateFile == null)
                {
                    return null;
                }
                var certificateBytes = Convert.FromBase64String(certificateFile);

                var empFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "C:\\A_Onboarding\\OnboardingFiles", Id);
                var filePath = Path.Combine(empFolderPath, fileName);

                if (!Directory.Exists(empFolderPath))
                {
                    Directory.CreateDirectory(empFolderPath);
                }
                else
                {
                    File.Delete(filePath);
                }

                int writeTimeoutMilliseconds = 5000;
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    var cancellationTokenSource = new CancellationTokenSource();
                    cancellationTokenSource.CancelAfter(writeTimeoutMilliseconds);

                    // Write to the file, with a timeout
                    var writeTask = WriteToFileAsync(fileStream, certificateBytes, cancellationTokenSource.Token);

                    // Wait for either the writeTask to complete or the timeout
                    await Task.WhenAny(writeTask, Task.Delay(-1, cancellationTokenSource.Token));
                    // fileStream.WriteAsync(certificateBytes, 0, certificateBytes.Length);
                    if (writeTask.IsCompleted)
                    {
                        await writeTask; // Ensure any exceptions are thrown
                    }
                    else
                    {
                        throw new TimeoutException("Write operation timed out.");
                    }
                }
                return filePath; // Return the file path
            }
            catch (OperationCanceledException ex)
            {
                throw new Exception(ex.Message);
            }
        }
        private async Task SendConfirmationEmail(string email, string name, string url, string tempPass)
        {
            string subject = "Confirm Your Email";
            string body = $@"
<!DOCTYPE html>
<html>
<head>
    <style>
        body {{
            font-family: Arial, sans-serif;
            line-height: 1.6;
        }}
        .container {{
            max-width: 600px;
            margin: 0 auto;
            padding: 10px;
        }}
        h1 {{
            color: #333;
        }}
        p {{
            margin-bottom: 20px;
        }}
        a {{
            color: #007bff;
            text-decoration: none;
        }}
        a:hover {{
            text-decoration: underline;
        }}
    </style>
</head>
<body>
    <div class='container'>
        <p>Dear {name},</p>
        <p>Please login into your profile by <a href='{HtmlEncoder.Default.Encode(url)}'>clicking here</a>.</p>
        <p>Your email is {email} and password is {tempPass}.</p>
        <p>Regards,<br />HR Department<br />Ideassion Technology Solutions LLP</p>
    </div>
</body>
</html>";

            try
            {
                await _emailSender.SendEmailAsync(email, subject, body);
            }
            catch (Exception ex)
            {
                // Handle email sending exceptions
                // You might want to log the exception
                throw new Exception("Error sending confirmation email: " + ex.Message);
            }
        }
        public async Task<int> AddPersonalInfo(bool directadd, PersonalInfoRequest personalInfoRequest)
        {
            using (IDbContextTransaction transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    if (personalInfoRequest != null)
                    {
                        int UserId = 0;
                        if (directadd == true)
                        {
                            var checkemail = _context.Login.Where(x => x.EmailId == personalInfoRequest.generalVM.Personal_Emailid).FirstOrDefault();
                            if (checkemail == null)
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
                                _context.SaveChanges();
                                UserId = login.Id;
                                string url = _configuration.GetSection("ApplicationURL").Value;
                                await SendConfirmationEmail(login.EmailId, login.Name, url, tempPass);
                            }
                        }

                        //General details:

                        int newGenId = 0;
                        var existingGeneral = personalInfoRequest.GenId == null ? null :
                            _context.EmployeeGeneralDetails.FirstOrDefault(e => e.Id == personalInfoRequest.GenId);

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

                            DateOnly? DateOfMarriage = personalInfoRequest.generalVM.DateOfMarriage == String.Empty ? null : personalInfoRequest.generalVM.DateOfMarriage == null ? null : DateOnly.Parse(personalInfoRequest.generalVM.DateOfMarriage);
                            existingGeneral.DateOfMarriage = DateOfMarriage;

                            existingGeneral.BloodGrp = personalInfoRequest.generalVM.BloodGrp;

                            existingGeneral.Profile_pic = await SaveImageFile(personalInfoRequest.generalVM.Profile_pic, personalInfoRequest.generalVM.Empname + personalInfoRequest.loginId.ToString(), "Profile.jpg");

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
                                DateOfMarriage = personalInfoRequest.generalVM.DateOfMarriage == String.Empty ? null : personalInfoRequest.generalVM.DateOfMarriage == null ? null : DateOnly.Parse(personalInfoRequest.generalVM.DateOfMarriage),
                                BloodGrp = personalInfoRequest.generalVM.BloodGrp,
                                Profile_pic = await SaveImageFile(personalInfoRequest.generalVM.Profile_pic, personalInfoRequest.generalVM.Empname + personalInfoRequest.loginId.ToString(), "Profile.jpg"),
                                Date_Created = DateTime.UtcNow,
                                Date_Modified = DateTime.UtcNow,
                                Created_by = personalInfoRequest.loginId.ToString(),
                                Modified_by = personalInfoRequest.loginId.ToString(),
                                Status = "A",
                                UserId = directadd == true ? UserId : personalInfoRequest.loginId
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



                        // Present And Permanent Address:
                        var existingContacts = _context.EmployeeContactDetails.Where(e => e.EmpGen_Id == GenId).ToList();
                        foreach (var type in personalInfoRequest.contact)
                        {
                            var existingContact = existingContacts.FirstOrDefault(e => e.Address_Type == type.AddressType);

                            switch (type.AddressType)
                            {
                                case "Present":
                                case "Permanent":
                                    if (existingContact != null)
                                    {
                                        // Update existing record
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
                                        // Add new record
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
                                    break;
                            }
                        }

                        //Family Details

                        List<EmployeeFamilyDetails> familyVMs = new List<EmployeeFamilyDetails>();
                        int index1 = 1; // Initialize the Company_no sequence
                        var existingFamilies = _context.EmployeeFamilyDetails.Where(x => x.EmpGen_Id == GenId);

                        foreach (var family in personalInfoRequest.families)
                        {
                            var existingFamily = existingFamilies.FirstOrDefault(e => e.EmpGen_Id == GenId && e.Family_no == index1);
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
                            List<EmployeeColleagueDetails> colleagueVMs = new List<EmployeeColleagueDetails>();
                            int index2 = 1;
                            var existingColleagues = _context.EmployeeColleagueDetails.Where(x => x.EmpGen_Id == GenId).ToList();

                            foreach (var colleague in personalInfoRequest.colleagues)
                            {
                                var existingColleague = existingColleagues.FirstOrDefault(e => e.EmpGen_Id == GenId && e.colleague_no == index2);
                                if (existingColleague != null)
                                {
                                    existingColleague.Employee_id = colleague.Empid;
                                    existingColleague.Colleague_Name = colleague.Colleague_Name;
                                    existingColleague.Location = colleague.Location;
                                    existingColleague.Date_Modified = DateTime.UtcNow;
                                    existingColleague.Modified_by = GenId.ToString();
                                    existingColleague.Status = "A";
                                    existingColleague.RelationShip = colleague.RelationShip;
                                }
                                else
                                {
                                    var _colleague = new EmployeeColleagueDetails()
                                    {
                                        EmpGen_Id = GenId,
                                        colleague_no = index2,
                                        Employee_id = colleague.Empid,
                                        Colleague_Name = colleague.Colleague_Name,
                                        Location = colleague.Location,
                                        Date_Created = DateTime.UtcNow,
                                        Date_Modified = DateTime.UtcNow,
                                        Created_by = GenId.ToString(),
                                        Modified_by = GenId.ToString(),
                                        Status = "A",
                                        RelationShip = colleague.RelationShip,
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
                            List<EmployeeEmergencyContactDetails> emergencyVMs = new List<EmployeeEmergencyContactDetails>();
                            int index3 = 1;
                            var existingemergencies = _context.EmployeeEmergencyContactDetails.Where(x => x.EmpGen_Id == GenId).ToList();

                            foreach (var emergency in personalInfoRequest.emergencies)
                            {
                                var existingemergency = existingemergencies.FirstOrDefault(e => e.EmpGen_Id == GenId && e.emergency_no == index3);

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
                                        emergency_no = index3,
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
                            _context.SaveChanges();
                        }

                        //Required Details

                        var existingRequired = _context.EmployeeRequiredDocuments.FirstOrDefault(e => e.EmpGen_Id == GenId);

                        if (existingRequired != null)
                        {
                            existingRequired.Aadhar = await SaveCertificateFile(personalInfoRequest.RequiredDocuments.Aadhar, GenId.ToString(), "Aadhar.pdf");
                            existingRequired.Pan = await SaveCertificateFile(personalInfoRequest.RequiredDocuments.Pan, GenId.ToString(), "Pan.pdf");
                            existingRequired.Driving_license = await SaveCertificateFile(personalInfoRequest.RequiredDocuments.Driving_license, GenId.ToString(), "Driving_license.pdf");
                            existingRequired.Passport = await SaveCertificateFile(personalInfoRequest.RequiredDocuments.Passport, GenId.ToString(), "Passport.pdf");
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
                                Aadhar = await SaveCertificateFile(personalInfoRequest.RequiredDocuments.Aadhar, GenId.ToString(), "Aadhar.pdf"),
                                Pan = await SaveCertificateFile(personalInfoRequest.RequiredDocuments.Pan, GenId.ToString(), "Pan.pdf"),
                                Driving_license = await SaveCertificateFile(personalInfoRequest.RequiredDocuments.Driving_license, GenId.ToString(), "Driving_license.pdf"),
                                Passport = await SaveCertificateFile(personalInfoRequest.RequiredDocuments.Passport, GenId.ToString(), "Passport.pdf"),
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
                        _context.ChangeTracker.Clear();
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
                byte[] file = null;

                using (System.IO.FileStream fs = System.IO.File.OpenRead(filepath))
                {
                    file = new byte[fs.Length];
                    int br = fs.Read(file, 0, file.Length);

                    if (br != fs.Length)
                    {
                        throw new IOException("Invalid path");
                    }
                }
                return file;
            }
            return null;
        }

        public async Task<OverallPersonalInfoResponse> GetPersonalInfo(int Id)
        {
            try
            {
                if (Id != 0 || Id != null)
                {
                    OverallPersonalInfoResponse overallPersonalInfoResponse = new OverallPersonalInfoResponse();
                    PersonalInfoResponse personalInfoResponse = new PersonalInfoResponse();

                    GeneralInfoResponse _general = _context.EmployeeGeneralDetails.Where(n => n.Id == Id).Select(general => new GeneralInfoResponse()
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
                        Location = e.Location,
                        RelationShip = e.RelationShip,
                    })
                      .ToList();

                    if (colleague.Count > 0)
                    {
                        personalInfoResponse.colleagues = colleague;
                        personalInfoResponse.IsCollegueAdded = true;
                    }
                    else
                    {
                        personalInfoResponse.IsCollegueAdded = false;
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
                    overallPersonalInfoResponse.result = personalInfoResponse;

                    return overallPersonalInfoResponse;
                }

                else
                {
                    throw new NullReferenceException("Null exception");
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<List<StatusCardResponse>> GetStatusByLoginId(int loginId)
        {
            try
            {
                List<StatusCardResponse> UserResponse = new List<StatusCardResponse>();
                List<StatusCardResponse> Adminresponse = new List<StatusCardResponse>();


                var role = _context.Login.Where(x => x.Id == loginId).FirstOrDefault();
                if (role.Role == "U")
                {
                    if (role.Invited_Status == "Invited")
                    {
                        StatusCardResponse userdet = (from a in _context.Login
                                                      where a.Role == "U" && a.Status == "A"
                                                      where a.Id == loginId
                                                      select new StatusCardResponse
                                                      {
                                                          UserId = a.Id,
                                                          Email = a.EmailId,
                                                          Status = a.Invited_Status,
                                                          Role = a.Role
                                                      }).FirstOrDefault();
                        UserResponse.Add(userdet);
                        return UserResponse;

                    }
                    else
                    {

                        StatusCardResponse OtherStatusUser = (from a in _context.Login
                                                              join b in _context.EmployeeGeneralDetails on a.Id equals b.UserId into
                                                              logGen
                                                              from lg in logGen.DefaultIfEmpty()
                                                              where a.Role == "U" && a.Status == "A" && lg.Status == "A"
                                                              where lg.UserId == loginId
                                                              select new StatusCardResponse
                                                              {
                                                                  UserId = lg.UserId,
                                                                  GenId = lg.Id,
                                                                  Email = a.EmailId,
                                                                  Status = a.Invited_Status,
                                                                  Role = a.Role
                                                              }).FirstOrDefault();
                        UserResponse.Add(OtherStatusUser);

                        return UserResponse;
                    }

                }
                if (role.Role == "A")
                {
                    List<StatusCardResponse> InvitedUsers = (from a in _context.Login
                                                             where a.Role == "U" && a.Status == "A" && a.Invited_Status == "Invited"
                                                             select new StatusCardResponse
                                                             {
                                                                 UserId = a.Id,
                                                                 Email = a.EmailId,
                                                                 Status = a.Invited_Status,
                                                                 Role = a.Role
                                                             }).ToList();

                    Adminresponse.AddRange(InvitedUsers);


                    List<StatusCardResponse> OtherUsers = (from a in _context.Login
                                                           join b in _context.EmployeeGeneralDetails on a.Id equals b.UserId into
                                                           logGen
                                                           from lg in logGen.DefaultIfEmpty()
                                                           where a.Role == "U" && a.Status == "A" && lg.Status == "A" && a.Invited_Status != "Invited"
                                                           select new StatusCardResponse
                                                           {
                                                               UserId = lg.UserId,
                                                               GenId = lg.Id,
                                                               Email = a.EmailId,
                                                               Status = a.Invited_Status,
                                                               Role = a.Role
                                                           }).ToList();
                    return OtherUsers;


                }
                return null;

            }
            catch (Exception e)
            {
                throw;
            }

        }
    }
}

