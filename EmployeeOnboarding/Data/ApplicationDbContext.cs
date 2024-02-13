 using Microsoft.EntityFrameworkCore;

namespace EmployeeOnboarding.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options) 
        {
            
        }

        public DbSet<Login> Login { get; set; }
        public DbSet<EmployeeCertifications> EmployeeCertifications { get; set; }
        public DbSet<EmployeeColleagueDetails> EmployeeColleagueDetails { get; set; }
        public DbSet<EmployeeContactDetails> EmployeeContactDetails { get; set; }
        public DbSet<EmployeeEducationDetails> EmployeeEducationDetails { get; set; }
        public DbSet<EmployeeEmergencyContactDetails> EmployeeEmergencyContactDetails { get; set; }
        public DbSet<EmployeeExistingBankAccount> EmployeeExistingBankAccount { get; set; }
        public DbSet<EmployeeExperienceDetails> EmployeeExperienceDetails { get; set; }
        public DbSet<EmployeeFamilyDetails> EmployeeFamilyDetails { get; set; }
        public DbSet<EmployeeGeneralDetails> EmployeeGeneralDetails { get; set; }
        public DbSet<EmployeeHealthInformation> EmployeeHealthInformation { get; set; }
        public DbSet<EmployeeHobbyMembership> EmployeeHobbyMembership { get; set; }
        public DbSet<EmployeeReferenceDetails> EmployeeReferenceDetails { get; set; }
        public DbSet<EmployeeRequiredDocuments> EmployeeRequiredDocuments { get; set; }
        public DbSet<ApprovalStatus> ApprovalStatus { get; set; }
        public DbSet<Country> Country { get; set; }
        public DbSet<State> State { get; set; }
        public DbSet<City> City { get; set; }
        //public DbSet<Roles> Roles { get; set; }
        //public DbSet<UserRoles> UserRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Login>(entity => {
                entity.HasKey(k => k.Id);
            });
        }
    }
}
