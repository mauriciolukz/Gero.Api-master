using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Gero.API.Enumerations
{
    public enum Status
    {
        [EnumMember(Value = "Active")]
        Active,
        [EnumMember(Value = "Inactive")]
        Inactive,
        [EnumMember(Value = "Created")]
        Created,
        [EnumMember(Value = "Used")]
        Used
    }
}
