using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Wordprocessing;
using EmployeeOnboarding.Data;
using EmployeeOnboarding.Data.Enum;
using EmployeeOnboarding.Models;
using EmployeeOnboarding.ViewModels;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http; // Add this namespace for IFormFile
using System.IO; // Add this namespace for file operations
using System.Threading.Tasks;

namespace EmployeeOnboarding.Services
{
    public class GeneralDetailService
    {
        private ApplicationDbContext _context;

        public GeneralDetailService(ApplicationDbContext context)
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

        public void AddGeneral(int Id, GeneralVM general)
        {
            var existingGeneral = _context.EmployeeGeneralDetails.FirstOrDefault(e => e.Login_ID == Id);

            if (existingGeneral != null)
            {
                existingGeneral.Empname = general.Empname;
                existingGeneral.Personal_Emailid = general.Personal_Emailid;
                existingGeneral.Contact_no = general.Contact_no;

                DateOnly DOB = DateOnly.Parse(general.DOB);
                existingGeneral.DOB = DOB;

                existingGeneral.Nationality = general.Nationality;
                existingGeneral.Gender = general.Gender;
                existingGeneral.MaritalStatus = general.MaritalStatus;

                DateOnly DateOfMarriage = DateOnly.Parse(general.DateOfMarriage);
                existingGeneral.DateOfMarriage = DateOfMarriage;

                existingGeneral.BloodGrp = general.BloodGrp;

                existingGeneral.Profile_pic = SaveImageFile(general.Profile_pic, Id.ToString(), "Profile.jpeg");

                existingGeneral.Date_Modified = DateTime.UtcNow;
                existingGeneral.Modified_by = Id.ToString();
                existingGeneral.Status = "A";
            }
            else
            {
                // Add new record
                var _general = new EmployeeGeneralDetails()
                {
                    Login_ID = Id,
                    Empname = general.Empname,
                    Personal_Emailid = general.Personal_Emailid,
                    Contact_no = general.Contact_no,
                    DOB = DateOnly.Parse(general.DOB),
                    Nationality = general.Nationality,
                    Gender = general.Gender,
                    MaritalStatus = general.MaritalStatus,
                    DateOfMarriage = DateOnly.Parse(general.DateOfMarriage),
                    BloodGrp = general.BloodGrp,
                    Profile_pic = SaveImageFile(general.Profile_pic, Id.ToString(), "Profile.jpg"),
                    Date_Created = DateTime.UtcNow,
                    Date_Modified = DateTime.UtcNow,
                    Created_by = Id.ToString(),
                    Modified_by = Id.ToString(),
                    Status = "A"
                };

                _context.EmployeeGeneralDetails.Add(_general);
            }

            var submit = _context.Login.FirstOrDefault(e => e.Id == Id);

            submit.Invited_Status = "Submitted";

            _context.Login.Update(submit);

            _context.SaveChanges();
        }

        public GetGeneralVM GetGeneral(int Id)
        {
            var _general = _context.EmployeeGeneralDetails.Where(n => n.Login_ID == Id).Select(general => new GetGeneralVM()
            {
                Empname = general.Empname,
                Personal_Emailid = general.Personal_Emailid,
                Contact_no = general.Contact_no,
                DOB = general.DOB,
                Nationality = general.Nationality,
                Gender = ((Gender)general.Gender).ToString(),
                MaritalStatus = ((MartialStatus)general.Gender).ToString(),
                DateOfMarriage = general.DateOfMarriage,
                BloodGrp = EnumExtensionMethods.GetEnumDescription((BloodGroup)general.BloodGrp)

            }).FirstOrDefault();

            return _general;
        }
    }
}
