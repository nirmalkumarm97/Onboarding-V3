using EmployeeOnboarding.Data;
using EmployeeOnboarding.Data.Enum;
using EmployeeOnboarding.ViewModels;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeOnboarding.Services
{
    public class StateService
    {

        private ApplicationDbContext _context;
        public StateService(ApplicationDbContext context)

            {
                _context = context;
            }

         /*   public void AddState(int Id,State state)

            {
            var existingstate = _context.State.FirstOrDefault(e => e.Id == Id && e.Country_Id == 1);

                if (existingstate != null)
                {
                //Update existing record

               existingstate.Country_Id = state.Country_Id;
                existingstate.State_Name = state.State_Name;
                existingstate.Date_Created = DateTime.UtcNow;
                existingstate.Date_Modified= DateTime.UtcNow;

              



                 }
                else
                {
                    //Add new record

                    var _state = new State()
                    {
                      Country_Id = state.Country_Id,
                       State_Name = state.State_Name,   
                       Date_Created = DateTime.UtcNow,
                       Date_Modified = DateTime.UtcNow,
                    };

                    _context.State.Add(_state);
                _context.SaveChanges();
              }

              
            }*/
        public State GetState(int Id)
        {
            var _state = _context.State.Where(n => n.Id == Id && n.Country_Id == 1).Select(state => new State()
            {
                Country_Id = state.Country_Id,
                State_Name = state.State_Name,
                Date_Created = DateTime.UtcNow,
                Date_Modified = DateTime.UtcNow,

            }).FirstOrDefault();
            return _state;
        }

    }
    }
