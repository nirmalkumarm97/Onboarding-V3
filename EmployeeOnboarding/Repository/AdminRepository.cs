using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using EmployeeOnboarding.Contracts;
using EmployeeOnboarding.Data;
using System.Data.Entity.Core.Objects;
using System.Diagnostics.Metrics;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Data.Entity.Core.Mapping;
using EmployeeOnboarding.ViewModels;
using EmployeeOnboarding.Data.Enum;
using OpenXmlPowerTools;
using DocumentFormat.OpenXml.Spreadsheet;
using EmployeeOnboarding.Response;
using EmployeeOnboarding.Request;
using DocumentFormat.OpenXml.Wordprocessing;
using EmployeeOnboarding.Constants;

namespace EmployeeOnboarding.Repository
{
    public class AdminRepository : IAdminRepository
    {

        public readonly ApplicationDbContext _context;
        public AdminRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<AdminDashBoardUsersData> GetInvitedEmployeeDetails(AdminRequest adminRequest)
        {
            AdminDashBoardUsersData response = new AdminDashBoardUsersData();
            var InvitedDetails = (from l in _context.Login
                                  where l.Status == "A" && (l.Invited_Status == "Invited" || l.Invited_Status == "Confirmed") &&
                                  (l.Name.ToLower().Contains(adminRequest.SearchCriteria.ToLower()) || l.EmailId.ToLower().Contains(adminRequest.SearchCriteria.ToLower()) || string.IsNullOrWhiteSpace(adminRequest.SearchCriteria))
                                  select new Dashboard1VM()
                                  {
                                      Login_Id = l.Id,
                                      Name = l.Name,
                                      DateModified = l.Date_Modified,
                                      Email_id = l.EmailId,
                                      Current_Status = l.Invited_Status,
                                      CreatedDate = l.Date_Created
                                  }).Skip((adminRequest.PageNumber - 1) * adminRequest.PageSize).Take(adminRequest.PageSize).OrderByDescending(x => x.CreatedDate).ToList();
            response.result = adminRequest.OrderByNew == true ? InvitedDetails.OrderBy(x => x.CreatedDate).ToList() : InvitedDetails;
            response.OverallCount = InvitedDetails.Count;
            return response;
        }
        public async Task<AdminDashBoardUsersData> GetRejectedEmployeeDetails(AdminRequest adminRequest)
        {
            AdminDashBoardUsersData response = new AdminDashBoardUsersData();

            var RejectedDetails = (from l in _context.Login
                                   join e in _context.EmployeeGeneralDetails on l.Id equals e.UserId
                                   join a in _context.ApprovalStatus on e.Id equals a.EmpGen_Id
                                   where l.Status == "A" && l.Role == "U" &&
                                         e.Status == "A" &&
                                         a.Status == "A" && a.Current_Status == 3 &&
                                         (
                                             e.Id.ToString().ToLower().Contains(adminRequest.SearchCriteria.ToLower()) ||
                                             e.Empname.ToLower().Contains(adminRequest.SearchCriteria.ToLower()) ||
                                             e.Contact_no.ToString().ToLower().Contains(adminRequest.SearchCriteria.ToLower()) ||
                                             e.Personal_Emailid.ToLower().Contains(adminRequest.SearchCriteria.ToLower()) ||
                                             string.IsNullOrWhiteSpace(adminRequest.SearchCriteria)
                                         )
                                   select new Dashboard1VM()
                                   {
                                       Login_Id = l.Id,
                                       EmpGen_Id = a.EmpGen_Id,
                                       Name = e.Empname,
                                       Contact_no = e.Contact_no,
                                       DateModified = a.Date_Modified,
                                       Email_id = l.EmailId,
                                       Current_Status = ((Data.Enum.Status)a.Current_Status).ToString(),
                                       RejectedComments = a.Comments,
                                       CreatedDate = a.Date_Created
                                   })
                     .Skip((adminRequest.PageNumber - 1) * adminRequest.PageSize)
                     .Take(adminRequest.PageSize)
                     .OrderByDescending(x => x.DateModified)
                     .ToList();

            response.result = adminRequest.OrderByNew == true ? RejectedDetails.OrderBy(x => x.DateModified).ToList() : RejectedDetails;
            response.OverallCount = RejectedDetails.Count;
            return response;
        }

        public async Task<AdminDashBoardUsersData> GetPendingEmployeeDetails(AdminRequest adminRequest)
        {
            AdminDashBoardUsersData response = new AdminDashBoardUsersData();

            var PendingDetails = (from l in _context.Login
                                  join e in _context.EmployeeGeneralDetails on l.Id equals e.UserId
                                  join a in _context.ApprovalStatus on e.Id equals a.EmpGen_Id
                                  where l.Status == "A" && l.Role == "U" &&
                                        e.Status == "A" &&
                                        a.Status == "A" && a.Current_Status == 2 &&
                                        (
                                            e.Id.ToString().ToLower().Contains(adminRequest.SearchCriteria.ToLower()) ||
                                            e.Empname.ToLower().Contains(adminRequest.SearchCriteria.ToLower()) ||
                                            e.Contact_no.ToString().ToLower().Contains(adminRequest.SearchCriteria.ToLower()) ||
                                            e.Personal_Emailid.ToLower().Contains(adminRequest.SearchCriteria.ToLower()) ||
                                            string.IsNullOrWhiteSpace(adminRequest.SearchCriteria)
                                        )
                                  select new Dashboard1VM()
                                  {
                                      Login_Id = l.Id,
                                      EmpGen_Id = e.Id,
                                      Contact_no = e.Contact_no,
                                      Name = l.Name,
                                      DateModified = a.Date_Modified,
                                      Email_id = l.EmailId,
                                      Current_Status = ((Data.Enum.Status)a.Current_Status).ToString(),
                                      CreatedDate = a.Date_Created,
                                  })
                    .Skip((adminRequest.PageNumber - 1) * adminRequest.PageSize)
                    .Take(adminRequest.PageSize)
                    .OrderByDescending(x => x.DateModified)
                    .ToList();
            response.result = adminRequest.OrderByNew == true ? PendingDetails.OrderBy(x => x.DateModified).ToList() : PendingDetails;
            response.OverallCount = PendingDetails.Count;
            return response;
        }

        public async Task<AdminDashBoardEmployeesData> GetEmployeeDetails(AdminRequest adminRequest)
        {
            AdminDashBoardEmployeesData response = new AdminDashBoardEmployeesData();

            var employeedetails = (from l in _context.Login
                                   join e in _context.EmployeeGeneralDetails on l.Id equals e.UserId
                                   join a in _context.ApprovalStatus on e.Id equals a.EmpGen_Id
                                   where l.Status == "A" && l.Role == "U" &&
                                         e.Status == "A" &&
                                         a.Status == "A" && a.Current_Status == 1 &&
                                         (
                                             e.Id.ToString().ToLower().Contains(adminRequest.SearchCriteria.ToLower()) ||
                                             e.Empname.ToLower().Contains(adminRequest.SearchCriteria.ToLower()) ||
                                             e.Contact_no.ToString().ToLower().Contains(adminRequest.SearchCriteria.ToLower()) ||
                                             e.Personal_Emailid.ToLower().Contains(adminRequest.SearchCriteria.ToLower()) ||
                                             string.IsNullOrWhiteSpace(adminRequest.SearchCriteria)
                                         )
                                   select new Dashboard2VM()
                                   {
                                       EmpGen_Id = e.Id,
                                       Empid = e.Empid,
                                       Empname = e.Empname,
                                       Contact_no = e.Contact_no,
                                       Email = e.Official_EmailId,
                                       education = (_context.EmployeeEducationDetails.Where(x => x.EmpGen_Id == e.Id).Select(x => x.Degree_achieved).OrderBy(x => x).LastOrDefault()),
                                       Status = ((Data.Enum.Status)a.Current_Status).ToString(),
                                       CreatedDate = a.Date_Created,
                                       DateModified = a.Date_Modified,
                                   })
                    .Skip((adminRequest.PageNumber - 1) * adminRequest.PageSize)
                    .Take(adminRequest.PageSize)
                    .OrderByDescending(x => x.DateModified)
                    .ToList();
            response.result = adminRequest.OrderByNew == true ? employeedetails.OrderBy(x => x.DateModified).ToList() : employeedetails;
            response.OverallCount = employeedetails.Count;
            return response;
        }

        public async Task<AdminDashBoardUsersData> GetExpiredDetails(AdminRequest adminRequest)
        {
            AdminDashBoardUsersData response = new AdminDashBoardUsersData();

            var expiredDetails = (from l in _context.Login
                                  where l.Status == "A" && (l.Invited_Status == "Expired") &&
                                  (l.Name.ToLower().Contains(adminRequest.SearchCriteria.ToLower()) || l.EmailId.ToLower().Contains(adminRequest.SearchCriteria.ToLower()) || string.IsNullOrWhiteSpace(adminRequest.SearchCriteria))
                                  select new Dashboard1VM()
                                  {
                                      Login_Id = l.Id,
                                      Name = l.Name,
                                      DateModified = l.Date_Modified,
                                      Email_id = l.EmailId,
                                      Current_Status = l.Invited_Status,
                                      CreatedDate = l.Date_Created
                                  }).Skip((adminRequest.PageNumber - 1) * adminRequest.PageSize).Take(adminRequest.PageSize).OrderByDescending(x => x.CreatedDate).ToList();

            response.result = adminRequest.OrderByNew == true ? expiredDetails.OrderBy(x => x.CreatedDate).ToList() : expiredDetails;
            response.OverallCount = expiredDetails.Count;
            return response;
        }

        //       // EnumExtensionMethods.GetEnumDescription((BloodGroup) general.BloodGrp)
        //        public async Task<List<PersonalInfoVM>>? GetPersonalInfo(int id)
        //        {
        //            var address = (from e in _context.EmployeeGeneralDetails where e.Id == id && e.Status == "A" join ea in _context.EmployeeAddressDetails on e.Id equals ea.EmpGen_Id where ea.Status == "A" select ea).ToArray();
        //            var emppersonal = (from e in _context.EmployeeGeneralDetails
        //                               where e.Id == id && e.Status == "A"
        //                               join ec in _context.EmployeeContactDetails on e.Id equals ec.EmpGen_Id
        //                               where ec.Status == "A"
        //                               join ead in _context.EmployeeAdditionalInfo on e.Id equals ead.EmpGen_Id
        //                               where ead.Status == "A"
        //                               join al in _context.ApprovalStatus on e.Id equals al.EmpGen_Id
        //                               where al.Current_Status == 2
        //                               select new PersonalInfoVM()
        //                               {
        //                                   Id = e.Id,
        //                                   EmpName = e.EmployeeName,
        //                                   FatherName = e.FatherName,
        //                                   DOB = e.DOB,
        //                                   mailId = ec.Personal_Emailid,
        //                                   MaritialStatus = ((Data.Enum.MartialStatus)e.MaritalStatus).ToString(),
        //                                   DOM = e.DateOfMarriage,
        //                                   Gender = ((Data.Enum.Gender)e.Gender).ToString(),
        //                                   bloodgrp = EnumExtensionMethods.GetEnumDescription((BloodGroup)e.BloodGrp),
        //                                   Contactno = ec.Contact_no,
        //                                   ECP = ec.Emgy_Contactperson,
        //                                   ECR = ((Data.Enum.EmergencyContactRelation)ec.Emgy_Contactrelation).ToString(),
        //                                   ECN = ec.Emgy_Contactno,
        //                                   PermanentAddress = new AddressVM1()
        //                                   {
        //                                       Address = address[0].Address,
        //                                       Country = _context.Country.Where(x => x.Id == address[0].Country_Id).Select(x => x.Country_Name).FirstOrDefault(),
        //                                       State = _context.State.Where(x => x.Id == address[0].State_Id).Select(x => x.State_Name).FirstOrDefault(),
        //                                       City = _context.City.Where(x => x.Id == address[0].City_Id).Select(x => x.City_Name).FirstOrDefault(),
        //                                       Pincode = address[0].Pincode
        //                                   },
        //                                   TemporaryAddress = new AddressVM1()
        //                                   {
        //                                       Address = address[1].Address,
        //                                       Country = _context.Country.Where(x => x.Id == address[1].Country_Id).Select(x => x.Country_Name).FirstOrDefault(),
        //                                       State = _context.State.Where(x => x.Id == address[1].State_Id).Select(x => x.State_Name).FirstOrDefault(),
        //                                       City = _context.City.Where(x => x.Id == address[1].City_Id).Select(x => x.City_Name).FirstOrDefault(),
        //                                       Pincode = address[1].Pincode
        //                                   },
        //                                   Disability = ead.Disability,
        //                                   Disablility_type = EnumExtensionMethods.GetEnumDescription((DisabilityType)ead.Disablility_type),
        //                                   CovidSts = EnumExtensionMethods.GetEnumDescription((VaccinationStatus)ead.Covid_VaccSts),
        //                                   CovidCerti = GetFile(ead.Vacc_Certificate),
        //                                   educationDetailsVMs = Education(id),
        //                                   experienceVMs = Experrience(id)
        //                               }).ToList();
        //            return emppersonal;
        //        }
        //        public List<EducationDetailsVM> Education(int employeeid)
        //        {
        //            List<EducationDetailsVM> edVM = new List<EducationDetailsVM>();
        //            var education = (from e in _context.EmployeeGeneralDetails where e.Id == employeeid && e.Status == "A" join eed in _context.EmployeeEducationDetails on e.Id equals eed.EmpGen_Id where eed.Status == "A" select eed);
        //            foreach (var educationdetails in education)
        //            {
        //                edVM.Add(new EducationDetailsVM()
        //                {
        //                    programme = educationdetails.programme,
        //                    CollegeName = educationdetails.CollegeName,
        //                    Degree = educationdetails.Degree,
        //                    Major = educationdetails.specialization,
        //                    PassedoutYear = educationdetails.Passoutyear,
        //                    Certificate = GetFile(educationdetails.Certificate)
        //                });

        //            }
        //            return edVM;
        //        }
        //        public List<ExperienceVM> Experrience(int employeeid)
        //        {
        //            List<ExperienceVM> exVM = new List<ExperienceVM>();
        //            var experiencecount = (from e in _context.EmployeeGeneralDetails where e.Id == employeeid && e.Status == "A" join eed in _context.EmployeeExperienceDetails on e.Id equals eed.EmpGen_Id where eed.Status == "A" select eed);
        //            foreach (var experience in experiencecount)
        //            {
        //                exVM.Add(new ExperienceVM()
        //                {
        //                    CompanyName = experience.Company_name,
        //                    StartDate = (DateOnly)experience.StartDate,
        //                    EndDate = (DateOnly)experience.EndDate,
        //                    Designation = experience.Designation,
        //                    ReasonForLeaving = experience.Reason,
        //                    ExperienceCerti = GetFile(experience.Exp_Certificate)
        //                });
        //            }
        //            return exVM;
        //        }

        //        public static byte[] GetFile(string filepath)
        //        {
        //            if (System.IO.File.Exists(filepath))
        //            {
        //                System.IO.FileStream fs = System.IO.File.OpenRead(filepath);
        //                byte[] file = new byte[fs.Length];
        //                int br = fs.Read(file, 0, file.Length);
        //                if (br != fs.Length)
        //                {
        //                    throw new IOException("Invalid path");
        //                }
        //                return file;
        //            }
        //            return null;
        //        }


        //        public async Task<List<ApprovedUserDetails>>? GetApprovedEmpDetails(int id)
        //        {
        //            var address = (from e in _context.EmployeeGeneralDetails where e.Id == id && e.Status == "A" join ea in _context.EmployeeAddressDetails on e.Id equals ea.EmpGen_Id where ea.Status == "A" select ea).ToArray();
        //            var emppersonal = (from e in _context.EmployeeGeneralDetails
        //                               where e.Id == id && e.Status == "A"
        //                               join ec in _context.EmployeeContactDetails on e.Id equals ec.EmpGen_Id
        //                               where ec.Status == "A"
        //                               join ead in _context.EmployeeAdditionalInfo on e.Id equals ead.EmpGen_Id
        //                               where ead.Status == "A"
        //                               join al in _context.ApprovalStatus on e.Id equals al.EmpGen_Id
        //                               where al.Current_Status == 1 && al.Status == "A"
        //                               select new ApprovedUserDetails()
        //                               {
        //                                   Id = e.Id,
        //                                   EmpId = e.Empid,
        //                                   EmpName = e.EmployeeName,
        //                                   Offical_EmailId = e.Official_EmailId,
        //                                   mailid = ec.Personal_Emailid,
        //                                   FatherName = e.FatherName,
        //                                   DOB = e.DOB,
        //                                   MaritialStatus = ((Data.Enum.MartialStatus)e.MaritalStatus).ToString(),
        //                                   DOM = e.DateOfMarriage,
        //                                   Gender = ((Data.Enum.Gender)e.Gender).ToString(),
        //                                   bloodgrp = EnumExtensionMethods.GetEnumDescription((BloodGroup)e.BloodGrp),
        //                                   Contactno = ec.Contact_no,
        //                                   ECP = ec.Emgy_Contactperson,
        //                                   ECR = ((Data.Enum.EmergencyContactRelation)ec.Emgy_Contactrelation).ToString(),
        //                                   ECN = ec.Emgy_Contactno,
        //                                   PermanentAddress = new AddressVM1()
        //                                   {
        //                                       Address = address[0].Address,
        //                                       Country = _context.Country.Where(x => x.Id == address[0].Country_Id).Select(x => x.Country_Name).FirstOrDefault(),
        //                                       State = _context.State.Where(x => x.Id == address[0].State_Id).Select(x => x.State_Name).FirstOrDefault(),
        //                                       City = _context.City.Where(x => x.Id == address[0].City_Id).Select(x => x.City_Name).FirstOrDefault(),
        //                                       Pincode = address[0].Pincode
        //                                   },
        //                                   TemporaryAddress = new AddressVM1()
        //                                   {
        //                                       Address = address[1].Address,
        //                                       Country = _context.Country.Where(x => x.Id == address[1].Country_Id).Select(x => x.Country_Name).FirstOrDefault(),
        //                                       State = _context.State.Where(x => x.Id == address[1].State_Id).Select(x => x.State_Name).FirstOrDefault(),
        //                                       City = _context.City.Where(x => x.Id == address[1].City_Id).Select(x => x.City_Name).FirstOrDefault(),
        //                                       Pincode = address[1].Pincode
        //                                   },
        //                                   Disability = ead.Disability,
        //                                   Disablility_type = EnumExtensionMethods.GetEnumDescription((DisabilityType)ead.Disablility_type),
        //                                   CovidSts = EnumExtensionMethods.GetEnumDescription((VaccinationStatus)ead.Covid_VaccSts),
        //                                   CovidCerti = GetFile(ead.Vacc_Certificate),
        //                                   educationDetailsVMs = Education(id),
        //                                   experienceVMs = Experrience(id)
        //                               }).ToList();
        //            return emppersonal;
        //        }

        //        public async Task<List<DashboardVM>>? SearchApprovedEmpDetails(string name)
        //        {
        //            var employeedetails = (from l in _context.Login
        //                                   where l.Status == "A"
        //                                   join e in _context.EmployeeGeneralDetails on l.Id equals e.Login_ID
        //                                   where e.Status == "A" && e.EmployeeName.ToLower().Contains(name.ToLower())
        //                                   join al in _context.ApprovalStatus on e.Id equals al.EmpGen_Id
        //                                   where al.Current_Status == 1 && al.Status == "A"
        //                                   join ec in _context.EmployeeContactDetails on e.Id equals ec.EmpGen_Id
        //                                   where ec.Status == "A"
        //                                   select new DashboardVM()
        //                                   {
        //                                       EmpGen_Id = e.Id,
        //                                       Empid = e.Empid,
        //                                       Empname = e.EmployeeName,
        //                                       Contact = ec.Contact_no,
        //                                       Email = e.Official_EmailId,
        //                                       education = (_context.EmployeeEducationDetails.Where(x => x.EmpGen_Id == e.Id).Select(x => x.Degree).OrderBy(x => x).LastOrDefault())
        //                                   }).ToList();
        //            return employeedetails;
        //        }

        //        public async Task<List<Dashboard1VM>>? SearchPendingEmpDetails(string name)
        //        {
        //            var PendingDetails = (from l in _context.Login
        //                                  where l.Status == "A"
        //                                  join e in _context.EmployeeGeneralDetails on l.Id equals e.Login_ID
        //                                  join a in _context.ApprovalStatus on e.Id equals a.EmpGen_Id
        //                                  where a.Status == "A" && a.Current_Status == 2 && l.Name.ToLower().Contains(name.ToLower())
        //                                  select new Dashboard1VM()
        //                                  {
        //                                      Login_Id = l.Id,
        //                                      EmpGen_Id = a.EmpGen_Id,
        //                                      Name = l.Name,
        //                                      DateModified = a.Date_Modified,
        //                                      Email_id = l.EmailId,
        //                                      Current_Status = ((Data.Enum.Status)a.Current_Status).ToString()
        //                                  }).ToList();
        //            return PendingDetails;
        //        }

        //        public async Task<List<Dashboard1VM>>? SearchInvitedEmpDetails(string name)
        //        {
        //            var InvitedDetails = (from l in _context.Login
        //                                  where l.Status == "A" && l.Invited_Status == "Invited" || l.Invited_Status == "Confirmed" && l.Name.ToLower().Contains(name.ToLower())
        //                                  select new Dashboard1VM()
        //                                  {
        //                                      Login_Id = l.Id,
        //                                      EmpGen_Id = a.EmpGen_Id,
        //                                      Name = l.Name,
        //                                      DateModified = l.Date_Modified,
        //                                      Email_id = l.EmailId,
        //                                      Current_Status = l.Invited_Status
        //                                  }).ToList();
        //            return InvitedDetails;
        //        }

        //        public async Task<List<Dashboard1VM>>? SearchRejectedEmpDetails(string name)
        //        {
        //            var RejectedDetails = (from e in _context.EmployeeGeneralDetails
        //                                   where e.Status == "A"
        //                                   join l in _context.Login on e.Login_ID equals l.Id
        //                                   where l.Status == "A"
        //                                   join a in _context.ApprovalStatus on e.Id equals a.EmpGen_Id
        //                                   where a.Status == "A" && a.Current_Status == 3 && l.Name.ToLower().Contains(name.ToLower())
        //                                   select new Dashboard1VM()
        //                                   {
        //                                       Login_Id = l.Id,
        //                                       EmpGen_Id = a.EmpGen_Id,
        //                                       Name = l.Name,
        //                                       DateModified = a.Date_Modified,
        //                                       Email_id = l.EmailId,
        //                                       Current_Status = ((Data.Enum.Status)a.Current_Status).ToString()
        //                                   }).ToList();
        //            return RejectedDetails;
        //        }








        //        /*var employeedetails = (from e in _context.EmployeeGeneralDetails
        //                                  where e.Status == "A"
        //                                  join a in _context.Status on e.Empid equals a.Empid
        //                                  where a.Status == "A" && a.Approved == null && a.Cancelled == null
        //                                  join l in _context.Login on e.Empid equals l.Empid
        //                                  where l.Status == "A"
        //                                  join ec in _context.EmployeeContactDetails on e.Empid equals ec.Empid
        //                                  where ec.Status == "A"
        //                                  join ee in _context.EmployeeEducationDetails on e.Empid equals ee.Empid
        //                                  where ee.Passoutyear == deg && ee.Status == "A"
        //                                  select new DashboardVM()
        //                                  {
        //                                      Empid = e.Empid,
        //                                      Empname = e.EmployeeName,
        //                                      designation = l.Designation,
        //                                      Contact = ec.Contact_no,
        //                                      Email = l.Emailid,
        //                                      education = ee.Degree
        //                                  }).ToList();
        //           return employeedetails;*/


        //        //join ed in _context.EmployeeEducationDetails on e.Id equals ed.EmpGen_Id
        //        //where ed.Status == "A"

        //        //join ed in _context.EmployeeEducationDetails on e.Id equals ed.EmpGen_Id
        //        //where ed.Status == "A"
        //        ///
        //        //education = _context.EmployeeEducationDetails.Where(x => x.Passoutyear == getMaxPassoutYear(ed.EmpGen_Id)).Select(x => x.Degree).FirstOrDefault()
        //        //education = _context.EmployeeEducationDetails.Where(x => x.Passoutyear == getMaxPassoutYear(ed.EmpGen_Id)).Select(x => x.Degree).FirstOrDefault()

        //        /* public string getMaxPassoutYear(int id)
        //        {
        //            var maxyear = _context.EmployeeEducationDetails.Where(x => x.Passoutyear == id).Select(x => x.Degree).ToString();
        //            return maxyear;
        //        }*/

        //        //var employeedetails = (from e in _context.EmployeeGeneralDetails
        //        //                       where e.Status == "A"
        //        //                       join a in _context.Status on e.Empid equals a.Empid
        //        //                       where a.Status == "A" && a.Approved == null && a.Cancelled == null
        //        //                       join l in _context.Login on e.Empid equals l.Empid
        //        //                       where l.Status == "A"
        //        //                       join ec in _context.EmployeeContactDetails on e.Empid equals ec.Empid
        //        //                       where ec.Status == "A"
        //        //                       join ee in _context.EmployeeEducationDetails on e.Empid equals ee.Empid
        //        //                       where ee.Passoutyear == deg && ee.Status == "A"
        //        //                       select new DashboardVM()
        //        //                       {
        //        //                           Empid = e.Empid,
        //        //                           Empname = e.EmployeeName,
        //        //                           designation = l.Designation,
        //        //                           Contact = ec.Contact_no,
        //        //                           Email = l.Emailid,
        //        //                           education = ee.Degree
        //        //                       }).ToList();
        //        //return employeedetails;


        //        /*public async Task DeleteEmployee(string[] employeeId)
        //{
        //    for (int i = 0; i < employeeId.Count(); i++)
        //    {
        //        if (employeeId != null)
        //        {
        //            var login = _context.Login.FirstOrDefault(l => l.Empid == employeeId[i]);
        //            var general = _context.EmployeeGeneralDetails.FirstOrDefault(g => g.Empid == employeeId[i]);
        //            var contact = _context.EmployeeContactDetails.FirstOrDefault(c => c.Empid == employeeId[i]);
        //            var address = _context.EmployeeAddressDetails.FirstOrDefault(a => a.Empid == employeeId[i]);
        //            var addtional = _context.EmployeeAdditionalInfo.FirstOrDefault(ad => ad.Empid == employeeId[i]);
        //            var education = _context.EmployeeEducationDetails.FirstOrDefault(ed => ed.Empid == employeeId[i]);
        //            var experience = _context.EmployeeExperienceDetails.FirstOrDefault(ex => ex.Empid == employeeId[i]);
        //            var approval = _context.Status.FirstOrDefault(app => app.Empid == employeeId[i]);
        //            if (login != null && general != null && contact != null && address != null && addtional != null && education != null & education != null && experience != null && approval != null)
        //            {
        //                login.Status = "D";
        //                login.Date_Modified = DateTime.UtcNow;
        //                login.Modified_by = "Admin";
        //                general.Status = "D";
        //                general.Date_Modified = DateTime.UtcNow;
        //                general.Modified_by = "Admin";
        //                contact.Status = "D";
        //                contact.Date_Modified = DateTime.UtcNow;
        //                contact.Modified_by = "Admin";
        //                address.Status = "D";
        //                address.Date_Modified = DateTime.UtcNow;
        //                address.Modified_by = "Admin";
        //                addtional.Status = "D";
        //                addtional.Date_Modified = DateTime.UtcNow;
        //                addtional.Modified_by = "Admin";
        //                education.Status = "D";
        //                education.Date_Modified = DateTime.UtcNow;
        //                education.Modified_by = "Admin";
        //                experience.Status = "D";
        //                experience.Date_Modified = DateTime.UtcNow;
        //                experience.Modified_by = "Admin";
        //                approval.Status = "D";
        //                approval.Date_Modified = DateTime.UtcNow;
        //                approval.Modified_by = "Admin";
        //                _context.SaveChanges();
        //            }
        //        }
        //    }
        //}
        //public async Task<List<DashboardVM>> GetEmployeeDetails()
        //{

        //    var deg = (from e in _context.EmployeeGeneralDetails
        //               where e.Status == "A"
        //               join ee in _context.EmployeeEducationDetails on e.Empid equals ee.Empid
        //               where ee.Status == "A"
        //               select ee.Passoutyear).Max();
        //    var employeedetails = (from e in _context.EmployeeGeneralDetails
        //                           where e.Status == "A"
        //                           join a in _context.Status on e.Empid equals a.Empid
        //                           where a.Status == "A" && a.Approved == null && a.Cancelled == null
        //                           join l in _context.Login on e.Empid equals l.Empid
        //                           where l.Status == "A"
        //                           join ec in _context.EmployeeContactDetails on e.Empid equals ec.Empid
        //                           where ec.Status == "A"
        //                           join ee in _context.EmployeeEducationDetails on e.Empid equals ee.Empid
        //                           where ee.Passoutyear == deg && ee.Status == "A"
        //                           select new DashboardVM()
        //                           {
        //                               Empid = e.Empid,
        //                               Empname = e.EmployeeName,
        //                               designation = l.Designation,
        //                               Contact = ec.Contact_no,
        //                               Email = l.Emailid,
        //                               education = ee.Degree
        //                           }).ToList();
        //    return employeedetails;
        //}

        //public async Task<List<PersonalInfoVM>>? GetPersonalInfo(string employeeid)
        //{
        //    var address = (from e in _context.EmployeeGeneralDetails where e.Empid == employeeid join ea in _context.EmployeeAddressDetails on e.Empid equals ea.Empid select ea).ToArray();
        //    var degree = (from e in _context.EmployeeGeneralDetails where e.Empid == employeeid join ee in _context.EmployeeEducationDetails on e.Empid equals ee.Empid select ee).ToArray();
        //    var experiencecount = (from e in _context.EmployeeGeneralDetails where e.Empid == employeeid join eed in _context.EmployeeExperienceDetails on e.Empid equals eed.Empid select eed).ToArray();
        //    var employeepersonal = (from e in _context.EmployeeGeneralDetails
        //                            where e.Empid == employeeid
        //                            join ea in _context.EmployeeAddressDetails on e.Empid equals ea.Empid
        //                            join ec in _context.EmployeeContactDetails on e.Empid equals ec.Empid
        //                            join ead in _context.EmployeeAdditionalInfo on e.Empid equals ead.Empid
        //                            select new PersonalInfoVM()
        //                            {
        //                                Empid = e.Empid,
        //                                EmpName = e.EmployeeName,
        //                                FatherName = e.FatherName,
        //                                DOB = e.DOB,
        //                                mailId = ec.Personal_Emailid,
        //                                MaritialStatus = e.MaritalName,
        //                                DOM = e.DateOfMarriage,
        //                                Contactno = ec.Contact_no,
        //                                Gender = e.Gender,
        //                                ECP = ec.Emgy_Contactperson,
        //                                ECR = ec.Emgy_Contactrelation,
        //                                ECN = ec.Emgy_Contactno,
        //                                PermanentAddress = new AddressVM()
        //                                {
        //                                    Address = address[0].Address,
        //                                    Country = address[0].Country,
        //                                    City = address[0].City,
        //                                    State = address[0].State,
        //                                    Pincode = address[0].Pincode
        //                                },
        //                                TemporaryAddress = new AddressVM()
        //                                {
        //                                    Address = address[1].Address,
        //                                    Country = address[1].Country,
        //                                    City = address[1].City,
        //                                    State = address[1].State,
        //                                    Pincode = address[1].Pincode
        //                                },
        //                                CovidSts = ead.Covid_VaccSts,
        //                                CovidCerti = ead.Vacc_Certificate,
        //                                UGDetails = new EducationDetailsVM()
        //                                {
        //                                    CollegeName = degree[0].CollegeName,
        //                                    Degree = degree[0].Degree,
        //                                    Major = degree[0].specialization,
        //                                    PassedoutYear = degree[0].Passoutyear,
        //                                    Certificate = GetFile(degree[0].Certificate)
        //                                },
        //                                PGDetails = new EducationDetailsVM()
        //                                {
        //                                    CollegeName = degree[1].CollegeName,
        //                                    Degree = degree[1].Degree,
        //                                    Major = degree[1].specialization,
        //                                    PassedoutYear = degree[1].Passoutyear,
        //                                    Certificate = GetFile(degree[1].Certificate)
        //                                },
        //                                experienceVMs = Experrience(employeeid)
        //                            }).ToList();

        //    return employeepersonal;
        //}

    }

}
