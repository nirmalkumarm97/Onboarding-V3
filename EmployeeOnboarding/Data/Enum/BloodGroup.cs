

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EmployeeOnboarding.Data.Enum
{
    public enum BloodGroup
    {
        [Description("O+")]
        OPositive = 1,
        [Description( "A+")]
        APositive=2,
        [Description("B+")]
        BPositive=3,
        [Description("AB+")]
        ABPositive=4,
        [Description("AB-")]
        ABNegative=5,
        [Description("A-")]
        ANegative=6,
        [Description("B-")]
        BNegative=7,
        [Description("O-")]
        ONegative=8
    }
}
