using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using EmployeeOnboarding.Data;
using EmployeeOnboarding.ViewModels;
using EmployeeOnboarding.Data.Enum;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeOnboarding.Services
{
    public class onboardstatusService
    {

        private ApplicationDbContext _context;
        public onboardstatusService(ApplicationDbContext context)
        {
            _context = context;
        }
        public void ChangeApprovalStatus(int Empid,onboardstatusVM onboardstatus)
        {
            var approved = _context.ApprovalStatus.FirstOrDefault(e => e.EmpGen_Id == Empid && e.Current_Status == 2);

            if (approved != null)
            {
                approved.Current_Status = (int)Status.Approved;
                approved.Date_Modified = DateTime.UtcNow;
                approved.Modified_by = "Admin";
                approved.Status = "A";
                _context.ApprovalStatus.Update(approved);
                _context.SaveChanges();
            }
            else
            {
              var approve = _context.ApprovalStatus.FirstOrDefault(e => e.EmpGen_Id == Empid && e.Current_Status == 3);
               
                if (approve != null)
                {

                    approve.Date_Modified = DateTime.UtcNow;
                    approve.Modified_by = "Admin";
                    approve.Status = "D";
                    _context.ApprovalStatus.Update(approve);
                    _context.SaveChanges();
                }

                var _onboard = new ApprovalStatus()
                {
                    EmpGen_Id = Empid,
                    Current_Status = (int)Status.Approved,
                    Comments = "",
                    Date_Created = DateTime.UtcNow,
                    Date_Modified = DateTime.UtcNow,
                    Created_by = "Admin",
                    Modified_by = "Admin",
                    Status = "A",
                };
                _context.ApprovalStatus.Add(_onboard);
                _context.SaveChanges();
            }

            var official = _context.EmployeeGeneralDetails.FirstOrDefault(e => e.Login_ID == Empid);

            official.Empid = onboardstatus.Emp_id;
            official.Official_EmailId = onboardstatus.Official_EmailId;

            _context.EmployeeGeneralDetails.Update(official);
            _context.SaveChanges();
        }

        public void ChangeCancelStatus(int Empid,commentVM onboardstatus)
        {
            var reject = _context.ApprovalStatus.FirstOrDefault(e => e.EmpGen_Id == Empid && e.Current_Status == 2);

            if (reject != null)
            {
                reject.Current_Status = (int)Status.Rejected;
                reject.Comments = onboardstatus.Comments;
                reject.Date_Created = DateTime.UtcNow;
                reject.Date_Modified = DateTime.UtcNow;
                reject.Created_by = "Admin";
                reject.Modified_by = "Admin";
                reject.Status = "A";
                _context.ApprovalStatus.Update(reject);
                _context.SaveChanges();
            }
            else
            {
                var rejected = _context.ApprovalStatus.FirstOrDefault(e => e.EmpGen_Id == Empid && e.Current_Status == 3);

                if (rejected != null)
                {

                    rejected.Date_Modified = DateTime.UtcNow;
                    rejected.Modified_by = "Admin";
                    rejected.Status = "D";
                    _context.ApprovalStatus.Update(rejected);
                    _context.SaveChanges();
                }
                var _onboard = new ApprovalStatus()
                {
                    EmpGen_Id = Empid,
                    Current_Status = (int)Status.Rejected,
                    Comments = onboardstatus.Comments,
                    Date_Created = DateTime.UtcNow,
                    Date_Modified = DateTime.UtcNow,
                    Created_by = "Admin",
                    Modified_by = "Admin",
                    Status = "A",
                };
                _context.ApprovalStatus.Add(_onboard);
                _context.SaveChanges();
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
                Status= "A",
            };
            _context.ApprovalStatus.Add(_onboard);
            _context.SaveChanges();
        }

        public async Task<rejectcommentVM> RejectedComment(int Empid)
        {
            var _onboard = _context.ApprovalStatus.Where(n => n.EmpGen_Id == Empid).
               Select(onboard => new rejectcommentVM()
               {
                   Comment = onboard.Comments,

               }).FirstOrDefault();

           return _onboard;
        }

    }
    
}
