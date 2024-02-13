using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Tracing;

namespace EmployeeOnboarding.Data.Enum
{
    public enum VaccinationStatus
    {
        [Description("Partially Vaccinated")]
        Partially_Vaccinated=1,
        [Description("Fully Vaccinated")]
        Fully_Vaccinated=2,
        [Description("None")]
        None = 3
    }
}
