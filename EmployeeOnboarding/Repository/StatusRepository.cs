using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using EmployeeOnboarding.Data;
using EmployeeOnboarding.ViewModels;
using EmployeeOnboarding.Data.Enum;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity.UI.Services;
using OpenXmlPowerTools;
using System.Text.Encodings.Web;
using DocumentFormat.OpenXml.Office2010.Excel;
using EmployeeOnboarding.Data.Models;

namespace EmployeeOnboarding.Repository
{
    public class StatusRepository
    {
        private readonly IEmailSender _emailSender;
        private readonly IConfiguration _configuration;
        private ApplicationDbContext _context;
        public StatusRepository(ApplicationDbContext context, IEmailSender emailSender, IConfiguration configuration)
        {
            _context = context;
            _emailSender = emailSender;
            _configuration = configuration;
        }
        public async Task ChangeApprovalStatus(int genId, onboardstatusVM onboardstatus)
        {
            // Validate inputs
            if (genId <= 0)
                throw new ArgumentException("Invalid genId");

            if (onboardstatus == null)
                throw new ArgumentNullException(nameof(onboardstatus));

            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    await UpdateApprovalStatus(genId);
                    UpdateEmployeeGeneralDetails(genId, onboardstatus);
                    await UpdateLoginStatus(genId);

                    transaction.Commit();
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions
                // You might want to log the exception details for troubleshooting
                // For simplicity, just re-throwing the exception here
                throw ex;
            }
        }

        private async Task UpdateApprovalStatus(int genId)
        {
            var approved = _context.ApprovalStatus.FirstOrDefault(e => e.EmpGen_Id == genId);

            if (approved != null && approved.Current_Status == 2)
            {
                // Update existing record to Approved
                approved.Current_Status = (int)Status.Approved;
                approved.Date_Modified = DateTime.UtcNow;
                approved.Modified_by = "Admin";
                approved.Status = "A";
                _context.ApprovalStatus.Update(approved);
            }
            else if (approved != null && approved.Current_Status == 3)
            {
                // Update existing record to Disapproved
                approved.Date_Modified = DateTime.UtcNow;
                approved.Modified_by = "Admin";
                approved.Status = "D";
                _context.ApprovalStatus.Update(approved);

                // If neither approved nor disapproved, create a new ApprovalStatus with Approved status
                var newApproval = new ApprovalStatus()
                {
                    EmpGen_Id = genId,
                    Current_Status = (int)Status.Approved,
                    Comments = "",
                    Date_Created = DateTime.UtcNow,
                    Date_Modified = DateTime.UtcNow,
                    Created_by = "Admin",
                    Modified_by = "Admin",
                    Status = "A",
                };
                _context.ApprovalStatus.Add(newApproval);
            }
            else if (approved != null && approved.Current_Status == 1)
            {
                return;
            }

            await _context.SaveChangesAsync(); // Save changes to ApprovalStatus
        }

        private void UpdateEmployeeGeneralDetails(int genId, onboardstatusVM onboardstatus)
        {
            var official = _context.EmployeeGeneralDetails.FirstOrDefault(e => e.Id == genId);
            if (official != null)
            {
                official.Empid = onboardstatus.Emp_id;
                official.Official_EmailId = onboardstatus.Official_EmailId;
                _context.EmployeeGeneralDetails.Update(official);
            }
            // Save changes to EmployeeGeneralDetails
            _context.SaveChanges();
        }

        private async Task UpdateLoginStatus(int genId)
        {
            var official = _context.EmployeeGeneralDetails.FirstOrDefault(e => e.Id == genId);
            if (official == null) return;

            var login = _context.Login.FirstOrDefault(x => x.Id == official.UserId);
            if (login == null) return;

            login.Invited_Status = "Approved";
            _context.Login.Update(login);

            string empName = login.Name;
            string email = login.EmailId;

            string url = _configuration.GetSection("ApplicationURL").Value;
            await SendApprovalEmail(email, empName, url, official.Empid, official.Official_EmailId);

            await _context.SaveChangesAsync(); // Save changes to Login
        }

        private async Task SendApprovalEmail(string email, string empName, string url, string empId, string officialEmail)
        {
            string subject = "Onboarding Status - Approved";
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
            padding: 20px;
            border: 1px solid #ccc;
            border-radius: 5px;
            background-color: #f9f9f9;
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
        <p>Dear {empName},</p>
        <p>Your onboarding form is approved.</p>
        <p>Your employee ID is {empId} and official email ID is {officialEmail}.</p>
        <p>You can check the status by <a href='{HtmlEncoder.Default.Encode(url)}'>clicking here</a>.</p>
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
                // Log email sending failure
                // You might want to handle this more gracefully, depending on your application's requirements
                throw ex;
            }
        }



        public async Task ChangeCancelStatus(int genId, commentVM onboardstatus)
        {
            try
            {
                // Input validation
                if (genId <= 0)
                {
                    throw new ArgumentException("Invalid genId.");
                }

                using (var transaction = _context.Database.BeginTransaction())
                {
                    var approvalStatus = _context.ApprovalStatus.FirstOrDefault(e => e.EmpGen_Id == genId);

                    if (approvalStatus != null && approvalStatus.Current_Status == 2)
                    {
                        approvalStatus.Current_Status = (int)Status.Rejected;
                        approvalStatus.Comments = onboardstatus.Comments;
                        approvalStatus.Date_Modified = DateTime.UtcNow;
                        approvalStatus.Modified_by = "Admin";
                        approvalStatus.Status = "A";
                        _context.ApprovalStatus.Update(approvalStatus);
                    }
                    else if (approvalStatus != null && approvalStatus.Current_Status == 3)
                    {
                        approvalStatus.Date_Modified = DateTime.UtcNow;
                        approvalStatus.Modified_by = "Admin";
                        approvalStatus.Status = "D";
                        _context.ApprovalStatus.Update(approvalStatus);


                        var newApprovalStatus = new ApprovalStatus()
                        {
                            EmpGen_Id = genId,
                            Current_Status = (int)Status.Rejected,
                            Comments = onboardstatus.Comments,
                            Date_Created = DateTime.UtcNow,
                            Date_Modified = DateTime.UtcNow,
                            Created_by = "Admin",
                            Modified_by = "Admin",
                            Status = "A",
                        };
                        _context.ApprovalStatus.Add(newApprovalStatus);
                    }
                    else if (approvalStatus != null && approvalStatus.Current_Status == 1)
                    {
                        return;
                    }

                    // Update 'Invited_Status' in Login table
                    var userId = _context.EmployeeGeneralDetails.Where(x => x.Id == genId).Select(x => x.UserId).FirstOrDefault();
                    var login = _context.Login.FirstOrDefault(x => x.Id == userId);
                    if (login != null)
                    {
                        login.Invited_Status = "Rejected";
                        _context.Login.Update(login);

                        // Scoped variables
                        string remarks = approvalStatus != null ? approvalStatus.Comments : onboardstatus.Comments;
                        string empName = login.Name;
                        string email = login.EmailId;

                        // Send rejection email
                        await SendRejectionEmailAsync(email, empName, remarks);
                    }

                    await _context.SaveChangesAsync();
                    transaction.Commit();

                    return;
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions
                // You might want to log the exception
                throw new Exception("Error changing cancel status: " + ex.Message);
            }
        }

        private async Task SendRejectionEmailAsync(string email, string empName, string remarks)
        {
            try
            {
                // Input validation for email and name
                if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(empName))
                {
                    throw new ArgumentNullException("Email or employee name is null or empty.");
                }

                // Email sending logic
                string url = _configuration.GetSection("ApplicationURL").Value;
                string subject = "Onboarding Status - Rejected";
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
            padding: 20px;
            border: 1px solid #ccc;
            border-radius: 5px;
            background-color: #f9f9f9;
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
        <p>Dear {empName},</p>
        <p>Your onboarding form has been rejected.</p>
        <p>Rejected Remarks: {remarks}</p>
        <p>You can check the status by <a href='{HtmlEncoder.Default.Encode(url)}'>clicking here</a>.</p>
        <p>Regards,<br />HR Department<br />Ideassion Technology Solutions LLP</p>
    </div>
</body>
</html>";

                await _emailSender.SendEmailAsync(email, subject, body);

                return;
            }
            catch (Exception ex)
            {
                // Handle email sending exceptions
                // You might want to log the exception
                throw new Exception("Error sending rejection email: " + ex.Message);
            }
        }



        public void ChangePendingStatus(int Empid)
        {
            var _onboard = new ApprovalStatus()
            {
                EmpGen_Id = Empid,
                Current_Status = (int)Status.Pending,
                Comments = "",
                Date_Created = DateTime.UtcNow,
                Date_Modified = DateTime.UtcNow,
                Created_by = Empid.ToString(),
                Modified_by = "Admin",
                Status = "A",
            };
            _context.ApprovalStatus.Add(_onboard);
            _context.SaveChanges();
        }

        public async Task<rejectcommentVM> RejectedComment(int genId)
        {
            var _onboard = _context.ApprovalStatus.Where(n => n.EmpGen_Id == genId).
               Select(onboard => new rejectcommentVM()
               {
                   Comment = onboard.Comments,

               }).FirstOrDefault();

            return _onboard;
        }

    }

}
