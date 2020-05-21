using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Gero.API.Enumerations
{
    public enum TypeOfRequest
    {
        [EnumMember(Value = "Mobile")]
        Mobile,
        [EnumMember(Value = "Web")]
        Web
    }
}
