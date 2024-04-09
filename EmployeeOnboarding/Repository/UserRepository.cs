using DocumentFormat.OpenXml.Office2010.Excel;
using EmployeeOnboarding.Contracts;
using EmployeeOnboarding.Data;
using EmployeeOnboarding.Data.Enum;
using EmployeeOnboarding.Helper;
using EmployeeOnboarding.Request;
using EmployeeOnboarding.Response;
using EmployeeOnboarding.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using OnboardingWebsite.Models;
using System;
using System.Data.Entity;
using System.Globalization;
using System.Net;
using System.Security.Principal;

namespace EmployeeOnboarding.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IServiceProvider _serviceProvider;
        private readonly IEmailSender _emailSender;

        public UserRepository(ApplicationDbContext context, IServiceProvider serviceProvider, IEmailSender emailSender)
        {
            _context = context;
            _serviceProvider = serviceProvider;
            _emailSender = emailSender;
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

        private async Task WriteToFileAsync(FileStream fileStream, byte[] data, CancellationToken cancellationToken)
        {
            await fileStream.WriteAsync(data, 0, data.Length, cancellationToken);
        }

        private async Task<string> SaveFileAsync(string certificateBase64, string genId, string fileName)
        {
            try
            {
                if (string.IsNullOrEmpty(certificateBase64))
                {
                    return null; // Return null if no certificate bytes are provided
                }

                var certificateBytes = Convert.FromBase64String(certificateBase64);

                var empFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "C:\\A_Onboarding\\OnboardingFiles", genId);
                var filePath = Path.Combine(empFolderPath, fileName);

                if (!Directory.Exists(empFolderPath))
                {
                    Directory.CreateDirectory(empFolderPath);
                }
                else
                {
                    //await Task.Delay(100);
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

        //Education details
        public async Task<int> AddEducation(int genId, List<EducationVM> educations)
        {
            using (IDbContextTransaction transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    if (educations.Count > 0)
                    {
                        using (var scope = _serviceProvider.CreateScope())
                        {
                            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                            List<EmployeeEducationDetails> addEducations = new List<EmployeeEducationDetails>();
                            List<EmployeeEducationDetails> updateEducations = new List<EmployeeEducationDetails>();
                            int index1 = 1; // Initialize the Company_no sequence
                            var existingEducations = dbContext.EmployeeEducationDetails.Where(e => e.EmpGen_Id == genId).ToList();

                            foreach (var education in educations)
                            {
                                var existingEducation = existingEducations.Where(x => x.Education_no == index1).FirstOrDefault();
                                if (existingEducation != null)
                                {
                                    var certificateFileName = $"education{index1}.pdf"; // Generate the certificate filename
                                                                                        // Update existing record
                                    existingEducation.Qualification = education.Qualification;
                                    existingEducation.University = education.University;
                                    existingEducation.Institution_name = education.Institution_name;
                                    existingEducation.Degree_achieved = education.Degree_achieved;
                                    existingEducation.specialization = education.specialization;
                                    existingEducation.Passoutyear = education.Passoutyear;
                                    existingEducation.Percentage = education.Percentage;
                                    existingEducation.Edu_certificate = "";
                                    existingEducation.Date_Modified = DateTime.UtcNow;
                                    existingEducation.Modified_by = genId.ToString();
                                    existingEducation.Status = "A";
                                    existingEducation.Status = "A";
                                    existingEducation.Edu_certificate = await SaveFileAsync(education.Edu_certificate, genId.ToString(), certificateFileName);

                                    //_context.Update(existingEducation);
                                    updateEducations.Add(existingEducation);
                                }
                                else
                                {
                                    // Add new record
                                    var certificateFileName = $"education{index1}.pdf"; // Generate the certificate filename
                                    var _education = new EmployeeEducationDetails()
                                    {
                                        EmpGen_Id = genId,
                                        Education_no = index1,
                                        Qualification = education.Qualification,
                                        University = education.University,
                                        Institution_name = education.Institution_name,
                                        Degree_achieved = education.Degree_achieved,
                                        specialization = education.specialization,
                                        Passoutyear = education.Passoutyear,
                                        Percentage = education.Percentage,
                                        Edu_certificate = await SaveFileAsync(education.Edu_certificate, genId.ToString(), certificateFileName),
                                        Date_Created = DateTime.UtcNow,
                                        Date_Modified = DateTime.UtcNow,
                                        Created_by = genId.ToString(),
                                        Modified_by = genId.ToString(),
                                        Status = "A"
                                    };
                                    addEducations.Add(_education);
                                }

                                index1++;

                            }
                            if (addEducations.Count > 0)
                            {
                                dbContext.EmployeeEducationDetails.AddRange(addEducations); // Add new entities
                                dbContext.SaveChanges();
                            }
                            // Save changes asynchronously
                            if (updateEducations.Count > 0)
                            {
                                dbContext.EmployeeEducationDetails.UpdateRange(updateEducations); // Add new entities

                                dbContext.SaveChanges(); // Save changes asynchronously
                            }
                            transaction.Commit();
                            dbContext.ChangeTracker.Clear();
                            return genId;
                        }
                    }
                    else throw new NullReferenceException("Request Cannot be null");
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception(ex.InnerException.Message);
                }

            }
        }


        public List<GetEducationVM> GetEducation(int genId)
        {
            var education = _context.EmployeeEducationDetails.Where(e => e.EmpGen_Id == genId && e.Education_no != null).Select(e => new GetEducationVM
            {
                GenId = e.EmpGen_Id,
                Qualification = Enum.GetName(typeof(Qualification), e.Qualification),
                University = e.University,
                Institution_name = e.Institution_name,
                Degree_achieved = e.Degree_achieved,
                specialization = e.specialization,
                Passoutyear = e.Passoutyear,
                Percentage = e.Percentage,
                Edu_certificate = GetFile(e.Edu_certificate)
            })
                .ToList();

            return education;
        }


        //AddCertificates

        public async Task<int> AddCertificate(int genId, List<CertificateVM> certificates)
        {
            using (IDbContextTransaction transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    //if (certificates.Count > 0)
                    //{
                    using (var scope = _serviceProvider.CreateScope())
                    {
                        var dbcontext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                        List<EmployeeCertifications> AddCertificates = new List<EmployeeCertifications>();
                        List<EmployeeCertifications> UpdateCertificates = new List<EmployeeCertifications>();

                        int index1 = 1; // Initialize the Company_no sequence
                        var existingCertificates = dbcontext.EmployeeCertifications.Where(e => e.EmpGen_Id == genId).ToList();

                        foreach (var certificate in certificates)
                        {
                            var existingCertificate = existingCertificates.Where(x => x.Certificate_no == index1).FirstOrDefault();
                            if (existingCertificate != null)
                            {
                                var certificateFileName = $"certificate{index1}.pdf"; // Generate the certificate filename
                                                                                      // Update existing record
                                existingCertificate.Certificate_name = certificate.Certificate_name;
                                existingCertificate.Issued_by = certificate.Issued_by;
                                DateOnly? Valid_till = certificate.Valid_till == null ? null : DateOnly.Parse(certificate.Valid_till);
                                existingCertificate.Valid_till = Valid_till;
                                existingCertificate.Duration = certificate.Duration;
                                existingCertificate.Specialization = certificate.Specialization;
                                existingCertificate.Percentage = certificate.Percentage;
                                existingCertificate.proof = await SaveFileAsync(certificate.proof, genId.ToString(), certificateFileName);
                                existingCertificate.Date_Modified = DateTime.UtcNow;
                                existingCertificate.Modified_by = genId.ToString();
                                existingCertificate.Status = "A";
                                UpdateCertificates.Add(existingCertificate);

                            }
                            else
                            {
                                // Add new record
                                var certificateFileName = $"certificate{index1}.pdf"; // Generate the certificate filename
                                var _certificate = new EmployeeCertifications()
                                {
                                    EmpGen_Id = genId,
                                    Certificate_no = index1,
                                    Certificate_name = certificate.Certificate_name,
                                    Issued_by = certificate.Issued_by,
                                    Valid_till = certificate.Valid_till == null ? null : DateOnly.Parse(certificate.Valid_till),
                                    Duration = certificate.Duration,
                                    Specialization = certificate.Specialization,
                                    Percentage = certificate.Percentage,
                                    proof = await SaveFileAsync(certificate.proof, genId.ToString(), certificateFileName),
                                    Date_Created = DateTime.UtcNow,
                                    Date_Modified = DateTime.UtcNow,
                                    Created_by = genId.ToString(),
                                    Modified_by = genId.ToString(),
                                    Status = "A"
                                };
                                AddCertificates.Add(_certificate);

                            }

                            index1++;

                        }
                        if (AddCertificates.Count > 0)
                        {
                            dbcontext.EmployeeCertifications.AddRange(AddCertificates);
                        }
                        if (UpdateCertificates.Count > 0)
                        {
                            dbcontext.EmployeeCertifications.UpdateRange(UpdateCertificates);
                        }
                        dbcontext.SaveChanges();
                        transaction.Commit();
                        dbcontext.ChangeTracker.Clear();
                        var id = AddCertificates.Select(x => x.EmpGen_Id).FirstOrDefault();

                        return genId;
                    }
                    //}
                    //return 0;
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    throw new Exception(e.Message);
                }
            }
        }

        public List<getCertificateVM> GetCertificate(int genId)
        {
            var certificate = _context.EmployeeCertifications.Where(e => e.EmpGen_Id == genId && e.Certificate_no != null).Select(e => new getCertificateVM
            {
                GenId = (int)e.EmpGen_Id,
                Certificate_name = e.Certificate_name,
                Issued_by = e.Issued_by,
                Valid_till = e.Valid_till,
                Duration = e.Duration,
                Specialization = e.Specialization,
                Percentage = e.Percentage,
                proof = GetFile(e.proof),
            })
            .ToList();

            return certificate;
        }


        //AddExperience
        public async Task<int> AddExperiences(int genId, List<WorkExperienceVM> experiences)
        {
            using (IDbContextTransaction transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    //if (experiences.Count > 0)
                    //{
                    using (var scope = _serviceProvider.CreateScope())
                    {
                        var dbcontext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                        List<EmployeeExperienceDetails> addExperiences = new List<EmployeeExperienceDetails>();
                        List<EmployeeExperienceDetails> updateExperiences = new List<EmployeeExperienceDetails>();

                        int index1 = 1; // Initialize the Company_no sequence
                        var existingExperiences = dbcontext.EmployeeExperienceDetails.Where(e => e.EmpGen_Id == genId).ToList();
                        foreach (var experience in experiences)
                        {
                            var existingExperience = existingExperiences.Where(x => x.Company_no == index1).FirstOrDefault();

                            if (existingExperience != null)
                            {
                                var certificateFileName = $"experience{index1}.pdf"; // Generate the certificate filename
                                                                                     // Update existing record
                                existingExperience.Company_name = experience.Company_name;
                                existingExperience.Designation = experience.Designation;
                                DateOnly? startDate = experience.StartDate == null ? null : DateOnly.Parse(experience.StartDate);
                                DateOnly? endDate = experience.EndDate == null ? null : DateOnly.Parse(experience.EndDate);
                                existingExperience.StartDate = startDate;
                                existingExperience.EndDate = endDate;
                                existingExperience.Reporting_to = experience.Reporting_to;
                                existingExperience.Reason = experience.Reason;
                                existingExperience.Location = experience.Location;
                                existingExperience.Exp_Certificate = await SaveFileAsync(experience.Exp_Certificate, genId.ToString(), certificateFileName);
                                existingExperience.Date_Modified = DateTime.UtcNow;
                                existingExperience.Modified_by = genId.ToString();
                                existingExperience.Status = "A";

                                updateExperiences.Add(existingExperience);
                            }
                            else
                            {
                                // Add new record
                                var certificateFileName = $"experience{index1}.pdf"; // Generate the certificate filename
                                var _experience = new EmployeeExperienceDetails()
                                {
                                    EmpGen_Id = genId,
                                    Company_no = index1,
                                    Company_name = experience.Company_name,
                                    Designation = experience.Designation,
                                    // Parse and assign DateOnly values
                                    StartDate = experience.StartDate == null ? null : DateOnly.Parse(experience.StartDate),
                                    EndDate = experience.EndDate == null ? null : DateOnly.Parse(experience.EndDate),
                                    Reporting_to = experience.Reporting_to,
                                    Reason = experience.Reason,
                                    Location = experience.Location,
                                    Exp_Certificate = await SaveFileAsync(experience.Exp_Certificate, genId.ToString(), certificateFileName),
                                    Date_Created = DateTime.UtcNow,
                                    Date_Modified = DateTime.UtcNow,
                                    Created_by = genId.ToString(),
                                    Modified_by = genId.ToString(),
                                    Status = "A"
                                };
                                addExperiences.Add(_experience);

                            }

                            index1++;

                        }
                        if (addExperiences.Count > 0)
                        {
                            dbcontext.EmployeeExperienceDetails.AddRange(addExperiences);
                            dbcontext.SaveChanges();
                        }
                        if (updateExperiences.Count > 0)
                        {
                            dbcontext.EmployeeExperienceDetails.UpdateRange(updateExperiences);
                            dbcontext.SaveChanges();
                        }
                        transaction.Commit();
                        dbcontext.ChangeTracker.Clear();
                        return genId;
                    }
                    //}
                    //return 0;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception(ex.Message);
                }
            }
        }


        public List<getExperienceVM> GetCompanyByEmpId(int genId)
        {
            var companyExperiences = _context.EmployeeExperienceDetails.Where(e => e.EmpGen_Id == genId && e.Company_no != null).Select(e => new getExperienceVM
            {
                GenId = (int)e.EmpGen_Id,
                Company_name = e.Company_name,
                Designation = e.Designation,
                Reason = e.Reason,
                Location = e.Location,
                StartDate = e.StartDate,
                EndDate = e.EndDate,
                Reporting_to = e.Reporting_to,
                Exp_Certificate = GetFile(e.Exp_Certificate),
            })
                .ToList();

            return companyExperiences;
        }


        //AddReferences
        public async Task<int> AddReference(int genId, ReferenceVM reference)
        {
            using (IDbContextTransaction transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    //if (reference != null)
                    //{
                    EmployeeReferenceDetails existingreference = _context.EmployeeReferenceDetails.Where(e => e.EmpGen_Id == genId).FirstOrDefault();
                    if (existingreference != null)
                    {
                        existingreference.Referral_name = reference.Referral_name;
                        existingreference.Designation = reference.Designation;
                        existingreference.Company_name = reference.Company_name;
                        existingreference.Contact_number = reference.Contact_number;
                        existingreference.Email_Id = reference.Email_Id;
                        existingreference.Authorize = reference.Authorize;
                        existingreference.Date_Modified = DateTime.UtcNow;
                        existingreference.Modified_by = genId.ToString();
                        existingreference.Status = "A";
                        _context.Update(existingreference);
                        _context.SaveChanges();
                    }
                    else
                    {
                        // Add new record
                        var _reference = new EmployeeReferenceDetails()
                        {
                            EmpGen_Id = genId,
                            Referral_name = reference.Referral_name,
                            Designation = reference.Designation,
                            Company_name = reference.Company_name,
                            Contact_number = reference.Contact_number,
                            Email_Id = reference.Email_Id,
                            Authorize = reference.Authorize,
                            Date_Created = DateTime.UtcNow,
                            Date_Modified = DateTime.UtcNow,
                            Created_by = genId.ToString(),
                            Modified_by = genId.ToString(),
                            Status = "A"
                        };

                        _context.EmployeeReferenceDetails.Add(_reference);
                        _context.SaveChanges();

                    }
                    transaction.Commit();
                    _context.ChangeTracker.Clear();
                    return genId;
                    //}
                    //return 0;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception(ex.Message);
                }
            }
        }

        public GetReferenceVM Getreference(int genId)
        {
            var _reference = _context.EmployeeReferenceDetails.Where(n => n.EmpGen_Id == genId).Select(reference => new GetReferenceVM()
            {
                GenId = (int)reference.EmpGen_Id,
                Referral_name = reference.Referral_name,
                Designation = reference.Designation,
                Company_name = reference.Company_name,
                Contact_number = reference.Contact_number,
                Email_Id = reference.Email_Id,
                Authorize = reference.Authorize

            }).FirstOrDefault();

            return _reference;
        }


        //AddHealthservices
        public async Task<int> AddHealth(int genId, HealthVM health)
        {
            using (IDbContextTransaction transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    if (health != null)
                    {
                        using (var scope = _serviceProvider.CreateScope())
                        {
                            var dbcontext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                            var existingHealth = _context.EmployeeHealthInformation.FirstOrDefault(e => e.EmpGen_Id == genId);

                            if (existingHealth != null)
                            {
                                existingHealth.Specific_health_condition = health.Specific_health_condition;
                                existingHealth.Allergies = health.Allergies;
                                existingHealth.surgery = health.surgery;
                                existingHealth.Surgery_explaination = health.Surgery_explaination;
                                existingHealth.Night_shifts = health.Night_shifts;
                                existingHealth.Disability = health.Disability;
                                existingHealth.Disability_explanation = health.Disability_explanation;
                                existingHealth.CovidVaccine = health.CovidVaccine;
                                existingHealth.Vaccine_certificate = await SaveFileAsync(health.Vaccine_certificate, genId.ToString(), "Vacc_Certificate.pdf");
                                existingHealth.Health_documents = await SaveFileAsync(health.Health_documents, genId.ToString(), "Health_Documents.pdf");
                                existingHealth.Date_Modified = DateTime.UtcNow;
                                existingHealth.Modified_by = genId.ToString();
                                existingHealth.Status = "A";
                                dbcontext.Update(existingHealth);
                                dbcontext.SaveChanges();
                            }
                            else
                            {
                                // Add new record
                                var _health = new EmployeeHealthInformation()
                                {
                                    EmpGen_Id = genId,
                                    Specific_health_condition = health.Specific_health_condition,
                                    Allergies = health.Allergies,
                                    surgery = health.surgery,
                                    Surgery_explaination = health.Surgery_explaination,
                                    Night_shifts = health.Night_shifts,
                                    Disability = health.Disability,
                                    Disability_explanation = health.Disability_explanation,
                                    CovidVaccine = health.CovidVaccine,
                                    Vaccine_certificate = await SaveFileAsync(health.Vaccine_certificate, genId.ToString(), "Vacc_Certificate.pdf"),
                                    Health_documents = await SaveFileAsync(health.Health_documents, genId.ToString(), "Health_Documents.pdf"),
                                    Date_Created = DateTime.UtcNow,
                                    Date_Modified = DateTime.UtcNow,
                                    Created_by = genId.ToString(),
                                    Modified_by = genId.ToString(),
                                    Status = "A"
                                };

                                dbcontext.EmployeeHealthInformation.Add(_health);
                                dbcontext.SaveChanges();

                            }
                            transaction.Commit();
                            dbcontext.ChangeTracker.Clear();
                            return genId;
                        }
                    }
                    else throw new NullReferenceException("Request Cannot be null");
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception(ex.Message);
                }
            }
        }

        public GetHealthVM GetHealth(int Id)
        {
            var _health = _context.EmployeeHealthInformation.Where(n => n.EmpGen_Id == Id).Select(health => new GetHealthVM()
            {
                GenId = health.EmpGen_Id,
                Specific_health_condition = health.Specific_health_condition,
                Allergies = health.Allergies,
                surgery = health.surgery,
                Surgery_explaination = health.Surgery_explaination,
                Night_shifts = health.Night_shifts,
                Disability = health.Disability,
                Disability_explanation = health.Disability_explanation,
                CovidVaccine = health.CovidVaccine,
                Health_documents = GetFile(health.Health_documents),
                Vaccine_certificate = GetFile(health.Vaccine_certificate)
            }).FirstOrDefault();
            return _health;
        }


        //Add Banking Details 

        public async Task<int> AddBank(int genId, ExistingBankVM bank)
        {
            using (IDbContextTransaction transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    if (bank != null)
                    {
                        using (var scope = _serviceProvider.CreateScope())
                        {
                            var dbcontext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                            var existingBank = _context.EmployeeExistingBankAccount.FirstOrDefault(e => e.EmpGen_Id == genId);
                            if (existingBank != null)
                            {
                                existingBank.Account_name = bank.Account_name;
                                existingBank.Bank_name = bank.Bank_name;
                                existingBank.Bank_Branch = bank.Bank_Branch;
                                existingBank.Account_number = bank.Account_number;
                                existingBank.IFSC_code = bank.IFSC_code;
                                existingBank.Joint_Account = bank.Joint_Account;
                                existingBank.Proof_submitted = string.Join(",", bank.ProofSubmitted);
                                existingBank.Bank_Documents = await SaveFileAsync(bank.Bank_Documents, genId.ToString(), "Bank_documents.pdf");
                                existingBank.Date_Modified = DateTime.UtcNow;
                                existingBank.Modified_by = genId.ToString();
                                existingBank.Status = "A";

                                dbcontext.Update(existingBank);
                                dbcontext.SaveChanges();
                            }
                            else
                            {
                                // Add new record
                                var _bank = new EmployeeExistingBankAccount()
                                {
                                    EmpGen_Id = genId,
                                    Account_name = bank.Account_name,
                                    Bank_name = bank.Bank_name,
                                    Bank_Branch = bank.Bank_Branch,
                                    Account_number = bank.Account_number,
                                    IFSC_code = bank.IFSC_code,
                                    Joint_Account = bank.Joint_Account,
                                    Proof_submitted = string.Join(",", bank.ProofSubmitted),
                                    Bank_Documents = await SaveFileAsync(bank.Bank_Documents, genId.ToString(), "Bank_documents.pdf"),
                                    Date_Created = DateTime.UtcNow,
                                    Date_Modified = DateTime.UtcNow,
                                    Created_by = genId.ToString(),
                                    Modified_by = genId.ToString(),
                                    Status = "A"
                                };

                                dbcontext.EmployeeExistingBankAccount.Add(_bank);
                                dbcontext.SaveChanges();

                            }
                            ////pending status
                            //var existingpending = dbcontext.ApprovalStatus.Where(x => x.EmpGen_Id == genId).FirstOrDefault();
                            //if (existingpending == null)
                            //{

                            //    var _onboard = new ApprovalStatus()
                            //    {
                            //        EmpGen_Id = genId,
                            //        Current_Status = (int)Status.Pending,
                            //        Comments = "",
                            //        Date_Created = DateTime.UtcNow,
                            //        Date_Modified = DateTime.UtcNow,
                            //        Created_by = genId.ToString(),
                            //        Status = "A",
                            //    };
                            //    dbcontext.ApprovalStatus.Add(_onboard);

                            //    var userId = dbcontext.EmployeeGeneralDetails.Where(x => x.Id == genId).FirstOrDefault();
                            //    if (userId != null)
                            //    {
                            //        var userlogin = dbcontext.Login.Where(x => x.Id == userId.UserId).FirstOrDefault();
                            //        if (userlogin != null)
                            //        {
                            //            await SendOnboardingSubmissionEmail(userlogin.EmailId, userlogin.Name);
                            //        }
                            //    }

                            //    dbcontext.SaveChanges();
                            //}
                            transaction.Commit();
                            dbcontext.ChangeTracker.Clear();
                            return genId;
                        }
                    }
                    else throw new NullReferenceException("Request Cannot be null");
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception(ex.Message);
                }
            }
        }


        public GetExistingBankVM GetBank(int genId)
        {
            var _bank = _context.EmployeeExistingBankAccount.FirstOrDefault(n => n.EmpGen_Id == genId);

            if (_bank != null)
            {
                var proofSubmitted = _bank.Proof_submitted;
                var proofSubmittedList = !string.IsNullOrEmpty(proofSubmitted)
                    ? proofSubmitted.Split(',').ToList()
                    : new List<string>();

                var bankVM = new GetExistingBankVM()
                {
                    GenId = (int)_bank.EmpGen_Id,
                    Account_name = _bank.Account_name,
                    Bank_name = _bank.Bank_name,
                    Bank_Branch = _bank.Bank_Branch,
                    Account_number = _bank.Account_number,
                    IFSC_code = _bank.IFSC_code,
                    Joint_Account = _bank.Joint_Account,
                    ProofSubmitted = proofSubmittedList,
                    Bank_Documents = GetFile(_bank.Bank_Documents)
                };
                return bankVM;
            }
            return null;
        }
        private async Task SendOnboardingSubmissionEmail(string email, string name)
        {
            string subject = "Onboarding Form Submission";
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
    </style>
</head>
<body>
    <div class='container'>
        <h1>Hi {name},</h1>
        <p>Your onboarding form is submitted Successfully.</p>
        <p>Wait for approval by HR team.</p>
        <p>Regards,<br />HR Team</p>
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
                throw new Exception("Error sending onboarding submission email: " + ex.Message);
            }
        }

        private async Task SendOnboardingReSubmissionEmail(string email, string name)
        {
            string subject = "Form Resubmission Confirmation";
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
            </style>
        </head>
        <body>
            <div class='container'>
                <h1>Hi {name},</h1>
                <p>Your form has been resubmitted successfully.</p>
                <p>We have received your updated information. Please wait for further instructions or confirmation.</p>
                <p>Thank you for your cooperation.</p>
                <p>Regards,<br />HR Team</p>
            </div>
        </body>
        </html>";

            try
            {
                // Assuming _emailSender is an instance of an email sending service
                await _emailSender.SendEmailAsync(email, subject, body);
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine("Error sending form resubmission confirmation email: " + ex.Message);
                // Rethrow the exception to propagate it further if necessary
                throw;
            }
        }


        public async Task<string> CreateSelfDeclaration(int genId, SelfDeclarationRequest selfDeclarationRequest)
        {
            using (IDbContextTransaction transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var existingSelfDeclaration = _context.SelfDeclaration.FirstOrDefault(x => x.EmpGen_Id == genId);

                    if (existingSelfDeclaration == null)
                    {
                        CreateNewSelfDeclaration(genId, selfDeclarationRequest);

                        var userId = _context.EmployeeGeneralDetails.FirstOrDefault(x => x.Id == genId)?.UserId;
                        var userLogin = _context.Login.FirstOrDefault(x => x.Id == userId);
                        if (userLogin != null)
                        {
                            await SendOnboardingSubmissionEmail(userLogin.EmailId, userLogin.Name);
                        }
                    }
                    else
                    {
                        UpdateExistingSelfDeclaration(existingSelfDeclaration, selfDeclarationRequest);
                        if (UpdateApprovalStatusIfRejected(genId))
                        {
                            // If approval status was updated from Rejected to Pending, send resubmission email
                            var userId = _context.EmployeeGeneralDetails.FirstOrDefault(x => x.Id == genId)?.UserId;
                            var userLogin = _context.Login.FirstOrDefault(x => x.Id == userId);
                            if (userLogin != null)
                            {
                                await SendOnboardingReSubmissionEmail(userLogin.EmailId, userLogin.Name);
                            }
                        }
                    }
                    await _context.SaveChangesAsync();
                    transaction.Commit();

                    return "succeed";
                }
                catch (FormatException ex)
                {
                    transaction.Rollback();
                    throw new Exception(ex.Message);
                }

            }
        }

        #region Create process
        private void CreateNewSelfDeclaration(int genId, SelfDeclarationRequest selfDeclarationRequest)
        {
            SelfDeclaration self = new SelfDeclaration()
            {
                EmpGen_Id = genId,
                Name = selfDeclarationRequest.Name,
                CreatedBy = selfDeclarationRequest.CreatedBy,
                CreatedDate = DateTime.ParseExact(selfDeclarationRequest.Date, "dd-MM-yyyy", CultureInfo.InvariantCulture),
                Status = "A"
            };
            _context.Add(self);
            CreateApprovalStatusIfNotExists(genId);
        }

        private void CreateApprovalStatusIfNotExists(int genId)
        {
            var existingPendingStatus = _context.ApprovalStatus.FirstOrDefault(x => x.EmpGen_Id == genId);
            if (existingPendingStatus == null)
            {
                var onboardStatus = new ApprovalStatus()
                {
                    EmpGen_Id = genId,
                    Current_Status = (int)Status.Pending,
                    Comments = "",
                    Date_Created = DateTime.UtcNow,
                    Date_Modified = DateTime.UtcNow,
                    Created_by = genId.ToString(),
                    Status = "A",
                };
                _context.ApprovalStatus.Add(onboardStatus);
            }
        }
        #endregion Create Process

        #region UpdateProcess
        private void UpdateExistingSelfDeclaration(SelfDeclaration existingSelfDeclaration, SelfDeclarationRequest selfDeclarationRequest)
        {
            existingSelfDeclaration.Name = selfDeclarationRequest.Name;
            existingSelfDeclaration.CreatedBy = selfDeclarationRequest.CreatedBy;
            existingSelfDeclaration.CreatedDate = DateTime.ParseExact(selfDeclarationRequest.Date, "dd-MM-yyyy", CultureInfo.InvariantCulture);

            _context.Update(existingSelfDeclaration);
        }

        private bool UpdateApprovalStatusIfRejected(int genId)
        {
            var existingStatus = _context.ApprovalStatus.FirstOrDefault(x => x.EmpGen_Id == genId && x.Current_Status == (int)Status.Rejected);
            if (existingStatus != null)
            {
                existingStatus.Current_Status = (int)Status.Pending;
                existingStatus.Comments = ""; // Clear comments for resubmission
                existingStatus.Date_Modified = DateTime.UtcNow;
                _context.Update(existingStatus);
                return true; // Status was updated
            }
            return false; // Status was not updated
        }
        #endregion Update Process
        public async Task<SelfDeclarationResponse> GetSelfDeclaration(int genId)
        {
            try
            {
                // Fetch Empname from EmployeeGeneralDetails for the given genId
                var employee =  _context.EmployeeGeneralDetails
                                              .FirstOrDefault(x => x.Id == genId);
                // Fetch SelfDeclaration record for the given genId
                var selfDeclaration =  _context.SelfDeclaration
                                        .FirstOrDefault(a => a.EmpGen_Id == genId);


                if (employee == null)
                {
                    // If EmployeeGeneralDetails record not found, return default response
                    return new SelfDeclarationResponse
                    {
                        Name = null,
                        Date = null,
                        IsSelfDeclared = false,
                    };
                }

                else if (selfDeclaration == null)
                {
                    // If no SelfDeclaration record found, return default response
                    return new SelfDeclarationResponse
                    {
                        Name = employee.Empname,
                        Date = null,
                        IsSelfDeclared = false
                    };
                }
                else
                {
                    // Create a SelfDeclarationResponse object
                    SelfDeclarationResponse selfDec = new SelfDeclarationResponse()
                    {
                        // Set Name to the fetched Empname
                        Name = employee.Empname,

                        // Set Date to CreatedDate formatted as short date string
                        Date = selfDeclaration.CreatedDate.Date.ToShortDateString(),

                        // Set IsSelfDeclared to true
                        IsSelfDeclared = true
                    };

                    // Return the SelfDeclarationResponse
                    return selfDec;
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                // You might want to log the exception details somewhere
                throw new Exception(ex.Message);
                // Return a default response indicating an error
            }
        }
    }
}