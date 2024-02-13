

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EmployeeOnboarding.Data.Enum
{
    public enum CovidVaccine
    {
        [Description("Fully")]
        Fully = 1,
        [Description( "Partially")]
        Partially = 2,
        [Description("Not Vaccinated")]
        NotVaccinated = 3,
    }
}
