//using DocumentFormat.OpenXml.Office2010.Excel;
//using DocumentFormat.OpenXml.Wordprocessing;
//using EmployeeOnboarding.Data;
//using EmployeeOnboarding.Data.Enum;
//using EmployeeOnboarding.Models;
//using EmployeeOnboarding.ViewModels;
//using System.ComponentModel.DataAnnotations;
//using Microsoft.AspNetCore.Http; // Add this namespace for IFormFile
//using System.IO; // Add this namespace for file operations
//using System.Threading.Tasks;
//using EmployeeOnboarding.Contracts;

//namespace EmployeeOnboarding.Services
//{
//    //public class ExistingBankService
//    //{
//    //    //private readonly ApplicationDbContext _context;
//        //private readonly IUserDetailsRepository _userDetailsRepository;

//        //public ExistingBankService(ApplicationDbContext context, IUserDetailsRepository userDetailsRepository)
//        //{
//        //    _context = context;
//        //    _userDetailsRepository = userDetailsRepository;
//        //}

//        //private string SaveImageFile(string image, string Id, string fileName)
//        //{
//        //    if (image == null)
//        //    {
//        //        return null;
//        //    }
//        //    var certificateBytes = Convert.FromBase64String(image);

//        //    var empFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "C:\\A_Onboarding\\OnboardingFiles", Id);
//        //    if (!Directory.Exists(empFolderPath))
//        //    {
//        //        Directory.CreateDirectory(empFolderPath);
//        //    }
//        //    var filePath = Path.Combine(empFolderPath, fileName);
//        //    using (var fileStream = new FileStream(filePath, FileMode.Create))
//        //    {
//        //        //image.CopyTo(fileStream);
//        //        fileStream.WriteAsync(certificateBytes, 0, certificateBytes.Length);

//        //    }
//        //    return filePath;
//        //}

//        //public void AddBank(int genId, ExistingBankVM bank)
//        //{
//        //    var existingBank = _context.EmployeeExistingBankAccount.FirstOrDefault(e => e.EmpGen_Id == genId);

//        //    if (existingBank != null)
//        //    {
//        //        existingBank.Account_name = bank.Account_name;
//        //        existingBank.Bank_name = bank.Bank_name ;
//        //        existingBank.Bank_Branch = bank.Bank_Branch;
//        //        existingBank.Account_number= bank.Account_number;
//        //        existingBank.IFSC_code= bank.IFSC_code;
//        //        existingBank.Joint_Account = bank.Joint_Account;
//        //        existingBank.Proof_submitted = string.Join(",", bank.ProofSubmitted);
//        //        existingBank.Bank_Documents = SaveImageFile(bank.Bank_Documents, genId.ToString(), "Bank_documents.jpeg");
//        //        existingBank.Date_Modified = DateTime.UtcNow;
//        //        existingBank.Modified_by = genId.ToString();
//        //        existingBank.Status = "A";

//        //     }
//        //    else
//        //    {
//        //        // Add new record
//        //        var _bank = new EmployeeExistingBankAccount()
//        //        {
//        //            EmpGen_Id = genId,
//        //            Account_name=bank.Account_name,
//        //            Bank_name=bank.Bank_name,
//        //            Bank_Branch = bank.Bank_Branch,
//        //            Account_number =bank.Account_number,
//        //            IFSC_code=bank.IFSC_code,
//        //            Joint_Account=bank.Joint_Account,
//        //            Proof_submitted = string.Join(",", bank.ProofSubmitted),
//        //            Bank_Documents = SaveImageFile(bank.Bank_Documents, genId.ToString(), "Bank_documents.jpg"),
//        //            Date_Created = DateTime.UtcNow,
//        //            Date_Modified = DateTime.UtcNow,
//        //            Created_by = genId.ToString(),
//        //            Modified_by = genId.ToString(),
//        //            Status = "A"
//        //        };

//        //        _context.EmployeeExistingBankAccount.Add(_bank);
//        //    }
//        //    _context.SaveChanges();
//        //}
//        //private static byte[] GetFile(string filepath)
//        //{
//        //    if (System.IO.File.Exists(filepath))
//        //    {
//        //        System.IO.FileStream fs = System.IO.File.OpenRead(filepath);
//        //        byte[] file = new byte[fs.Length];
//        //        int br = fs.Read(file, 0, file.Length);
//        //        if (br != fs.Length)
//        //        {
//        //            throw new IOException("Invalid path");
//        //        }
//        //        return file;
//        //    }
//        //    return null;
//        //}

//        //public GetExistingBankVM GetBank(int genId)
//        //{
//        //    var _bank = _context.EmployeeExistingBankAccount.FirstOrDefault(n => n.EmpGen_Id == genId);

//        //    if (_bank != null)
//        //    {
//        //        var proofSubmitted = _bank.Proof_submitted;
//        //        var proofSubmittedList = !string.IsNullOrEmpty(proofSubmitted)
//        //            ? proofSubmitted.Split(',').ToList()
//        //            : new List<string>();

//        //        var bankVM = new GetExistingBankVM()
//        //        {
//        //            GenId = (int)_bank.EmpGen_Id,
//        //            Account_name = _bank.Account_name,
//        //            Bank_name = _bank.Bank_name,
//        //            Bank_Branch = _bank.Bank_Branch,
//        //            Account_number = _bank.Account_number,
//        //            IFSC_code = _bank.IFSC_code,
//        //            Joint_Account = _bank.Joint_Account,
//        //            ProofSubmitted = proofSubmittedList,
//        //            Bank_Documents = GetFile(_bank.Bank_Documents)
//        //        };
//        //        return bankVM;
//        //    }
//        //    return null; 
//        //}
//    //}
//}
