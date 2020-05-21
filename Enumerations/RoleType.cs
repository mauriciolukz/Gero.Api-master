using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Gero.API.Enumerations
{
    public enum RoleType
    {
        [EnumMember(Value = "0")]
        MOBILE,
        [EnumMember(Value = "1")]
        WEB
    }
}
