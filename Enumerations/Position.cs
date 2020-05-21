using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Gero.API.Enumerations
{
    public enum Position
    {
        [EnumMember(Value = "Top")]
        Top,
        [EnumMember(Value = "Right")]
        Right,
        [EnumMember(Value = "Bottom")]
        Bottom,
        [EnumMember(Value = "Left")]
        Left,
        [EnumMember(Value = "Center")]
        Center
    }
}
