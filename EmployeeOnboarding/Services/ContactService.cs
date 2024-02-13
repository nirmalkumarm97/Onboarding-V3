using EmployeeOnboarding.Data;
using EmployeeOnboarding.Data.Enum;
using EmployeeOnboarding.ViewModels;

namespace EmployeeOnboarding.Services
{
    public class ContactService
    {
        private ApplicationDbContext _context;
        public ContactService(ApplicationDbContext context)
        {
            _context = context;
        }


        public void AddPresentContact(int Id, ContactVM contact)
        {
            var existingContact = _context.EmployeeContactDetails.FirstOrDefault(e => e.EmpGen_Id == Id && e.Address_Type == "Present");

            if (existingContact != null)
            {
                //Update existing record
                existingContact.Address1 = contact.Address1;
                existingContact.Address2 = contact.Address2;
                existingContact.Country_Id = contact.Country_Id;
                existingContact.State_Id = contact.State_Id;
                existingContact.City_Id = contact.City_Id;
                existingContact.Pincode = contact.Pincode;
                existingContact.Date_Modified = DateTime.UtcNow;
                existingContact.Modified_by = Id.ToString();
                existingContact.Status = "A";
            }
            else
            {
                //Add new record

                var _contact = new EmployeeContactDetails()
                {
                    EmpGen_Id = Id,
                    Address_Type ="Present",
                    Address1 = contact.Address1,
                    Address2 = contact.Address2,
                    Country_Id = contact.Country_Id,
                    State_Id = contact.State_Id,
                    City_Id = contact.City_Id,
                    Pincode = contact.Pincode,
                    Date_Created = DateTime.UtcNow,
                    Date_Modified = DateTime.UtcNow,
                    Created_by = Id.ToString(),
                    Modified_by = Id.ToString(),
                    Status = "A"
                };

                _context.EmployeeContactDetails.Add(_contact);
            }

            _context.SaveChanges();
        }


        public void AddPermanentContact(int Id, ContactVM contact)
        {
            var existingContact = _context.EmployeeContactDetails.FirstOrDefault(e => e.EmpGen_Id == Id && e.Address_Type == "Permanent");

            if (existingContact != null)
            {
                existingContact.Address1 = contact.Address1;
                existingContact.Address2 = contact.Address2;
                existingContact.Country_Id = contact.Country_Id;
                existingContact.State_Id = contact.State_Id;
                existingContact.City_Id = contact.City_Id;
                existingContact.Pincode = contact.Pincode;
                existingContact.Date_Modified = DateTime.UtcNow;
                existingContact.Modified_by = Id.ToString();
                existingContact.Status = "A";
            }
            else
            {
                //Add new record

                var _contact = new EmployeeContactDetails()
                {
                    EmpGen_Id = Id,
                    Address_Type = "Permanent",
                    Address1 = contact.Address1,
                    Address2 = contact.Address2,
                    Country_Id = contact.Country_Id,
                    State_Id = contact.State_Id,
                    City_Id = contact.City_Id,
                    Pincode = contact.Pincode,
                    Date_Created = DateTime.UtcNow,
                    Date_Modified = DateTime.UtcNow,
                    Created_by = Id.ToString(),
                    Modified_by = Id.ToString(),
                    Status = "A"
                };

                _context.EmployeeContactDetails.Add(_contact);
            }

            _context.SaveChanges();
        }


        //get method

        public List<ContactVM> GetContact(int Id)
        {
            var _contact = _context.EmployeeContactDetails.Where(n => n.EmpGen_Id == Id).Select(contact => new ContactVM()
            {
                Address1 = contact.Address1,
                Address2 = contact.Address2,
                Country_Id = contact.Country_Id,
                State_Id = contact.State_Id,
                City_Id = contact.City_Id,
                Pincode = contact.Pincode,

            }).ToList();
            return _contact;

        }
    }
}
