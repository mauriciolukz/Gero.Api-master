using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Gero.API.Enumerations
{
    public enum ProcessType
    {
        [EnumMember(Value = "2")]
        Upload,
        [EnumMember(Value = "1")]
        Download
    }
}
