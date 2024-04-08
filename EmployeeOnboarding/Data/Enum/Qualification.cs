using System.ComponentModel;
using System.Runtime.Serialization;

namespace EmployeeOnboarding.Data.Enum
{
    public enum Qualification
    {
        [EnumMember(Value = "10th")]
        Tenth,

        [EnumMember(Value = "12th")]
        Twelfth,

        [EnumMember(Value = "Diploma")]
        Diploma,

        [EnumMember(Value = "UG")]
        UG,

        [EnumMember(Value = "PG")]
        PG,

        [EnumMember(Value = "PhD")]
        Phd
    }
}
