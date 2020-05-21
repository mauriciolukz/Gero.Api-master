using Gero.API.Enumerations;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gero.API.Models
{
    [Table("UserDevices", Schema = "DISTRIBUCION")]
    public class UserDevice
    {
        public int UserId { get; set; }
        public User User { get; set; }

        public int DeviceId { get; set; }
        public Device Device { get; set; }

        [Required]
        public Status Status { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTimeOffset CreatedAt { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTimeOffset UpdatedAt { get; set; }
    }
}