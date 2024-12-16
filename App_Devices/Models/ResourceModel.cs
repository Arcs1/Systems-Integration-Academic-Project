using System;

namespace App_Devices.Models
{
    public abstract class ResourceModel
    {
        public int Id { get; set; }
        public DateTime Creation_dt { get; set; }
    }
}
