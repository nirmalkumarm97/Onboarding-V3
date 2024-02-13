using EmployeeOnboarding.Contracts;
using EmployeeOnboarding.Data;
using EmployeeOnboarding.Models;
using EmployeeOnboarding.ViewModels;

namespace EmployeeOnboarding.Repository
{

    public class AuthenticateLogin  : ILogin
    {
        
        private readonly ApplicationDbContext _context;

        public AuthenticateLogin(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<employloginVM> AuthenticateEmp(string email, string password)
        {
            var _succeeded = _context.Login.Where(authUser => authUser.EmailId == email && authUser.Password == password).
               Select(succeeded => new employloginVM()
               {
                   Id = succeeded.Id,
                   Name = succeeded.Name,

               }).FirstOrDefault();

            if (_succeeded == null)
                return null;
            else
                return _succeeded ;
        }

        public async Task<IEnumerable<Login>> getemp()
        {
            return _context.Login.ToList();
        }
    }
}
