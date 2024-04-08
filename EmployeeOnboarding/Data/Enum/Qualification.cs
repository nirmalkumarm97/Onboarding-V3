using System.ComponentModel;
using System.Runtime.Serialization;

namespace EmployeeOnboarding.Data.Enum
{
    public enum Qualification
    {
        [EnumMember(Value = "10th")]
        Tenth = 1,

        [EnumMember(Value = "12th")]
        Twelfth = 2,

        [EnumMember(Value = "Diploma")]
        Diploma = 3,

        [EnumMember(Value = "Graduate")]
        UG = 4,

        [EnumMember(Value = "Post Graduate")]
        PG = 5,

        [EnumMember(Value = "PhD")]
        Phd = 6
    }
}
