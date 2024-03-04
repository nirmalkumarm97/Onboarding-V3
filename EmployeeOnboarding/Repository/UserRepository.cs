using DocumentFormat.OpenXml.Office2010.Excel;
using EmployeeOnboarding.Contracts;
using EmployeeOnboarding.Data;
using EmployeeOnboarding.Data.Enum;
using EmployeeOnboarding.ViewModels;
using OnboardingWebsite.Models;

namespace EmployeeOnboarding.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
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
        public async Task<List<EmployeeEducationDetails>> AddEducation(int genId, List<EducationVM> educations)
        {
            try
            {
                List<EmployeeEducationDetails> educationVMs = new List<EmployeeEducationDetails>();
                int index1 = 1; // Initialize the Company_no sequence

                foreach (var education in educations)
                {
                    var existingEducation = _context.EmployeeEducationDetails.FirstOrDefault(e => e.EmpGen_Id == genId && e.Education_no == index1);

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
                        existingEducation.Edu_certificate = await SaveFileAsync(education.Edu_certificate, genId.ToString(), certificateFileName);
                        existingEducation.Date_Modified = DateTime.UtcNow;
                        existingEducation.Modified_by = genId.ToString();
                        existingEducation.Status = "A";
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
                        educationVMs.Add(_education);
                    }

                    index1++;

                }
                _context.EmployeeEducationDetails.AddRange(educationVMs);
                _context.SaveChanges();
                _context.ChangeTracker.Clear();

                //var id = educationVMs.Select(x => x.EmpGen_Id).FirstOrDefault();

                return educationVMs;

            }
            catch (Exception ex)
            {
                throw;
            }
        }
       
        public List<GetEducationVM> GetEducation(int genId)
        {
            var education = _context.EmployeeEducationDetails.Where(e => e.EmpGen_Id == genId && e.Education_no != null).Select(e => new GetEducationVM
            {
                GenId = e.EmpGen_Id,
                Qualification = e.Qualification,
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

        public async Task<List<EmployeeCertifications>> AddCertificate(int genId, List<CertificateVM> certificates)
        {
            List<EmployeeCertifications> certificateVMs = new List<EmployeeCertifications>();
            int index1 = 1; // Initialize the Company_no sequence

            foreach (var certificate in certificates)
            {
                var existingCertificate = _context.EmployeeCertifications.FirstOrDefault(e => e.EmpGen_Id == genId && e.Certificate_no == index1);

                if (existingCertificate != null)
                {
                    var certificateFileName = $"certificate{index1}.pdf"; // Generate the certificate filename
                    // Update existing record
                    existingCertificate.Certificate_name = certificate.Certificate_name;
                    existingCertificate.Issued_by = certificate.Issued_by;
                    DateOnly Valid_till = DateOnly.Parse(certificate.Valid_till);
                    existingCertificate.Valid_till = Valid_till;
                    existingCertificate.Duration = certificate.Duration;
                    existingCertificate.Percentage = certificate.Percentage;
                    existingCertificate.proof = await SaveFileAsync(certificate.proof, genId.ToString(), certificateFileName);
                    existingCertificate.Date_Modified = DateTime.UtcNow;
                    existingCertificate.Modified_by = genId.ToString();
                    existingCertificate.Status = "A";

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
                        Valid_till = DateOnly.Parse(certificate.Valid_till),
                        Duration = certificate.Duration,
                        Percentage = certificate.Percentage,
                        proof = await SaveFileAsync(certificate.proof, genId.ToString(), certificateFileName),
                        Date_Created = DateTime.UtcNow,
                        Date_Modified = DateTime.UtcNow,
                        Created_by = genId.ToString(),
                        Modified_by = genId.ToString(),
                        Status = "A"
                    };
                    certificateVMs.Add(_certificate);

                }

                index1++;

            }
            _context.EmployeeCertifications.AddRange(certificateVMs);
            _context.SaveChanges();
            _context.ChangeTracker.Clear();
            var id = certificateVMs.Select(x => x.EmpGen_Id).FirstOrDefault();

            return certificateVMs;
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
                Percentage = e.Percentage,
                proof = GetFile(e.proof),
            })
            .ToList();

            return certificate;
        }


//AddExperience
        public async Task<List<EmployeeExperienceDetails>> AddExperiences(int genId, List<WorkExperienceVM> experiences)
        {
            List<EmployeeExperienceDetails> experienceVMs = new List<EmployeeExperienceDetails>();
            int index1 = 1; // Initialize the Company_no sequence

            foreach (var experience in experiences)
            {
                var existingExperience = _context.EmployeeExperienceDetails.FirstOrDefault(e => e.EmpGen_Id == genId && e.Company_no == index1);

                if (existingExperience != null)
                {
                    var certificateFileName = $"experience{index1}.pdf"; // Generate the certificate filename
                    // Update existing record
                    existingExperience.Company_name = experience.Company_name;
                    existingExperience.Designation = experience.Designation;
                    DateOnly startDate = DateOnly.Parse(experience.StartDate);
                    DateOnly endDate = DateOnly.Parse(experience.EndDate);
                    existingExperience.StartDate = startDate;
                    existingExperience.EndDate = endDate;
                    existingExperience.Reporting_to = experience.Reporting_to;
                    existingExperience.Reason = experience.Reason;
                    existingExperience.Location = experience.Location;
                    existingExperience.Exp_Certificate = await SaveFileAsync(experience.Exp_Certificate, genId.ToString(), certificateFileName);
                    existingExperience.Date_Modified = DateTime.UtcNow;
                    existingExperience.Modified_by = genId.ToString();
                    existingExperience.Status = "A";
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
                        StartDate = DateOnly.Parse(experience.StartDate),
                        EndDate = DateOnly.Parse(experience.EndDate),
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
                    experienceVMs.Add(_experience);

                }

                index1++;

            }
            _context.EmployeeExperienceDetails.AddRange(experienceVMs);
            _context.SaveChanges();
            _context.ChangeTracker.Clear();
            //pending status
            var _onboard = new ApprovalStatus()
            {
                EmpGen_Id = genId,
                Current_Status = (int)Status.Pending,
                Comments = "",
                Date_Created = DateTime.UtcNow,
                Date_Modified = DateTime.UtcNow,
                Created_by = genId.ToString(),
                Modified_by = "Admin",
                Status = "A",
            };
            _context.ApprovalStatus.Add(_onboard);
            _context.SaveChanges();
            _context.ChangeTracker.Clear();
            var id = experienceVMs.Select(x => x.EmpGen_Id).FirstOrDefault();

            return experienceVMs;

        }

        public List<getExperienceVM> GetCompanyByEmpId(int genId)
        {
            var companyExperiences = _context.EmployeeExperienceDetails.Where(e => e.EmpGen_Id == genId && e.Company_no != null).Select(e => new getExperienceVM
            {
                GenId = (int)e.EmpGen_Id,
                Company_name = e.Company_name,
                Designation = e.Designation,
                Reason = e.Reason,
                StartDate = e.StartDate,
                EndDate = e.EndDate,
                Exp_Certificate = GetFile(e.Exp_Certificate),
            })
                .ToList();

            return companyExperiences;
        }


//AddReferences
        public async Task<string> AddReference(int genId, ReferenceVM reference)
        {
            var existingreference = _context.EmployeeReferenceDetails.FirstOrDefault(e => e.EmpGen_Id == genId);

            if (existingreference != null)
            {
                existingreference.Referral_name = existingreference.Referral_name;
                existingreference.Designation = existingreference.Designation;
                existingreference.Company_name = existingreference.Company_name;
                existingreference.Contact_number = existingreference.Contact_number;
                existingreference.Email_Id = existingreference.Email_Id;
                existingreference.Authorize = existingreference.Authorize;
                existingreference.Date_Modified = DateTime.UtcNow;
                existingreference.Modified_by = genId.ToString();
                existingreference.Status = "A";
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
            }
            _context.SaveChanges();
            _context.ChangeTracker.Clear();
            return "Succeed";
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
        public async Task<string> AddHealth(int genId, HealthVM health)
        {
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

                _context.EmployeeHealthInformation.Add(_health);
            }
            _context.SaveChanges();
            _context.ChangeTracker.Clear();
            return "Succeed";
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

        public async Task<string> AddBank(int genId, ExistingBankVM bank)
        {
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
                existingBank.Bank_Documents = await SaveFileAsync(bank.Bank_Documents, genId.ToString(), "Bank_documents.jpeg");
                existingBank.Date_Modified = DateTime.UtcNow;
                existingBank.Modified_by = genId.ToString();
                existingBank.Status = "A";

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
                    Bank_Documents = await SaveFileAsync(bank.Bank_Documents, genId.ToString(), "Bank_documents.jpg"),
                    Date_Created = DateTime.UtcNow,
                    Date_Modified = DateTime.UtcNow,
                    Created_by = genId.ToString(),
                    Modified_by = genId.ToString(),
                    Status = "A"
                };

                _context.EmployeeExistingBankAccount.Add(_bank);
            }
            _context.SaveChanges();
            _context.ChangeTracker.Clear();
            return "Succeed";
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

    }
}